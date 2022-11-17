using Matrox.MatroxImagingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMatrox
{
    public class JMdisp : IDisposable
    {
        private MIL_ID _mSys = MIL.M_NULL;
        private MIL_ID _mDisp = MIL.M_NULL;
        private MIL_ID _mImg = MIL.M_NULL;
        private IntPtr _userWndHandle;
        private int _width = -1;
        private int _height = -1;
        private int _colorBandSize = -1;


        public JMdisp(int width, int height, int colorBandSize, IntPtr userWndHandle)
        {
            _width = width;
            _height = height;
            _colorBandSize = colorBandSize;
            _userWndHandle = userWndHandle;

            _mSys = JMapp.Inst.MilSys;

            long attr = MIL.M_IMAGE + MIL.M_DISP;
            MIL.MdispAlloc(_mSys, MIL.M_DEFAULT, "M_DEFAULT", MIL.M_DEFAULT, ref _mDisp);
            MIL.MbufAllocColor(_mSys, _colorBandSize, _width, _height, 8 + MIL.M_UNSIGNED, attr, ref _mImg);
            MIL.MbufClear(_mImg, 0);

            MIL.MdispSelectWindow(_mDisp, _mImg, _userWndHandle);
        }


        public void SetDisplay(IntPtr userWindowHandle)
        {
            MIL.MdispSelectWindow(_mDisp, _mImg, userWindowHandle);
        }


        public void Dispose()
        {
            if (_mDisp != MIL.M_NULL)
                MIL.MdispSelect(_mDisp, MIL.M_NULL);
            if (_mImg != MIL.M_NULL)
                MIL.MbufFree(_mImg);
            if (_mDisp != MIL.M_NULL)
                MIL.MdispFree(_mDisp);
        }
    }
}
