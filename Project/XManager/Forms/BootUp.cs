using CameraInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XManager.Camera;

namespace XManager.Forms
{
    public partial class BootUp : Form
    {
        private string _message = "";
        public BootUp()
        {
            InitializeComponent();
            CStatus.Instance().BootUpTextUpdateHandler += new CStatus.BootUpTextUpdateDelegate(UroBootUp_BootUpTextUpdateHandler);
            CStatus.Instance().BootUpTimerStartHandler += new CStatus.BootUpTimerStartDelegate(BootUpTimerStart);
            CStatus.Instance().BootUpTimerStopHandler += new CStatus.BootUpTimerStopDelegate(BootUpTimerStop);
            CStatus.Instance().BootUpCloseFormHandler += new CStatus.BootUpCloseFormDelegate(BootUpFormClose);
        }

        private void UroBootUp_Load(object sender, EventArgs e)
        {//bootup 없앨때
            try
            {
                //CStatus.Instance().GetConfig();
                CStatus.Instance().Settings.Load();
                //if(CStatus.Instance().LiveCenter == 0)
                //{
                //    CStatus.Instance().LiveCenter = CStatus.Instance().Administrator_LiveCenter;
                //}
                //if(CStatus.Instance().LiveWidth == 0)
                //{
                //    CStatus.Instance().LiveWidth = CStatus.Instance().Administrator_LiveWidth;
                //}

                if (CStatus.Instance().Settings.CameraType == eCameraType.NONE)
                {
                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm.Text == "Settings")
                            return;
                    }
                    MessageBox.Show("Camera not selected.\nPlease select camera.");
                    SetUpForm form = new SetUpForm();
                    form.ShowDialog();

                }

                CStatus.Instance().ErrorLogManager.ErrorLogInitializate();
                CStatus.Instance().ErrorLogManager.WriteErrorLog(MethodBase.GetCurrentMethod().Name, "Start");

