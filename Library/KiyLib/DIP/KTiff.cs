using BitMiracle.LibTiff.Classic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiyLib.DIP
{
    // https://bitmiracle.github.io/libtiff.net/?topic=html/e4f25423-eede-4ef6-a920-9cb539d056c6.htm <- 샘플코드 사이트

    /// <summary>
    /// Tiff 이미지 포맷을 지원하기 위한 클래스
    /// </summary>
    public class KTiff : ITagInfos, IDisposable
    {
        private Tiff _tif;
        private int _width;
        private int _height;
        private KDepthType _depth = KDepthType.None;
        private KColorType _color = KColorType.Gray;
        private dynamic[] _data;
        private string _descriptionTag;
        private string _path;

        /// <summary>
        /// 가로 길이
        /// </summary>
        public int Width
        {
            get { return _width; }
            private set { _width = value; }
        }

        /// <summary>
        /// 세로 길이
        /// </summary>
        public int Height
        {
            get { return _height; }
            private set { _height = value; }
        }

        /// <summary>
        /// 픽셀 데이터
        /// </summary>
        public dynamic[] Data
        {
            get { return _data; }
            private set { _data = value; }
        }

        /// <summary>
        /// DescriptionTag의 내용
        /// </summary>
        public string DescriptionTag
        {
            get { return _descriptionTag; }
            set { _descriptionTag = value; }
        }

        /// <summary>
        /// Depth(bit)
        /// </summary>
        public KDepthType Depth
        {
            get { return _depth; }
            private set { _depth = value; }
        }

        /// <summary>
        /// 색상 형식
        /// </summary>
        public KColorType Color
        {
            get { return _color; }
            private set { _color = value; }
        }

        /// <summary>
        /// 색상 채널의 수(흑백:1, 칼라:3)
        /// </summary>
        public int NumberOfChannels
        {
            get { return (_color == KColorType.Gray) ? 1 : 3; }
        }


        /// <summary>
        /// 생성자
        /// 특정 경로의 Tiff 이미지를 불러온다
        /// </summary>
        /// <param name="path">Tiff 이미지의 경로</param>
        public KTiff(string path)
        {
            _tif?.Dispose();
            _tif = null;

            this.Load(path);
        }

        /// <summary>
        /// 생성자
        /// Tiff 이미지를 생성한다
        /// </summary>
        /// <param name="width">가로 길이</param>
        /// <param name="height">세로 길이</param>
        /// <param name="depth">Depth(bit)</param>
        /// <param name="color">색상 형식</param>
        public KTiff(int width, int height, KDepthType depth, KColorType color)
        {
            this.Width = width;
            this.Height = height;
            this._depth = depth;
            this._color = color;

            this._data = new dynamic[width * height * NumberOfChannels];

            switch (depth)
            {
                case KDepthType.Dt_8:
                case KDepthType.Dt_24:
                    for (int i = 0; i < _data.Length; i++)
                        _data[i] = (byte)0;
                    break;

                case KDepthType.Dt_16:
                    for (int i = 0; i < _data.Length; i++)
                        _data[i] = (ushort)0;
                    break;

                case KDepthType.None:
                    throw new Exception("Depth는 None일수 없습니다");
            }
        }


        /// <summary>
        /// 이미지를 저장한다
        /// </summary>
        /// <param name="path">저장할 경로</param>
        public void Save(string path)
        {
            Save(path, this.Data);
        }

        /// <summary>
        /// 이미지를 저장한다
        /// </summary>
        /// <param name="path">저장할 경로</param>
        /// <param name="imageArr">이미지의 픽셀 데이터, 이미지의 크기와 데이터의 길이가 일치하지 않으면 이미지가 깨진다</param>
        public void Save(string path, dynamic[] imageArr)
        {
            using (Tiff tifOut = Tiff.Open(path, "w"))
            {
                if (_tif == null)
                    InitTiffFieldByDefault(tifOut);
                else
                {
                    //tif 태그를 tifOut에 전부 복사
                    //STRIPOFFSETS, STRIPBYTECOUNTS가 원본파일과 다르게 복사됨(이미지 열기등은 정상동작) 원인은 불명
                    for (ushort t = ushort.MinValue; t < ushort.MaxValue; ++t)
                    {
                        TiffTag tag = (TiffTag)t;
                        FieldValue[] value = _tif.GetField(tag);

                        if (value != null)
                            tifOut.SetField(tag, value.Select(v => v.Value).ToArray());
                    }
                }

                int stride = _width * NumberOfChannels;

                if (_depth == KDepthType.Dt_16)
                    WriteTiff<ushort>(tifOut, imageArr, stride);
                else
                    WriteTiff<byte>(tifOut, imageArr, stride);
            }
        }

        // 현재 Description Tag 읽는부분만 구현 (이미지 기본 정보 제외)
        /// <summary>
        /// 이미지를 불러온다
        /// </summary>
        /// <param name="path">이미지의 경로</param>
        public void Load(string path)
        {
            _tif = Tiff.Open(path, "r");
            this._path = path;

            if (_tif == null)
            {
                MessageBox.Show("Could not open incoming image", "Format Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw new FormatException("Could not open incoming image");
            }

            // Image info get
            _width = _tif.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
            _height = _tif.GetField(TiffTag.IMAGELENGTH)[0].ToInt();
            int bPs = _tif.GetField(TiffTag.BITSPERSAMPLE)[0].ToInt();
            int sPp = _tif.GetField(TiffTag.SAMPLESPERPIXEL)[0].ToInt();
            _depth = (KDepthType)(bPs * sPp);
            _color = (_depth == KDepthType.Dt_24) ? KColorType.Color : KColorType.Gray;
            _descriptionTag = _tif.GetField(TiffTag.IMAGEDESCRIPTION)?[0].ToString() ?? "";

            if (_depth == KDepthType.None)
                throw new Exception("Depth는 None일수 없습니다");

            // byte[][]로 픽셀 데이터 불러오기
            _data = new dynamic[_width * _height * NumberOfChannels];
            int scanlineSize = _tif.ScanlineSize();
            byte[][] temp = new byte[_height][];
            byte[] dstBuffer = new byte[scanlineSize * _height];

            for (int i = 0; i < _height; i++)
            {
                temp[i] = new byte[scanlineSize];
                _tif.ReadScanline(temp[i], i);
                Buffer.BlockCopy(temp[i], 0, dstBuffer, i * scanlineSize, temp[i].Length * sizeof(byte));
            }

            if (_depth == KDepthType.Dt_16)
            {
                var usData = KImage.Convert2ByteToUShort(dstBuffer);

                for (int i = 0; i < usData.Length; i++)
                    _data[i] = usData[i];
            }
            else
            {
                for (int i = 0; i < dstBuffer.Length; i++)
                    _data[i] = dstBuffer[i];
            }
        }

        /// <summary>
        /// Tag 정보를 가져온다
        /// 모든 Tag정보가 아닌 자주 사용되는 Tag들만 가져온다
        /// </summary>
        /// <returns>결과 값(Tag이름, Tag값)</returns>
        public Dictionary<string, string> GetTagInfos()
        {
            var rtTags = new Dictionary<string, string>();

            rtTags.Add("Filename", System.IO.Path.GetFileName(_path));
            rtTags.Add("Image Width", _width.ToString());
            rtTags.Add("Image Height", _height.ToString());

            var bitsPerSample = _tif.GetField(TiffTag.BITSPERSAMPLE)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(bitsPerSample))
                rtTags.Add("Bits Per Sample", bitsPerSample);

            var compression = _tif.GetField(TiffTag.COMPRESSION)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(compression))
                rtTags.Add("Compression", compression);

            var photometricInterpretation = _tif.GetField(TiffTag.PHOTOMETRIC)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(photometricInterpretation))
                rtTags.Add("Photometric Interpretation", photometricInterpretation);

            if (!string.IsNullOrEmpty(_descriptionTag))
                rtTags.Add("Image Description", _descriptionTag);

            //var stripOffset = (Array)tif.GetField(TiffTag.STRIPOFFSETS)?[0].Value ?? null;
            //if (stripOffset != null && stripOffset.Length > 0)
            //{
            //    string result = "";
            //    for (int i = 0; i < stripOffset.Length; i++)
            //        result += stripOffset.GetValue(i).ToString() + ", ";

            //    rtTags.Add("StripOffset", result);
            //}

            var orientation = _tif.GetField(TiffTag.ORIENTATION)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(orientation))
                rtTags.Add("Orientation", orientation);

            var samplePerPixel = _tif.GetField(TiffTag.SAMPLESPERPIXEL)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(samplePerPixel))
                rtTags.Add("Samples Per Pixel", samplePerPixel);

            var rowsPerStrip = _tif.GetField(TiffTag.ROWSPERSTRIP)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(rowsPerStrip))
                rtTags.Add("Rows Per Strip", rowsPerStrip);

            //var stripByteCnt = (Array)tif.GetField(TiffTag.STRIPBYTECOUNTS)?[0].Value ?? null;
            //if (stripByteCnt != null && stripByteCnt.Length > 0)
            //{
            //    string result = "";
            //    for (int i = 0; i < stripByteCnt.Length; i++)
            //        result += stripByteCnt.GetValue(i).ToString() + ", ";

            //    rtTags.Add("StripByteCount", result);
            //}

            var xRes = _tif.GetField(TiffTag.XRESOLUTION)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(xRes))
                rtTags.Add("X Resolution", xRes);

            var yRes = _tif.GetField(TiffTag.YRESOLUTION)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(yRes))
                rtTags.Add("Y Resolution", yRes);

            //var planarConfig = tif.GetField(TiffTag.PLANARCONFIG)?[0].ToString() ?? "";
            //if (!string.IsNullOrEmpty(planarConfig))
            //    rtTags.Add("PlanarConfiguration", planarConfig);

            var resUnit = _tif.GetField(TiffTag.RESOLUTIONUNIT)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(resUnit))
                rtTags.Add("Resolution Unit", resUnit);

            var soft = _tif.GetField(TiffTag.SOFTWARE)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(soft))
                rtTags.Add("Software", soft);

            var artist = _tif.GetField(TiffTag.ARTIST)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(artist))
                rtTags.Add("Artist", artist);

            //var exifOffset = tif.GetField(TiffTag.EXIFIFD)?[0].ToString() ?? "";
            //if (!string.IsNullOrEmpty(exifOffset))
            //    rtTags.Add("Exif Offset", exifOffset);

            var shutterSpeedVal = _tif.GetField(TiffTag.EXIF_SHUTTERSPEEDVALUE)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(shutterSpeedVal))
                rtTags.Add("Shutter Speed", shutterSpeedVal);

            var isoSpeedRating = _tif.GetField(TiffTag.EXIF_ISOSPEEDRATINGS)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(isoSpeedRating))
                rtTags.Add("Iso Speed Rating", isoSpeedRating);

            var exposureTime = _tif.GetField(TiffTag.EXIF_EXPOSURETIME)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(exposureTime))
                rtTags.Add("Exposure Time", exposureTime);

            var make = _tif.GetField(TiffTag.MAKE)?[0].ToString() ?? "";
            if (!string.IsNullOrEmpty(make))
                rtTags.Add("MAKE", make);

            return rtTags;
        }


        /// <summary>
        /// Tiff파일을 생성할때 필요한 Tag들을 초기화한다
        /// 기존에 존재하는 Tiff파일을 로드할때는 사용되지 않는다
        /// </summary>
        /// <param name="tiff"></param>
        private void InitTiffFieldByDefault(Tiff tiff)
        {
            tiff.SetField(TiffTag.IMAGEWIDTH, Width);
            tiff.SetField(TiffTag.IMAGELENGTH, Height);
            tiff.SetField(TiffTag.SAMPLESPERPIXEL, NumberOfChannels);
            tiff.SetField(TiffTag.BITSPERSAMPLE, (int)Depth / NumberOfChannels);
            tiff.SetField(TiffTag.ORIENTATION, BitMiracle.LibTiff.Classic.Orientation.TOPLEFT);
            tiff.SetField(TiffTag.RESOLUTIONUNIT, ResUnit.NONE);
            tiff.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);
            tiff.SetField(TiffTag.COMPRESSION, Compression.NONE);

            if (NumberOfChannels == 1)
                tiff.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISBLACK);
            else
                tiff.SetField(TiffTag.PHOTOMETRIC, Photometric.RGB);

            //tiff.SetField(TiffTag.ROWSPERSTRIP, Height);
            //tiff.SetField(TiffTag.FILLORDER, FillOrder.MSB2LSB);

            //tiff.SetField(TiffTag.XRESOLUTION, 100);
            //tiff.SetField(TiffTag.YRESOLUTION, 100);
            //tiff.SetField(TiffTag.RESOLUTIONUNIT, 3);    //RESUNIT_NONE = 1;
            //RESUNIT_INCH = 2;
            //RESUNIT_CENTIMETER = 3;

            //descriptionTag 추가 루틴
            if (string.IsNullOrEmpty(_descriptionTag))
                return;

            tiff.SetField(TiffTag.EXIFIFD, 8);  //EXIFIFD offset은 보통 8로 정한다고 한다.(확실치는 않으나 동작에는 이상없음)
            tiff.SetField(TiffTag.IMAGEDESCRIPTION, _descriptionTag);
        }

        /// <summary>
        /// 이미지에 픽셀 데이터를 대입한다
        /// </summary>
        /// <typeparam name="T">픽셀 데이터의 타입(ex) byte, ushort)</typeparam>
        /// <param name="tifOut">사용할 Tiff객체</param>
        /// <param name="imageArr">대입할 픽셀 데이터</param>
        /// <param name="stride">이미지의 stride</param>
        private void WriteTiff<T>(Tiff tifOut, dynamic[] imageArr, int stride)
        {
            int tSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
            T[] cvtImgArr = new T[imageArr.Length];

            for (int i = 0; i < cvtImgArr.Length; i++)
                cvtImgArr[i] = imageArr[i];

            for (int i = 0; i < Height; i++)
            {
                T[] samples = new T[stride];
                int srcIndex = i * stride;

                Array.Copy(cvtImgArr, srcIndex, samples, 0, samples.Length);

                byte[] buffer = new byte[samples.Length * tSize];
                Buffer.BlockCopy(samples, 0, buffer, 0, buffer.Length);
                tifOut.WriteScanline(buffer, i);
            }
        }


        // IDisposable
        /// <summary>
        /// 사용된 리소스를 해제한다
        /// </summary>
        public void Dispose()
        {
            if (_tif != null)
                _tif.Dispose();
        }
    }
}
