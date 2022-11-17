using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    /// <summary>
    /// Raw파일을 위한 클래스
    /// </summary>
    public class KRaw
    {
        private int _width;
        private int _height;
        private KDepthType _depth = KDepthType.None;
        private KColorType _color = KColorType.Gray;
        private dynamic[] _data;

        /// <summary>
        /// 이미지의 가로길이
        /// </summary>
        public int Width
        {
            get { return _width; }
            private set { _width = value; }
        }

        /// <summary>
        /// 이미지의 세로길이
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
            set { _data = value; }
        }

        /// <summary>
        /// 이미지의 Depth타입을 나타낸다,
        /// 8bit: 흑백Only, 16bit: 흑백Only, 24bit: 칼라Only
        /// </summary>
        public KDepthType Depth
        {
            get { return _depth; }
            private set { _depth = value; }
        }

        /// <summary>
        /// 이미지의 색상타입 흑백또는 칼라를 나타낸다.
        /// </summary>
        public KColorType Color
        {
            get { return _color; }
            private set { _color = value; }
        }

        /// <summary>
        /// 이미지의 색상채널의 수 (흑백:1, 칼라:3 고정)
        /// </summary>
        public int NumberOfChannels
        {
            get { return (_color == KColorType.Gray) ? 1 : 3; }
        }


        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="path">파일의 경로</param>
        /// <param name="width">가로 길이</param>
        /// <param name="height">세로 길이</param>
        /// <param name="depth">Depth(bit)</param>
        /// <param name="color">색상 타입</param>
        public KRaw(string path, int width, int height, KDepthType depth, KColorType color)
            : this(width, height, depth, color)
        {
            this.Load(path, width, height, depth, color);
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="width">가로 길이</param>
        /// <param name="height">세로 길이</param>
        /// <param name="depth">Depth(bit)</param>
        /// <param name="color">색상 타입</param>
        public KRaw(int width, int height, KDepthType depth, KColorType color)
        {
            if (depth == KDepthType.None)
                throw new Exception("KRaw - Depth는 None일수 없습니다");

            this.Width = width;
            this.Height = height;
            this._depth = depth;
            this._color = color;

            this._data = new dynamic[width * height * NumberOfChannels];
        }


        /// <summary>
        /// 파일을 저장한다
        /// </summary>
        /// <param name="path">파일을 저장할 경로</param>
        public void Save(string path)
        {
            var ext = Path.GetExtension(path);

            if (!ext.Equals(".raw", StringComparison.OrdinalIgnoreCase))
                throw new FormatException("KRaw - Save(..): path의 확장자가 .raw가 아닙니다");

            using (var outputStream = File.Open(path, FileMode.Create))
            using (var writer = new BinaryWriter(outputStream))
            {
                int len = _data.Length;

                for (int i = 0; i < len; i++)
                    writer.Write(_data[i]);
            }
        }

        /// <summary>
        /// 파일을 불러온다
        /// </summary>
        /// <param name="path">파일을 불러올 경로</param>
        public void Load(string path)
        {
            Load(path, this._width, this._height, this._depth, this._color);
        }

        /// <summary>
        /// 파일을 불러온다
        /// </summary>
        /// <param name="path">파일을 불러올 경로</param>
        /// <param name="width">가로 길이</param>
        /// <param name="height">세로 길이</param>
        /// <param name="depth">Depth(bit)</param>
        /// <param name="color">색상 타입</param>
        public void Load(string path, int width, int height, KDepthType depth, KColorType color)
        {
            int index = 0;

            using (var inputStream = File.Open(path, FileMode.Open))
            using (var reader = new BinaryReader(inputStream))
            {
                this._data = new dynamic[width * height * NumberOfChannels];

                if (depth == KDepthType.Dt_8 ||
                    depth == KDepthType.Dt_24)
                {
                    while (inputStream.Position < inputStream.Length)
                        _data[index++] = reader.ReadByte();

                }
                else //depth == KDepthType.Dt_16
                {
                    while (inputStream.Position < inputStream.Length)
                        _data[index++] = reader.ReadUInt16();
                }
            }
        }
    }
}