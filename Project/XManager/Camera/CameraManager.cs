using CameraInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XManager.Forms;
using static CameraInterface.ICamera;

namespace XManager.Camera
{
    public class CameraManager
    {
        private ICamera handle = null;
        public bool CreateInstance()
        {
            eCameraType type = CStatus.Instance().Settings.CameraType;
            string startupPath = Application.StartupPath;
            bool ret = false;
            switch (type)
            {
                case eCameraType.NONE:
                    break;
                case eCameraType.ViewWorks_FXDD_0606CA:
                    Assembly assem = Assembly.LoadFile(Path.Combine(startupPath, "ViewWorks_FXDD_0606CA.dll"));
                    Type targetType = assem.GetType("ViewWorks_FXDD_0606CA.FXDD_0606CA");
                    handle = (ICamera)Activator.CreateInstance(targetType);
                    ret = true;
                    break;
                case eCameraType.Thales_2121S:
                    break;
                case eCameraType.Basler_acA1300_30gm:
                    break;
                case eCameraType.Basler_acA1300_60gm:
                    break;
                case eCameraType.Basler_acA2040_25gmNIR:
                    break;
                default:
                    break;
            }
            return ret;
        }

        public bool Initializate()
        {
            bool ret = false;
            ret = CreateInstance();

            if (!ret)
                return ret;
            ret = handle.Initializate();

            return ret;
        }

        public void RegisterCallBack(SendDisplayBufferDele callbackFunc)
        {
            if (this.handle == null)
                return;
            handle.callbackFunc = callbackFunc;
        }
    

        public void StartContinuousGrap()
        {
            if (this.handle == null)
                return;
            this.handle.StartContinuousGrap();
        }

        public void StopGrap()
        {
            if (this.handle == null)
                return;
            this.handle.StopGrap();
        }

        public void Close()
        {
            if (this.handle == null)
                return;
            this.handle.Close();
        }
    }
}
