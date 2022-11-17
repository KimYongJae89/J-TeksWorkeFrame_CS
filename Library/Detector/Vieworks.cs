using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vieworks;
using Vieworks.vivixt;

namespace Detector
{
    public class Vieworks : IDetectorBasic
    {
        private VWSDKnet _vwSDK;
        private int _width = -1;
        private int _height = -1;
        private int detectorID = -1;
        private List<string> _deviceList;
        private List<string> _driveModeList;
        private int selectedDeviceIdx = -1;
        private int selectedDriveModeIdx = -1;

        //private byte[] rstBuffer;       //Avg처리된 16bit에서 8bit로 변환한후 저장되는, 최종 이미지 버퍼
        //private ushort[] tempBuffer;    //image_info.pImage 카피

        //private bool resetAvg;
        //private int avgCurCnt = 0;
        //private int[] avgSumArr;        //Avg를 위해 영상 합산한 결과 임시저장
        //private int[] avgResultArr;     //Avg합산된 이미지를 계산하여 최종 Avg이미지 배열 저장

        public int Width { get { return _width; } set { _width = value; } }
        public int Height { get { return _height; } set { _height = value; } }
        public List<string> DeviceList { get { return _deviceList; } private set { _deviceList = value; } }
        public List<string> DriveModeList { get { return _driveModeList; } private set { _driveModeList = value; } }
        public event Action<ushort[]> CallBackGrab;
        public event Action DetectorDisconnected;


        public Vieworks(int width, int height)
        {
            _width = width;
            _height = width;
        }


        public bool Initialize()
        {
            _vwSDK = new VWSDKnet();

            DeviceList = new List<string>();
            DriveModeList = new List<string>();

            //rstBuffer = new byte[_width * _height];
            //tempBuffer = new ushort[_width * _height];
            //avgSumArr = new int[_width * _height];
            //avgResultArr = new int[_width * _height];

            _vwSDK.InitializeSDK();

            List<DEVICE_DISCOVERY_INFO> DiscoveryInfoList = null;
            RESULT res;
            res = _vwSDK.DiscoveryDevice(out DiscoveryInfoList);
            selectedDeviceIdx = DiscoveryInfoList.Count - 1;

            if (res != RESULT.RESULT_SUCCESS)
                return false;

            for (int i = 0; i < DiscoveryInfoList.Count; i++)
            {
                DeviceList.Add(string.Format("{0} : {1}", i, DiscoveryInfoList[i].model));
            }

            Open();

            if (DriveModeList.Count > 0 || selectedDriveModeIdx != -1)
                return true;
            else
                return false;
        }

        public void Open()
        {
            RESULT res;

            res = _vwSDK.OpenDetector(selectedDeviceIdx, out detectorID);
            if (res != RESULT.RESULT_SUCCESS)
                throw new Exception($"VIVIX_ERR_[DetectorOpen()]: {_vwSDK.GetLastErrorMsg()}");

            res = _vwSDK.SetDelegateImageInEx(detectorID, DelegateCallbackImageIn);
            if (res != RESULT.RESULT_SUCCESS)
                throw new Exception($"VIVIX_ERR_[DetectorOpen()]: {_vwSDK.GetLastErrorMsg()}");

            res = _vwSDK.SetDelegateNotification(detectorID, DelegateCallbackNotification);
            if (res != RESULT.RESULT_SUCCESS)
                throw new Exception($"VIVIX_ERR_[DetectorOpen()]: {_vwSDK.GetLastErrorMsg()}");

            // Get Drive mode info
            List<string> DriveModeNameList;
            res = _vwSDK.GetDriveModeNameList(detectorID, out DriveModeNameList);
            if (res == RESULT.RESULT_SUCCESS)
            {
                for (int i = 0; i < DriveModeNameList.Count; i++)
                {
                    DriveModeList.Add(string.Format("{0} - {1}", i, DriveModeNameList[i]));
                }
            }

            // Select current drive mode
            int driveMode;
            res = _vwSDK.GetDriveMode(detectorID, out driveMode);
            if (res == RESULT.RESULT_SUCCESS)
            {
                selectedDriveModeIdx = driveMode;
            }
        }

        public void Close()
        {
            if (_vwSDK.IsGrabbing(detectorID))
            {
                _vwSDK.AcquisitionStop(detectorID);
            }

            if (_vwSDK.IsOpen(detectorID))
            {
                _vwSDK.CloseDetector(detectorID);
            }

            _vwSDK.FinalizeSDK();
        }

