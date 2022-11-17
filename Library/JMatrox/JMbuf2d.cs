using KiyLib.General;
using Matrox.MatroxImagingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMatrox
{
    public class JMbuf2d : IDisposable, ICloneable
    {
        private int _width = -1;
        private int _height = -1;
        private long _attribute;
        private MIL_ID _mBuf = MIL.M_NULL;
        private MIL_INT _type;

        public int Width
        {
            get { return _width; }
            private set { _width = value; }
        }
        public int Height
        {
            get { return _height; }
            private set { _height = value; }
        }
        public int NumberOfPixels
        {
            get { return _width * _height; }
        }


        /// <summary>
        /// JMbuf2d 객체를 생성한다
        /// </summary>
        /// <param name="width">가로</param>
        /// <param name="height">세로</param>
        /// <param name="bitOfdataType">픽셀의 Depth [1,8,16,32]</param>
        public JMbuf2d(int width, int height, int depth)
            : this(width, height, depth + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC) { }

        /// <summary>
        /// JMbuf2d 객체를 생성한다
        /// </summary>
        /// <param name="width">가로</param>
        /// <param name="height">세로</param>
        /// <param name="type">픽셀의 Depth  + Type [ex) 16 + M_UNSIGNED, 8 + M_FLOAT]</param>
        /// <param name="attribute">버퍼의 용도 [ex) MIL.M_IMAGE + MIL.M_PROC]</param>
        public JMbuf2d(int width, int height, MIL_INT type, long attribute)
        {
            _width = width;
            _height = height;
            _type = type;
            _attribute = attribute;

            Initialize();
        }

        /// <summary>
        /// JMbuf2d 객체를 생성한다
        /// </summary>
        /// <param name="buf2d">복사할 버퍼</param>
        public JMbuf2d(MIL_ID buf2d)
        {
            MIL.MbufInquire(buf2d, MIL.M_SIZE_X, ref _width);
            MIL.MbufInquire(buf2d, MIL.M_SIZE_Y, ref _height);
            MIL.MbufInquire(buf2d, MIL.M_TYPE, ref _type);
            MIL.MbufInquire(buf2d, MIL.M_EXTENDED_ATTRIBUTE, ref _attribute);

            Initialize();

            MIL.MbufCopy(buf2d, _mBuf);
        }


        /// <summary>
        /// 버퍼를 초기화 한다
        /// </summary>
        private void Initialize()
        {
            var sysID = JMapp.Inst.MilSys;

            MIL.MbufAlloc2d(sysID, Width, Height, _type, _attribute, ref _mBuf); ;
        }


        /// <summary>
        /// 버퍼에 데이터를 입력한다
        /// </summary>
        /// <param name="data">입력할 데이터</param>
        public void Put(byte[] data)
        {
            MIL.MbufPut(_mBuf, data);
        }

        /// <summary>
        /// 버퍼에 데이터를 입력한다
        /// </summary>
        /// <param name="data">입력할 데이터</param>
        public void Put(ushort[] data)
        {
            MIL.MbufPut(_mBuf, data);
        }

        /// <summary>
        /// 버퍼에 데이터를 입력한다
        /// </summary>
        /// <param name="data">입력할 데이터</param>
        public void Put(float[] data)
        {
            MIL.MbufPut(_mBuf, data);
        }

        /// <summary>
        /// 버퍼에 데이터를 입력한다
        /// </summary>
        /// <param name="data">입력할 데이터</param>
        public void Put(int[] data)
        {
            MIL.MbufPut(_mBuf, data);
        }

        /// <summary>
        /// 데이터를 가져온다
        /// </summary>
        /// <param name="dstArr">가져온 데이터를 저장할 배열</param>
        public void Get(byte[] dstArr)
        {
            MIL.MbufGet(_mBuf, dstArr);
        }

        /// <summary>
        /// 데이터를 가져온다
        /// </summary>
        /// <param name="dstArr">가져온 데이터를 저장할 배열</param>
        public void Get(ushort[] dstArr)
        {
            MIL.MbufGet(_mBuf, dstArr);
        }

        /// <summary>
        /// 데이터를 가져온다
        /// </summary>
        /// <param name="dstArr">가져온 데이터를 저장할 배열</param>
        public void Get(float[] dstArr)
        {
            MIL.MbufGet(_mBuf, dstArr);
        }

        /// <summary>
        /// 데이터를 가져온다
        /// </summary>
        /// <param name="dstArr">가져온 데이터를 저장할 배열</param>
        public void Get(int[] dstArr)
        {
            MIL.MbufGet(_mBuf, dstArr);
        }

        public JMbuf2d WndLvlTranform(int srcStart, int srcEnd, int dstStart, int dstEnd)
        {
            JMbuf2d rtBuf = new JMbuf2d(_mBuf);
            MIL_ID lut = MIL.M_NULL;

            MIL.MbufAlloc1d(JMapp.Inst.MilSys, NumberOfPixels, _type, MIL.M_LUT, ref lut); ;

            MIL.MgenLutRamp(lut, 0, 0, srcStart, 0);
            MIL.MgenLutRamp(lut, srcStart, dstStart, srcEnd, dstEnd);
            MIL.MgenLutRamp(lut, srcEnd, dstEnd, 65535, dstEnd);

            MIL.MimLutMap(_mBuf, rtBuf._mBuf, lut);

            MIL.MbufFree(lut);

            return rtBuf;
        }


        public object Clone()
        {
            return new JMbuf2d(_mBuf);
        }

        public void Dispose()
        {
            if (_mBuf != MIL.M_NULL)
                MIL.MbufFree(_mBuf);
        }
    }
}