                //bootup 없앨때
                //if (!CStatus.Instance().IsTestMode)
                //{
                Thread starter = new Thread(new ThreadStart(ReceiveThread));
                starter.Start();
                //}
                //else
                //{
                // bool result = CStatus.Instance().CameraManager.Initializate();
                // bool result = CStatus.ROTATION_TYPE
                // CallFormClose();
                //}
            }
            catch (Exception err)
            {
                CStatus.Instance().ErrorLogManager.WriteErrorLog(MethodBase.GetCurrentMethod().Name, err.Message);
            }
        }


        private void ReceiveThread()
        {
            // config 값 읽어오기
           // CStatus.Instance().GetConfig();
            if(CStatus.Instance().Settings.CameraType == eCameraType.NONE)
            {
                
            }
            bool result = CStatus.Instance().CameraManager.Initializate();

            if (result)
            {
                Thread.Sleep(10);
                CallFormClose();
            }
            BootUpTimerStop();


            #region 구조 변경 전
            //string ip = CStatus.Instance().PU_ByteIpAddress[0].ToString() + "." +
            //            CStatus.Instance().PU_ByteIpAddress[1].ToString() + "." +
            //            CStatus.Instance().PU_ByteIpAddress[2].ToString() + "." +
            //            CStatus.Instance().PU_ByteIpAddress[3].ToString();

            //bool isSuccess = CStatus.Instance().CameraManager.TCP_Initialize(ip, Convert.ToInt32(CStatus.Instance().PU_Port)); //0
            //if (isSuccess)
            //{
            //    lbl_TCPConnect.ImageIndex = 1;
            //}
            //else
            //{
            //    lbl_TCPConnect.ImageIndex = 2;
            //    MessageBox.Show("Please Check PU IP Address and Port");

            //    CallFormClose();
            //    return;
            //}

            //CStatus.Instance().BootupThreadHanlder.Reset();
            //CStatus.Instance().CameraManager.PU_Open(); //1
            //if (!CStatus.Instance().BootupThreadHanlder.WaitOne(5000))
            //{
            //    ProcessUnitAlram();
            //    lbl_PU_Open.ImageIndex = 2;
            //    Console.WriteLine("Error : PUOpen");
            //    if (this._firstEnter == false)
            //        return;
            //}
            //else
            //{
            //    lbl_PU_Open.ImageIndex = 1;
            //    Console.WriteLine("-------------------------- PU_Open Success");
            //}

            //CStatus.Instance().BootupThreadHanlder.Reset();

            //CStatus.Instance().CameraManager.startDarkCalibration();//2
            //while (CStatus.Instance().BootupThreadHanlder.WaitOne(5000))
            //{
            //    CStatus.Instance().BootupThreadHanlder.Reset();

            //    CStatus.Instance().CameraManager.startDarkCalibration();//2
            //}
            //lbl_DarkCalibration.ImageIndex = 1;
            //Console.WriteLine("-------------------------- startDarkCalibration Success");

            //CStatus.Instance().BootupThreadHanlder.Reset();
            //CStatus.Instance().IsCalbirationMode = true;
            //CStatus.Instance().CameraManager.resumeCalibration();//3
            //while (CStatus.Instance().BootupThreadHanlder.WaitOne(5000))
            //{
            //    CStatus.Instance().BootupThreadHanlder.Reset();

            //    CStatus.Instance().CameraManager.resumeCalibration();//2
            //}
            //lbl_ResumeCalibration.ImageIndex = 1;
            //Console.WriteLine("-------------------------- resumeCalibration Success");

            //CStatus.Instance().IsCalbirationMode = false;

            //CStatus.Instance().BootupThreadHanlder.Reset();
            //CStatus.Instance().CameraManager.loadReferences();//4
            //if (!CStatus.Instance().BootupThreadHanlder.WaitOne(5000))
            //{
            //    ProcessUnitAlram();
            //    lbl_LoadReferences.ImageIndex = 2;
            //    Console.WriteLine("Error : LoadReferences");
            //    if (this._firstEnter == false)
            //        return;
            //}
            //else
            //{
            //    lbl_LoadReferences.ImageIndex = 1;
            //    Console.WriteLine("-------------------------- loadReferences Success");
            //}
            //string xmlFilePath = Path.Combine(CStatus.Instance().ConfigPath, "ProcessingChain.xml");
            ////string DSAxmlFilePath = Path.Combine(CStatus.Instance().ConfigPath, "ProcessingChain(DSA).xml");

            ////string testXmlFIlePath = @"D:\Project\uro\QT\pixdyn_messages_samples\pixdyn_messages_samples\ProcessingChainWithRecordBlock.xml";
            //CStatus.Instance().MakeCreateABCXMLFile(xmlFilePath);
            ////CStatus.Instance().MakeCreateDSAXMLFile(DSAxmlFilePath);

            //CStatus.Instance().BootupThreadHanlder.Reset();

            //CStatus.Instance().AcquisitionType = ACQUISITION_TYPE.ABC;
            //CStatus.Instance().CameraManager.createProcessingChain(xmlFilePath);
            //if (!CStatus.Instance().BootupThreadHanlder.WaitOne(5000))
            //{
            //    ProcessUnitAlram();
            //    Console.WriteLine("Error : CreateProcessingChain");
            //    if (this._firstEnter == false)
            //        return;
            //}

            //CStatus.Instance().BootupThreadHanlder.Reset();
            //CStatus.Instance().CameraManager.applyProcessingChain();
            //if (!CStatus.Instance().BootupThreadHanlder.WaitOne(5000))
            //{
            //    ProcessUnitAlram();
            //    lbl_CreateProcessChain.ImageIndex = 2;
            //    Console.WriteLine("Error : ApplyProcessingChain");
            //    if (this._firstEnter == false)
            //        return;
            //}
            //else
            //{
            //    lbl_CreateProcessChain.ImageIndex = 1;
            //}

            //Thread.Sleep(10);
            //CallFormClose();
            #endregion
        }

        private delegate void callback();
        private void CallFormClose()
        {
            if (this.InvokeRequired)
            {
                callback call = CallFormClose;
                Invoke(call);
                return;
            }
            this.Close();
        }

        private bool _firstEnter = true;
        private void ProcessUnitAlram()
        {

            if (this._firstEnter == true)
            {
                MessageBox.Show("Detector connection is unstable.\n" +
                                  "If you have not set IP and Port, please restart the program after setting.\n" +
                                  "If you set IP and Port, reboot the detector.");

                this._firstEnter = false;
                CStatus.Instance().IsBootError = true;
                CallFormClose();
                return;
            }
            else
            {
                return;
            }
        }

        int enterCount = 0;
        private void labelSettings_Click(object sender, EventArgs e)
        {
            if (enterCount == 3)
            {

            }
            enterCount++;
        }

        private void UroBootUp_BootUpTextUpdateHandler(string message)
        {
            this._message = message;
            LabelUpdateBootMessage();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this._message == "")
                return;
            LabelUpdateBootMessage();

        }

        private delegate void XLabelUpdateBootMessageDelegate();
        private void LabelUpdateBootMessage()
        {
            if (this.InvokeRequired)
            {
                XLabelUpdateBootMessageDelegate callback = LabelUpdateBootMessage;
                Invoke(callback);
                return;
            }
            string dot = "";
            CStatus.Instance().BootUpDotCount++;
            int count = CStatus.Instance().BootUpDotCount % 6;
            for (int i = 0; i < count; i++)
            {
                dot += ".";
            }

            lblLoadingText.Text = this._message + " " + dot;
        }

        private void BootUpTimerStop()
        {
            timer.Stop();
        }

        private void BootUpTimerStart()
        {
            timer.Start();
        }

        private void BootUpFormClose(string message)
        {
            MessageBox.Show(message);
            CStatus.Instance().IsBootError = true;
            CallFormClose();
        }
    }
}