        public void AcquisitionStart()
        {
            RESULT res;
            res = _vwSDK.AcquisitionStart(detectorID);

            if (res != RESULT.RESULT_SUCCESS)
                throw new Exception($"VIVIX_ERR_[AcquisitionStart()]: {_vwSDK.GetLastErrorMsg()}");
        }

        public void AcquisitionStop()
        {
            RESULT res;
            res = _vwSDK.AcquisitionStop(detectorID);

            if (res != RESULT.RESULT_SUCCESS)
                throw new Exception($"VIVIX_ERR_[AcquisitionClose()]: {_vwSDK.GetLastErrorMsg()}");
        }

        public bool SelectDriveMode(int index)
        {
            RESULT res;
            res = _vwSDK.SetDriveMode(detectorID, index);

            if (res != RESULT.RESULT_SUCCESS)
                return false;
            else
                return true;
        }


        // CallBack Event
        private void DelegateCallbackImageIn(IMAGE_INFO_EX image_info)
        {
            // Check image error
            if (image_info.callbackResult != RESULT.RESULT_SUCCESS)
            {
                string strValue = "Image error : " + _vwSDK.GetLastErrorMsg();
                return;
            }

            CallBackGrab?.Invoke(image_info.pImage);
        }

        private void DelegateCallbackNotification(NOTIFICATION_DATA noti_data)
        {
            switch (noti_data.Type)
            {
                case NOTIFICATION_TYPE.NOTIFY_CONNECT_STATUS:
                    if (noti_data.ConnectStatus.ConnectionNotify == NOTIFICATION_CONNECTION.NOTIFY_DISCONNECT)
                    {
                        DetectorDisconnected?.Invoke();
                    }
                    break;
            }
        }
    }
}


//private BackgroundWorker _workerWaitCall;
//private int m_nDetectorId = -1;

//public VWSDKnet VwSDK;
//public event Action<IMAGE_INFO_EX> CallBackGrab;
//public event Action DetectorDisconnected;


//public Vieworks()
//{
//    VwSDK = new VWSDKnet();
//}


//public void DetectorOpen(int deviceIdx = 0)
//{
//    if (m_nDetectorId >= 0)
//        return;

//    RESULT res;

//    res = VwSDK.OpenDetector(deviceIdx, out m_nDetectorId);
//    if (res != RESULT.RESULT_SUCCESS)
//        throw new Exception($"VIVIX_ERR_[DetectorOpen()]: {VwSDK.GetLastErrorMsg()}");

//    res = VwSDK.SetDelegateImageInEx(m_nDetectorId, DelegateCallbackImageIn);
//    if (res != RESULT.RESULT_SUCCESS)
//        throw new Exception($"VIVIX_ERR_[DetectorOpen()]: {VwSDK.GetLastErrorMsg()}");

//    res = VwSDK.SetDelegateNotification(m_nDetectorId, DelegateCallbackNotification);
//    if (res != RESULT.RESULT_SUCCESS)
//        throw new Exception($"VIVIX_ERR_[DetectorOpen()]: {VwSDK.GetLastErrorMsg()}");

//    // Get Drive mode info
//    /*comboDriveMode.Items.Clear();
//    List<string> DriveModeNameList;
//    res = VwSDK.GetDriveModeNameList(m_nDetectorId, out DriveModeNameList);
//    if (res == RESULT.RESULT_SUCCESS)
//    {
//        for (int i = 0; i < DriveModeNameList.Count; i++)
//        {
//            comboDriveMode.Items.Add(string.Format("{0} - {1}", i, DriveModeNameList[i]));
//        }
//    }*/

//    // Select current drive mode
//    /*int nDriveMode;
//    res = VwSDK.GetDriveMode(m_nDetectorId, out nDriveMode);
//    if (res == RESULT.RESULT_SUCCESS)
//    {
//        comboDriveMode.SelectedIndex = nDriveMode;
//    }

//    UpdateInformation();

//    EnableControl(STATE.OPEN);*/
//}

//public void DetectorClose()
//{
//    RESULT res;
//    res = VwSDK.CloseDetector(m_nDetectorId);
//    //             if (res != RESULT.RESULT_SUCCESS)
//    //             {
//    //                 MessageBox.Show(VwSDK.GetLastErrorMsg());
//    //                 return;
//    //             }

//    m_nDetectorId = -1;

//    Discovery();
//}

//public void AcquisitionStart()
//{
//    RESULT res;
//    res = VwSDK.AcquisitionStart(m_nDetectorId);

