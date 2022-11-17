using CameraInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vieworks;
using Vieworks.vivixt;

namespace ViewWorks_FXDD_0606CA
{
    public class FXDD_0606CA : ICamera
    {
        private VWSDKnet vwSDK;
        private int width = 1280;
        private int height = 1280;
        private int detectorId;
        private List<string> deviceList;
        private List<string> driveModeList;
        private int selectedDeviceIndex = -1;
        private int selectedDriveModeIndex = -1;
        
        #region ICamera Interfaces
        public delegate void CallbackDelegate(string status,int Test);
        public override bool Initializate()
        {
            base.BufferInitializate(width, height, 16);

            vwSDK = new VWSDKnet();

            detectorId = -1;
            deviceList = new List<string>();
            driveModeList = new List<string>();

            RESULT res;
            List<DEVICE_DISCOVERY_INFO> DiscoveryInfoList = null;

            vwSDK.InitializeSDK();
            res = vwSDK.DiscoveryDevice(out DiscoveryInfoList);
            selectedDeviceIndex = DiscoveryInfoList.Count - 1;

            if (res != RESULT.RESULT_SUCCESS)
                return false;

            Open();

            if (driveModeList.Count > 0 || selectedDriveModeIndex != -1)
                return true;
            else
                return false;
        }

        public override string Close()
        {
            try
            {
                if (vwSDK.IsGrabbing(detectorId))
                {
                    vwSDK.AcquisitionStop(detectorId);
                }

                if (vwSDK.IsOpen(detectorId))
                {
                    vwSDK.CloseDetector(detectorId);
                }

                vwSDK.FinalizeSDK();

                return "Success";
            }
            catch (Exception)
            {
                return vwSDK.GetLastErrorMsg();
            }
        }

        public override ushort[] GetDisplayCalc16bitRawData()
        {
            throw new NotImplementedException();
        }

        public override string GetParamValue(object param)
        {
            throw new NotImplementedException();
        }

        public override void SetParam(object param)
        {
            throw new NotImplementedException();
        }

        public override void ShowParamForm()
        {
            throw new NotImplementedException();
        }

        public override string StartContinuousGrap()
        {
            try
            {
                RESULT res;
                res = vwSDK.AcquisitionStart(detectorId);

                if (res != RESULT.RESULT_SUCCESS)
                {
                    return vwSDK.GetLastErrorMsg();
                }

                return "success";
            }
            catch (Exception)
            {
                return vwSDK.GetLastErrorMsg();
            }
        }

        public override string StopGrap()
        {
            try
            {
                RESULT res;
                res = vwSDK.AcquisitionStop(detectorId);

                if (res != RESULT.RESULT_SUCCESS)
                {
                    return vwSDK.GetLastErrorMsg();
                }
                return "success";
            }
            catch (Exception)
            {
                return vwSDK.GetLastErrorMsg();
            }
        }

        public override void TestAvg(byte[] buffer16Bit)
        {
            throw new NotImplementedException();
        }

        public override int GetOrgWidth()
        {
            return width;
        }

        public override int GetOrgHeight()
        {
            return height;
        }

        #endregion

        public void Open()
        {
            try
            {
                RESULT res;
                res = vwSDK.OpenDetector(selectedDeviceIndex, out detectorId);
                if (res != RESULT.RESULT_SUCCESS)
                {
                    string message = vwSDK.GetLastErrorMsg();
                    return;
                }

                res = vwSDK.SetDelegateImageInEx(detectorId, DelegateCallbackImageIn);
                if (res != RESULT.RESULT_SUCCESS)
                {
                    string message = vwSDK.GetLastErrorMsg();
                    return;
                }

                res = vwSDK.SetDelegateNotification(detectorId, DelegateCallbackNotification);
                if (res != RESULT.RESULT_SUCCESS)
                {
                    string message = vwSDK.GetLastErrorMsg();
                    return;
                }

                // Get Drive mode info
                List<string> DriveModeNameList;
                res = vwSDK.GetDriveModeNameList(detectorId, out DriveModeNameList);
                if (res == RESULT.RESULT_SUCCESS)
                {
                    for (int i = 0; i < DriveModeNameList.Count; i++)
                    {
                        driveModeList.Add(string.Format("{0} - {1}", i, DriveModeNameList[i]));
                    }
                }

                // Select current drive mode
                int nDriveMode;
                res = vwSDK.GetDriveMode(detectorId, out nDriveMode);
                if (res == RESULT.RESULT_SUCCESS)
                {
                    selectedDriveModeIndex = nDriveMode;
                }
            }
            catch (Exception)
            {
                string message = vwSDK.GetLastErrorMsg();
            }
        }

        public int GetDriveMode()
        {
            int nDriveMode;
            RESULT res;

            res = vwSDK.GetDriveMode(detectorId, out nDriveMode);
            if (res == RESULT.RESULT_SUCCESS)
                return nDriveMode;
            else
                return -1;
        }

        public bool SetDriveMode(int index)
        {
            RESULT res;
            res = vwSDK.SetDriveMode(detectorId, selectedDriveModeIndex);

            if (res != RESULT.RESULT_SUCCESS)
                return false;
            else
                return true;
        }


        public void DelegateCallbackImageIn(IMAGE_INFO_EX image_info)
        {
            int imgArrLength = image_info.pImage.Length;

            Array.Clear(base.rstBuffer, 0, base.rstBuffer.Length);
            Array.Clear(base.avgResultArr, 0, base.avgResultArr.Length);

            image_info.pImage.CopyTo(base.tempBuffer, 0);

            // Check image error
            if (image_info.callbackResult != RESULT.RESULT_SUCCESS)
            {
                string strValue = "Image error : " + vwSDK.GetLastErrorMsg();
                return;
            }

            base.CalcAvgBuffer(tempBuffer, this.width, this.height, 16);
        }

        public void DelegateCallbackNotification(NOTIFICATION_DATA noti_data)
        {
            switch (noti_data.Type)
            {
                case NOTIFICATION_TYPE.NOTIFY_CONNECT_STATUS:
                    if (noti_data.ConnectStatus.ConnectionNotify == NOTIFICATION_CONNECTION.NOTIFY_DISCONNECT)
                    {
                        DetectorDisconnected();
                    }
                    break;
            }
        }

        public void DetectorDisconnected()
        {
            if (vwSDK.IsGrabbing(detectorId))
            {
                vwSDK.AcquisitionStop(detectorId);
                vwSDK.CloseDetector(detectorId);
            }
        }
    }
}
