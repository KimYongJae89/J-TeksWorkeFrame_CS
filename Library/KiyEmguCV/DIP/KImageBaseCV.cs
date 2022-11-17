using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using KiyControls.Forms;
using KiyEmguCV.Forms;
using KiyLib.DIP;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace KiyEmguCV.DIP
{
    /// <summary>
    /// 이미지 프로세싱의 전반적인 기능을 제공하는 클래스
    /// </summary>
    public partial class KImageBaseCV : IKImage, IKROI, IKFourierTransform, IKConvertibleToBitmap, IDisposable
    {
        protected IImage cvIImg;
        protected Mat cvMat;
        protected Mat cvMatLoadByDepth;
        protected Mat cvMatLoadByColor;
        protected byte[,,] data8;
        protected ushort[,,] data16;

        /// <summary>
        /// 이미지의 포맷 기본값: None (지원포맷: RAW, TIFF, BMP, JPEG, PNG, DCM)
        /// </summary>
        public KImageFormat Format { get; protected set; }

        /// <summary>
        /// 이미지의 Depth타입을 나타낸다,
        /// 8bit: 흑백Only, 16bit: 흑백Only, 24bit: 칼라Only
        /// </summary>
        public KDepthType Depth { get; protected set; }

        /// <summary>
        /// 이미지의 색상타입 흑백또는 칼라를 나타낸다.
        /// </summary>
        public KColorType Color { get; protected set; }

        /// <summary>
        /// 이미지의 가로길이
        /// </summary>
        public int Width { get; protected set; }

        /// <summary>
        /// 이미지의 세로길이
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        /// 이미지의 색상채널의 수 (흑백:1, 칼라:3 고정)
        /// </summary>
        public int NumberOfChannels { get; protected set; }

        /// <summary>
        /// 이미지 파일의 경로
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// 픽셀의 가로 Pitch 크기(단위: mm)
        /// </summary>
        public double PixelPitchX_mm { get; set; }

        /// <summary>
        /// 픽셀의 세로 Pitch 크기(단위: mm)
        /// </summary>
        public double PixelPitchY_mm { get; set; }


        /// <summary>
        /// 이미지의 크기(FileSize)
        /// </summary>
        public int ImageSize_Byte
        {
            get
            {
                if (data8 != null && data16 != null)
                    throw new Exception("Get Property: ImageSize_Byte ERR - data8, data16 both of them not empty");
                if (data8 == null && data16 == null)
                    return 0;
                if (data8 != null && data16 == null)
                {
                    var typeSz = sizeof(byte);
                    var arrLen = data8.GetLength(0) * data8.GetLength(1) * data8.GetLength(2);
                    return arrLen * typeSz;
                }
                if (data8 == null && data16 != null)
                {
                    var typeSz = sizeof(ushort);
                    var arrLen = data16.GetLength(0) * data16.GetLength(1) * data16.GetLength(2);
                    return arrLen * typeSz;
                }

                throw new Exception("Get Property: ImageSize_Byte ERR");
            }
        }


        /// <summary>
        /// 이미지를 생성한다.
        /// </summary>
        protected KImageBaseCV() { }

        /// <summary>
        /// 이미지를 생성한다.
        /// </summary>
        /// <param name="path">원본 이미지의 경로</param>
        public KImageBaseCV(string path)
        {
            Initialize();

            this.Load(path);
        }

        /// <summary>
        /// 이미지를 생성한다.
        /// </summary>
        /// <param name="width">이미지의 가로길이</param>
        /// <param name="height">이미지의 세로길이</param>
        /// <param name="depth">이미지의 Depth(bit)</param>
        public KImageBaseCV(int width, int height, KDepthType depth)
        {
            if (depth == KDepthType.None)
                throw new FormatException(LangResource.ER_JIMG_DepthFmt);

            Initialize();

            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            DepthType cvDT;

            switch (Depth)
            {
                case KDepthType.Dt_8:
                    cvDT = DepthType.Cv8U;
                    Color = KColorType.Gray;
                    NumberOfChannels = 1;
                    break;

                case KDepthType.Dt_16:
                    cvDT = DepthType.Cv16U;
                    Color = KColorType.Gray;
                    NumberOfChannels = 1;
                    break;

                case KDepthType.Dt_24:
                    cvDT = DepthType.Cv8U;
                    Color = KColorType.Color;
                    NumberOfChannels = 3;
                    break;

                case KDepthType.None:
                default:
                    throw new FormatException(LangResource.ER_JIMG_DepthFmt);
            }

            this.cvMat = new Mat(new Size(width, height), cvDT, NumberOfChannels);
            cvMat.SetTo(new MCvScalar(0)); //0으로 초기화

            switch (Depth)
            {
                case KDepthType.Dt_8:
                    Color = KColorType.Gray;
                    cvIImg = cvMat.ToImage<Gray, byte>();
                    DataCopyFromCVImgs<Gray, byte>();
                    break;

                case KDepthType.Dt_16:
                    Color = KColorType.Gray;
                    cvIImg = cvMat.ToImage<Gray, ushort>();
                    DataCopyFromCVImgs<Gray, ushort>();
                    break;

                case KDepthType.Dt_24:
                    Color = KColorType.Color;
                    cvIImg = cvMat.ToImage<Bgr, byte>();
                    DataCopyFromCVImgs<Bgr, byte>();
                    break;

                case KDepthType.None:
                default:
                    throw new FormatException(LangResource.ER_JIMG_DepthFmt);
            }
        }

        /// <summary>
        /// 이미지를 생성한다.
        /// </summary>
        /// <param name="mat">원본 Mat객체</param>
        /// <param name="depth">이미지의 Depth(bit)</param>
        public KImageBaseCV(Mat mat, KDepthType depth)
        {
            if (depth == KDepthType.None)
                throw new FormatException(LangResource.ER_JIMG_DepthFmt);

            Initialize();

            this.Width = mat.Width;
            this.Height = mat.Height;
            this.Depth = depth;
            this.cvMat = mat.Clone();

            switch (Depth)
            {
                case KDepthType.Dt_8:
                    Color = KColorType.Gray;
                    cvIImg = cvMat.ToImage<Gray, byte>();
                    DataCopyFromCVImgs<Gray, byte>();
                    NumberOfChannels = 1;
                    break;

                case KDepthType.Dt_16:
                    Color = KColorType.Gray;
                    cvIImg = cvMat.ToImage<Gray, ushort>();
                    DataCopyFromCVImgs<Gray, ushort>();
                    NumberOfChannels = 1;
                    break;

                case KDepthType.Dt_24:
                    Color = KColorType.Color;
                    cvIImg = cvMat.ToImage<Bgr, byte>();
                    DataCopyFromCVImgs<Bgr, byte>();
                    NumberOfChannels = 3;
                    break;

                case KDepthType.None:
                default:
                    throw new FormatException(LangResource.ER_JIMG_DepthFmt);
            }
        }


        /// <summary>
        /// 이미지를 불러온다.
        /// </summary>
        /// <param name="path">원본 이미지의 경로</param>
        public void Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(LangResource.ER_JIMG_FileNotExist);

            Initialize();

            Path = path;
            Format = KImageCV.CheckImageFormat(path);
            Depth = KImageCV.CheckDepthType(path);
            NumberOfChannels = KImageCV.CheckNumOfChannels(path);

            switch (Format)
            {
                case KImageFormat.TIFF:
                case KImageFormat.BMP:
                case KImageFormat.JPEG:
                case KImageFormat.PNG:
                    {
                        cvMat = CvInvoke.Imread(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);

                        switch (this.Depth)
                        {
                            case KDepthType.Dt_8:
                                Color = KColorType.Gray;
                                cvIImg = cvMat.ToImage<Gray, byte>();
                                DataCopyFromCVImgs<Gray, byte>();
                                break;

                            case KDepthType.Dt_16:
                                Color = KColorType.Gray;
                                cvIImg = cvMat.ToImage<Gray, ushort>();
                                DataCopyFromCVImgs<Gray, ushort>();
                                break;

                            case KDepthType.Dt_24:
                                Color = KColorType.Color;
                                cvIImg = cvMat.ToImage<Bgr, byte>();
                                DataCopyFromCVImgs<Bgr, byte>();
                                break;

                            case KDepthType.None:
                            default:
                                throw new FormatException(LangResource.ER_JIMG_DepthFmt);
                        }
                        Width = cvMat.Width;
                        Height = cvMat.Height;
                    }
                    break;

                case KImageFormat.RAW:
                    {
                        RawImageConvertForm rawFrm = new RawImageConvertForm(Path);
                        rawFrm.FormClosed += RawFrm_FormClosed;
                        rawFrm.ShowDialog();
                    }
                    break;

                case KImageFormat.DCM:
                    {
                        KDicom dcm = new KDicom(path);

                        Width = dcm.Width;
                        Height = dcm.Height;
                        Color = dcm.Color;

                        //dcm 프레임 여러개일때
                        int numOfFrames = dcm.NumberOfFrames;
                        int selectedFrame = 0;

                        if (numOfFrames > 1)
                        {
                            List<Bitmap> thnailList = new List<Bitmap>();
                            Mat thnailMat;

                            switch (Depth)
                            {
                                case KDepthType.Dt_8:
                                    thnailMat = new Mat(new Size(Width, Height), DepthType.Cv8U, NumberOfChannels);
                                    Image<Gray, byte> thnailImg8 = thnailMat.ToImage<Gray, byte>();

                                    for (int i = 0; i < numOfFrames; i++)
                                    {
                                        var data = dcm.GetFrameData<byte>(i, false);
                                        thnailImg8.Data = data;
                                        thnailList.Add(thnailImg8.ToBitmap());
                                    }
                                    break;

                                case KDepthType.Dt_16:
                                    thnailMat = new Mat(new Size(Width, Height), DepthType.Cv16U, NumberOfChannels);
                                    Image<Gray, ushort> thnailImg16 = cvMat.ToImage<Gray, ushort>();

                                    for (int i = 0; i < numOfFrames; i++)
                                    {
                                        var data = dcm.GetFrameData<ushort>(i, false);
                                        thnailImg16.Data = data;
                                        thnailList.Add(thnailImg16.ToBitmap());
                                    }
                                    break;

                                case KDepthType.Dt_24:
                                    thnailMat = new Mat(new Size(Width, Height), DepthType.Cv8U, NumberOfChannels);
                                    Image<Bgr, byte> thnailImg24 = cvMat.ToImage<Bgr, byte>();

                                    for (int i = 0; i < numOfFrames; i++)
                                    {
                                        var data = dcm.GetFrameData<byte>(i, false);
                                        thnailImg24.Data = data;
                                        thnailList.Add(thnailImg24.ToBitmap());
                                    }
                                    break;

                                case KDepthType.None:
                                default:
                                    break;
                            }

                            DicomOpenForm dcmFrm = new DicomOpenForm(thnailList);
                            dcmFrm.SelectClicked += (s, e) =>
                            {
                                var frm = s as DicomOpenForm;
                                selectedFrame = frm.SelectedFrameIndex;
                            };
                            dcmFrm.ShowDialog();
                        }

                        switch (Depth)
                        {
                            case KDepthType.Dt_8:
                                cvMat = new Mat(new Size(Width, Height), DepthType.Cv8U, NumberOfChannels);
                                Image<Gray, byte> img8 = cvMat.ToImage<Gray, byte>();

                                data8 = dcm.GetFrameData<byte>(selectedFrame, false);  //EnguCV에 맞게 BGR로 순서 변경
                                img8.Data = data8;

                                this.cvIImg = img8;
                                this.cvMat = img8.Mat;
                                break;

                            case KDepthType.Dt_16:
                                cvMat = new Mat(new Size(Width, Height), DepthType.Cv16U, NumberOfChannels);
                                Image<Gray, ushort> img16 = cvMat.ToImage<Gray, ushort>();

                                data16 = dcm.GetFrameData<ushort>(selectedFrame, false);  //EnguCV에 맞게 BGR로 순서 변경
                                img16.Data = data16;

                                this.cvIImg = img16;
                                this.cvMat = img16.Mat;
                                break;

                            case KDepthType.Dt_24:
                                cvMat = new Mat(new Size(Width, Height), DepthType.Cv8U, NumberOfChannels);
                                Image<Bgr, byte> img24 = cvMat.ToImage<Bgr, byte>();

                                data8 = dcm.GetFrameData<byte>(selectedFrame, false);  //EnguCV에 맞게 BGR로 순서 변경
                                img24.Data = data8;

                                this.cvIImg = img24;
                                this.cvMat = img24.Mat;
                                break;

                            case KDepthType.None:
                            default:
                                break;
                        }
                    }
                    break;

                case KImageFormat.None:
                default:
                    throw new FormatException(LangResource.ER_JIMG_DepthFmt);
            }
        }

        /// <summary>
        /// 이미지를 Path속성 경로에 저장한다.  
        /// </summary>
        public void Save()
        {
            Save(this.Path);
        }

        /// <summary>
        /// 이미지를 저장한다.
        /// </summary>
        /// <param name="path">이미지를 저장할 경로</param>
        public void Save(string path)
        {
            Save(path, this.Color);
        }

        //Dicom(8, 16, 24)포맷 저장 구현할것
        //칼라 타입 변경할 의도로 만들었으나 일단 보류 (tiff 16bit->24bitClr, Dicom 변환문제)
        /// <summary>
        /// 이미지를 저장한다. 
        /// </summary>
        /// <param name="path">이미지를 저장할 경로</param>
        /// <param name="colorType">저장할 칼라타입. 해당 인자에 따라 칼라타입이 변환돼서 저장된다.</param>
        protected void Save(string path, KColorType colorType)
        {
            IImage tempImg = null;
            KImageFormat targetFmt = KImageCV.CheckImageFormat(path);

            if (targetFmt == KImageFormat.RAW)
            {
                using (var outputStream = File.Open(path, FileMode.Create))
                using (var writer = new BinaryWriter(outputStream))
                {
                    if (data8 != null)
                    {
                        var cvtData = KImage.Convert3Dto1D(data8);

                        for (int i = 0; i < cvtData.Length; i++)
                            writer.Write(cvtData[i]);
                    }
                    else if (data16 != null)
                    {
                        var cvtData = KImage.Convert3Dto1D(data16);

                        for (int i = 0; i < cvtData.Length; i++)
                            writer.Write(cvtData[i]);
                    }
                    else
                        throw new Exception(LangResource.ER_JIMG_data8And16isNull);
                }

                return;
            }

            //16비트로 저장할때 tif, dcm포맷이 아니면 8비트로 변환후 저장
            if ((this.Depth == KDepthType.Dt_16 && targetFmt != KImageFormat.TIFF) &&
                (this.Depth == KDepthType.Dt_16 && targetFmt != KImageFormat.DCM))
            {
                //throw new Exception("16비트 저장은 TIFF, DCM 포맷만 지원합니다.");

                Mat mat8;
                if (cvMat != null)
                {
                    mat8 = this.To8BitG(cvMat);
                    var cvtImg = mat8.ToImage<Gray, byte>();
                    CvInvoke.Imwrite(path, cvtImg);
                }
                else
                    throw new Exception(LangResource.ER_JIMG_CvMatIsNull);

                return;
            }

            switch (targetFmt)
            {
                case KImageFormat.None:
                    throw new FormatException(
                        string.Format("KImageCV - Save(..): Incorrect extensions \"{0}\"", System.IO.Path.GetExtension(path)));

                case KImageFormat.TIFF:
                    {
                        if (colorType == KColorType.Gray)
                        {
                            //16비트 흑백이미지는 imwrite로는 제대로 저장하지 못한다.
                            //따라서 KTiff클래스로 따로 저장
                            if (Depth == KDepthType.Dt_16)
                            {
                                using (KTiff tif = new KTiff(Width, Height, Depth, colorType))
                                {
                                    if (data8 != null)
                                        tif.Save(path, KImage.Convert3Dto1D<byte, dynamic>(data8));
                                    else if (data16 != null)
                                        tif.Save(path, KImage.Convert3Dto1D<ushort, dynamic>(data16));
                                    else
                                        throw new Exception(LangResource.ER_JIMG_TiffSaveNullData);
                                }
                                break;
                            }
                            else //Dt_8
                                tempImg = cvMat.ToImage<Gray, byte>();
                        }
                        else //KColorType.Color
                            tempImg = cvMat.ToImage<Bgr, byte>();

                        //ImwriteFlags에는 Tiff flag가 없기 때문에 libTiff 태그를 직접 캐스팅해 넣어줘야한다.
                        //EmguCV에서 libTiff라이브러리 쓰는걸로 추정
                        int TIFFTAG_COMPRESSION = 259;
                        int COMPRESSION_NONE = 1;

                        KeyValuePair<ImwriteFlags, int> noCompParam
                            = new KeyValuePair<ImwriteFlags, int>((ImwriteFlags)TIFFTAG_COMPRESSION, COMPRESSION_NONE);
                        CvInvoke.Imwrite(path, tempImg, noCompParam);
                    }
                    break;

                //작업진행중
                case KImageFormat.DCM:
                    {
                        KDicom dcm = new KDicom(Width, Height, Depth, colorType);

                        int offset = 0, srcIdx = 0, dstIdx = 0;

                        if (colorType == KColorType.Gray)
                        {
                            if (data8 != null) //8
                            {
                                var cvt3to1Data = KImage.Convert3Dto1D(data8);

                                if (Width % 4 != 0)
                                    offset = 4 - (Width % 4);

                                if (offset == 0)
                                {
                                    dcm.Save(path, cvt3to1Data, KColorType.Gray);
                                }
                                else
                                {
                                    byte[] offsetCvtData = new byte[cvt3to1Data.Length];

                                    if (data8.GetLength(1) % 4 == 0)
                                    {
                                        for (int h = 0; h < Height; h++)
                                        {
                                            dstIdx = Width * h;
                                            srcIdx = dstIdx + offset * h;

                                            Array.Copy(cvt3to1Data, srcIdx, offsetCvtData, dstIdx, Width);
                                        }
                                        dcm.Save(path, offsetCvtData, KColorType.Gray);
                                    }
                                    else //offset가 Width모두가 4의 배수가 아닌경우는 dcm을 dcm으로 저장하는 경우
                                    {
                                        dcm.Save(path, cvt3to1Data, KColorType.Gray);
                                    }
                                }
                            }
                            else if (data16 != null)  //16bit
                            {
                                var cvt3to1Data = KImage.Convert3Dto1D(data16);

                                dcm.Save(path, cvt3to1Data);
                            }
                            else
                                throw new Exception(LangResource.ER_JIMG_TiffSaveNullData);
                        }
                        else //KColorType.Color
                        {
                            if (data8 != null) //24
                            {
                                var cvt3to1Data = KImage.Convert3Dto1D(data8);

                                if (Width % 4 != 0)
                                    offset = 4 - (Width % 4);

                                if (offset == 0)
                                {
                                    dcm.Save(path, cvt3to1Data, KColorType.Color);
                                }
                                else
                                {
                                    byte[] offsetCvtData = new byte[cvt3to1Data.Length];

                                    if (data8.GetLength(1) % 4 == 0)
                                    {
                                        for (int h = 0; h < Height; h++)
                                        {
                                            dstIdx = Width * h * 3; //칼라라서 * 3
                                            srcIdx = dstIdx + (offset * h * 3);

                                            Array.Copy(cvt3to1Data, srcIdx, offsetCvtData, dstIdx, Width * 3);
                                        }

                                        dcm.Save(path, offsetCvtData, KColorType.Color);
                                    }
                                    else //offset가 Width모두가 4의 배수가 아닌경우는 dcm을 dcm으로 저장하는 경우
                                    {
                                        dcm.Save(path, cvt3to1Data, KColorType.Color);
                                    }
                                }
                            }
                            else
                                throw new Exception(LangResource.ER_JIMG_TiffSaveNullData);
                        }
                    }
                    break;

                default:
                    {
                        if (colorType == KColorType.Gray)
                            tempImg = cvMat.ToImage<Gray, byte>();
                        else
                            tempImg = cvMat.ToImage<Bgr, byte>();

                        CvInvoke.Imwrite(path, tempImg);
                    }
                    break;
            }
        }

        /// <summary>
        /// 픽셀의 밝기값을 가져온다. (흑백 전용)
        /// </summary>
        /// <param name="x">픽셀의 x 좌표</param>
        /// <param name="y">픽셀의 y 좌표</param>
        /// <returns>픽셀의 밝기</returns>
        public int GetPixelGray(int x, int y)
        {
            int rt = 0;

            if (NumberOfChannels != 1)
                throw new FormatException(LangResource.ER_JIMG_IsNotGrayImg);

            if (Depth == KDepthType.Dt_8)
                rt = data8[y, x, 0];
            if (Depth == KDepthType.Dt_16)
                rt = data16[y, x, 0];
            if (Depth == KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_IsNotGrayImg);

            return rt;
        }

        /// <summary>
        /// 픽셀의 밝기값을 가져온다. (칼라 전용)
        /// </summary>
        /// <param name="x">픽셀의 x 좌표</param>
        /// <param name="y">픽셀의 y 좌표</param>
        /// <returns>픽셀의 밝기</returns>
        public KColor GetPixelColor(int x, int y)
        {
            int[] rt = new int[3];

            if (NumberOfChannels != 3)
                throw new FormatException(LangResource.ER_JIMG_IsNotColorImg);

            return new KColor(data8[y, x, 0], data8[y, x, 1], data8[y, x, 2]);
        }

        /// <summary>
        /// 픽셀의 밝기값을 설정한다. (흑백 전용)
        /// </summary>
        /// <param name="x">픽셀의 x 좌표</param>
        /// <param name="y">픽셀의 y 좌표</param>
        /// <param name="GrayValue">설정할 픽셀 밝기</param>
        public void SetPixelGray(int x, int y, int GrayValue)
        {
            histoGray = histoB = histoG = histoR = null;
            switch (Depth)
            {
                case KDepthType.Dt_8:
                    var img8 = cvIImg as Image<Gray, byte>;
                    img8.Data[y, x, 0] = (byte)GrayValue;
                    cvMat = img8.Mat;
                    break;
                case KDepthType.Dt_16:
                    var img16 = cvIImg as Image<Gray, ushort>;
                    img16.Data[y, x, 0] = (ushort)GrayValue;
                    cvMat = img16.Mat;
                    break;
                case KDepthType.Dt_24:
                case KDepthType.None:
                default:
                    throw new FormatException(LangResource.ER_JIMG_IsNotColorImg);
            }
        }

        /// <summary>
        /// 픽셀의 밝기값을 설정한다. (칼라 전용)
        /// </summary>
        /// <param name="x">픽셀의 x 좌표</param>
        /// <param name="y">픽셀의 y 좌표</param>
        /// <param name="GrayValue">설정할 픽셀 밝기</param>
        public void SetPixelColor(int x, int y, KColor clr)
        {
            histoGray = histoB = histoG = histoR = null;
            switch (Depth)
            {
                case KDepthType.Dt_24:
                    var img24 = cvIImg as Image<Bgr, byte>;
                    img24.Data[y, x, 0] = clr.B;
                    img24.Data[y, x, 1] = clr.G;
                    img24.Data[y, x, 2] = clr.R;
                    cvMat = img24.Mat;
                    break;
                case KDepthType.Dt_8:
                case KDepthType.Dt_16:
                case KDepthType.None:
                default:
                    throw new FormatException(LangResource.ER_JIMG_IsNotColorImg);
            }
        }

        /// <summary>
        /// 직선의 길이(mm)를 계산한다.
        /// </summary>
        /// <param name="startX">시작점의 X좌표</param>
        /// <param name="startY">시작점의 Y좌표</param>
        /// <param name="endX">끝점의 X좌표</param>
        /// <param name="endY">끝점의 Y좌표</param>
        /// <param name="digit">결과값의 소수점 자릿수 표시범위(2면 소수점 두번째 자리까지 표시)</param>
        /// <returns>계산된 직선의 길이(mm)</returns>
        public double GetDistance_mm(int startX, int startY, int endX, int endY, int digit = 2)
        {
            return GetDistance_mm(new Point(startX, startY), new Point(endX, endY), digit);
        }

        /// <summary>
        /// 직선의 길이(mm)를 계산한다.
        /// </summary>
        /// <param name="start">시작점</param>
        /// <param name="end">끝점</param>
        /// <param name="digit">결과값의 소수점 자릿수 표시범위(2면 소수점 두번째 자리까지 표시)</param>
        /// <returns>계산된 직선의 길이(mm)</returns>
        public double GetDistance_mm(Point start, Point end, int digit = 2)
        {
            double x = Math.Abs(start.X - end.X) * PixelPitchX_mm;
            double y = Math.Abs(start.Y - end.Y) * PixelPitchY_mm;
            double dist = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

            return Math.Round(dist, digit);
        }

        /// <summary>
        /// 직선의 실제 길이(mm)를 바탕으로 PixelPitch(각 픽셀의 실제 사이즈)를 세팅한다.
        /// 이미지에서 특정 시작점과 끝점이 실제로 몇 mm인지 알고있을때 사용한다.
        /// </summary>
        /// <param name="startX">시작점의 X좌표</param>
        /// <param name="startY">시작점의 Y좌표</param>
        /// <param name="endX">끝점의 X좌표</param>
        /// <param name="endY">끝점의 Y좌표</param>
        /// <param name="realLineLength_mm">직선의 실제길이(mm)</param>
        public void SetPixelPitchXYSizeByLineLength(int startX, int startY, int endX, int endY, double realLineLength_mm)
        {
            SetPixelPitchXYSizeByLineLength(new Point(startX, startY), new Point(endX, endY), realLineLength_mm);
        }

        /// <summary>
        /// 직선의 실제 길이(mm)를 바탕으로 PixelPitch(각 픽셀의 실제 사이즈)를 세팅한다.
        /// 이미지에서 특정 시작점과 끝점이 실제로 몇 mm인지 알고있을때 사용한다.
        /// </summary>
        /// <param name="start">시작점</param>
        /// <param name="end">끝점</param>
        /// <param name="realLineLength_mm">직선의 실제길이(mm)</param>
        public void SetPixelPitchXYSizeByLineLength(Point start, Point end, double realLineLength_mm)
        {
            double dist_pixel = KLine.GetDistance(start, end);

            if (dist_pixel == 0)
            {
                PixelPitchX_mm = PixelPitchY_mm = 0;
                return;
            }

            PixelPitchX_mm = PixelPitchY_mm = realLineLength_mm / dist_pixel;
        }

        /// <summary>
        /// 실제 FOV를 기준으로 PixelPitch(각 픽셀의 실제 사이즈)를 세팅한다.(길이는 mm단위)
        /// </summary>
        /// <param name="widthRealLength_mm">FOV에서 가로의 실제 길이(mm)</param>
        /// <param name="hieghtRealLength_mm">FOV에서 세로의 실제 길이(mm)</param>
        public void SetPixelPitchXYSize(double widthRealLength_mm, double hieghtRealLength_mm)
        {
            PixelPitchX_mm = widthRealLength_mm / Width;
            PixelPitchY_mm = hieghtRealLength_mm / Height;
        }

        /// <summary>
        /// 실제 FOV를 기준으로 PixelPitch(각 픽셀의 실제 사이즈)를 세팅한다.(길이는 mm단위)
        /// </summary>
        /// <param name="widthRealLength_mm">FOV에서 가로의 실제 길이(mm)</param>
        /// <param name="hieghtRealLength_mm">FOV에서 세로의 실제 길이(mm)</param>
        /// <param name="width">이미지의 가로 길이</param>
        /// <param name="height">이미지의 세로 길이</param>
        public void SetPixelPitchXYSize(double widthRealLength_mm, double hieghtRealLength_mm, int width, int height)
        {
            PixelPitchX_mm = widthRealLength_mm / width;
            PixelPitchY_mm = hieghtRealLength_mm / height;
        }


        // IKROI
        /// <summary>
        /// 지정한 영역(ROI)의 정보를 가져온다.
        /// </summary>
        /// <param name="region">정보를 가져올 영역</param>
        /// <returns>지정한 영역의 정보</returns>
        public KRoiCV GetROIInfo(Rectangle region)
        {
            return new KRoiCV(cvMat, region);
        }

        // IKFourierTransform
        /// <summary>
        /// 현재 이미지에 FFT를 실행할 수 있는 객체를 가져온다.
        /// </summary>
        /// <returns>FFT 객체</returns>
        public FourierTransformCV GetFFTInfo()
        {
            return new FourierTransformCV(cvMat);
        }

        // IKConvertibleToBitmap
        /// <summary>
        /// C# Bitmap 클래스로 변환한다
        /// 원본이 16비트 흑백 이미지일 경우 흑백 8비트로 변환해서 반환한다
        /// </summary>
        /// <returns>변환된 Bitmap 객체</returns>
        public Bitmap ToBitmap()
        {
            try
            {
                if (cvIImg == null)
                    throw new Exception(LangResource.ER_JIMG_cvIImg);

                switch (Depth)
                {
                    case KDepthType.Dt_16:
                        {
                            var arrSrc = KImage.Convert3Dto1D<ushort>(data16);

                            {   // WndLv적용되지 않았을때의 루프
                                var needWndLv = from val in arrSrc
                                                where val > byte.MaxValue
                                                select val;

                                bool needToNormalize = needWndLv.Count() > 0;
                                if (needToNormalize)
                                {
                                    Image<Gray, ushort> usImg = new Image<Gray, ushort>(data16);
                                    return usImg.ToBitmap();
                                }
                            }

                            {   // WndLv적용됐을때의 루프
                                Image<Gray, ushort> depImg = new Image<Gray, ushort>(Width, Height)
                                {
                                    Data = data16
                                };

                                return depImg.ToBitmap();
                            }
                        }

                    case KDepthType.Dt_8:
                    case KDepthType.Dt_24:
                        return cvIImg.Bitmap.Clone() as Bitmap;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        // IDisposable
        /// <summary>
        /// 사용된 리소스를 해제한다
        /// </summary>
        public void Dispose()
        {
            cvIImg?.Dispose();
            cvMat?.Dispose();
            cvMatLoadByDepth?.Dispose();
            cvMatLoadByColor?.Dispose();

            Format = KImageFormat.None;
            Depth = KDepthType.None;
            Color = KColorType.Gray;

            histoGray = histoB = histoG = histoR = null;
            Width = Height = NumberOfChannels = 0;
            Path = null;

            if (data8 != null)
            {
                Array.Clear(data8, 0, data8.Length);
                data8 = null;
            }
            if (data16 != null)
            {
                Array.Clear(data16, 0, data16.Length);
                data16 = null;
            }
        }


        /// <summary>
        /// data8 또는 data16 배열에 EmguCV Mat객체의 픽셀 데이터를 복사한다.
        /// </summary>
        /// <typeparam name="TColor">칼라 형식. Image로 변환할때 사용</typeparam>
        /// <typeparam name="TDepth">Depth 형식. Image로 변환할때 사용</typeparam>
        private void DataCopyFromCVImgs<TColor, TDepth>()
            where TColor : struct, IColor
            where TDepth : IConvertible, new()
        {
            var img = cvIImg as Image<TColor, TDepth>;

            int h = img.Data.GetLength(0),
                w = img.Data.GetLength(1),
                c = img.Data.GetLength(2);

            switch (Depth)
            {
                case KDepthType.Dt_8:
                case KDepthType.Dt_24:
                    data8 = new byte[h, w, c];
                    Array.Copy(img.Data, data8, h * w * c);
                    break;
                case KDepthType.Dt_16:
                    data16 = new ushort[h, w, c];
                    Array.Copy(img.Data, data16, h * w * c);
                    break;
            }
        }

        /// <summary>
        /// 객체를 초기화 한다.
        /// </summary>
        private void Initialize()
        {
            Dispose();
        }

        /// <summary>
        /// Raw파일 생성 Form이 닫힐때 호출. Raw파일을 이미지로 변환한다.
        /// </summary>
        /// <param name="sender">sender 객체(Raw Form)</param>
        /// <param name="e">event 객체</param>
        private void RawFrm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            var frm = sender as RawImageConvertForm;
            this.Depth = frm.Depth;
            this.NumberOfChannels = frm.NumberOfChannels;
            this.cvMat = frm.CvMat;

            switch (this.Depth)
            {
                case KDepthType.Dt_8:
                    Color = KColorType.Gray;
                    cvIImg = cvMat.ToImage<Gray, byte>();
                    DataCopyFromCVImgs<Gray, byte>();
                    break;

                case KDepthType.Dt_16:
                    Color = KColorType.Gray;
                    cvIImg = cvMat.ToImage<Gray, ushort>();
                    DataCopyFromCVImgs<Gray, ushort>();
                    break;

                case KDepthType.Dt_24:
                    Color = KColorType.Color;
                    cvIImg = cvMat.ToImage<Bgr, byte>();
                    DataCopyFromCVImgs<Bgr, byte>();
                    break;

                case KDepthType.None:
                default:
                    return;
            }
            Width = cvMat.Width;
            Height = cvMat.Height;
        }
    }
}