//    if (res != RESULT.RESULT_SUCCESS)
//        throw new Exception($"VIVIX_ERR_[AcquisitionStart()]: {VwSDK.GetLastErrorMsg()}");
//}

//public void AcquisitionClose()
//{
//    RESULT res;
//    res = VwSDK.AcquisitionStop(m_nDetectorId);

//    if (res != RESULT.RESULT_SUCCESS)
//        throw new Exception($"VIVIX_ERR_[AcquisitionClose()]: {VwSDK.GetLastErrorMsg()}");
//}

//public void Discovery()
//{
//    VwSDK.InitializeSDK();

//    string strLogDir = Directory.GetCurrentDirectory() + "\\Log";

//    Directory.CreateDirectory(strLogDir);

//    VwSDK.LogOption(strLogDir, "Sample", 30);
//    RESULT res;
//    List<DEVICE_DISCOVERY_INFO> DiscoveryInfoList = null;

//    comboBoxDevice.Items.Clear();
//    res = VwSDK.DiscoveryDevice(out DiscoveryInfoList);

//    if (res != RESULT.RESULT_SUCCESS)
//    {
//        throw new Exception($"VIVIX_ERR_[Discovery()]: {VwSDK.GetLastErrorMsg()}");
//    }

//    for (int i = 0; i < DiscoveryInfoList.Count; i++)
//    {
//        comboBoxDevice.Items.Add(string.Format("{0} : {1}", i, DiscoveryInfoList[i].model));
//    }

//    if (DiscoveryInfoList.Count > 0)
//    {
//        comboBoxDevice.SelectedIndex = 0;
//    }

//    EnableControl(DiscoveryInfoList.Count > 0 ? STATE.DISCOVERY : STATE.INIT);
//}


//private void DelegateCallbackImageIn(IMAGE_INFO_EX image_info)
//{
//    // Check image error
//    if (image_info.callbackResult != RESULT.RESULT_SUCCESS)
//    {
//        string strValue = "Image error : " + VwSDK.GetLastErrorMsg();
//        return;
//    }

//    CallBackGrab?.Invoke(image_info);
//}

//private void DelegateCallbackNotification(NOTIFICATION_DATA noti_data)
//{
//    string strValue = "";
//    switch (noti_data.Type)
//    {
//        case NOTIFICATION_TYPE.NOTIFY_CONNECT_STATUS:
//            {
//                if (noti_data.ConnectStatus.ConnectionNotify == NOTIFICATION_CONNECTION.NOTIFY_DISCONNECT)
//                {
//                    DetectorDisconnected?.Invoke();
//                    //DeleUiUpdate delDisconnect = new DeleUiUpdate(DetectorDisconnected);
//                    //this.Invoke(delDisconnect);
//                }
//                strValue = noti_data.ConnectStatus.ConnectionNotify.ToString();
//            }
//            break;
//        case NOTIFICATION_TYPE.NOTIFY_ACQUISITION_STATUS:
//            {
//                strValue = noti_data.AcquisitionStatus.Notify.ToString();
//            }
//            break;

//        case NOTIFICATION_TYPE.NOTIFY_BATTERY_WARNING_STATUS:
//            {
//                strValue = noti_data.BatteryStatus.eBatteryLevel.ToString();
//            }
//            break;

//        case NOTIFICATION_TYPE.NOTIFY_SLEEP_STATUS:
//            {
//                strValue = noti_data.ePowerMode.ToString();
//            }
//            break;

//        case NOTIFICATION_TYPE.NOTIFY_BATTERY_EQUIPMENT_STATUS:
//            {
//                strValue = string.Format("Battery {0}: Equipped: {1}, Charging: {2}", 
//                    noti_data.BatteryEquipmentStatus.nBatteryNo.ToString(), 
//                    noti_data.BatteryEquipmentStatus.bEquipped, 
//                    noti_data.BatteryEquipmentStatus.bCharging);
//            }
//            break;

//        default:
//            break;
//    }

//   /* if (listNotification.InvokeRequired)
//    {
//        listNotification.Invoke(new MethodInvoker(delegate ()
//        {
//            ListViewItem lvi = new ListViewItem(noti_data.Type.ToString());
//            lvi.SubItems.Add(strValue);
//            listNotification.Items.Add(lvi);
//        }));
//    }
//    else
//    {
//        ListViewItem lvi = new ListViewItem(noti_data.Type.ToString());
//        lvi.SubItems.Add(strValue);
//        listNotification.Items.Add(lvi);
//    }*/
//}