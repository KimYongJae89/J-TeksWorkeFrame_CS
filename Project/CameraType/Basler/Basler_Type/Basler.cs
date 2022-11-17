using CameraInterface;
using PylonC.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basler_Type
{
    public class Basler : ICamera
    {
        private int _width = 1296;
        private int _height = 966;
        byte[] ImgBuffer = null;
        private bool _isGrabberOpen;
        PYLON_DEVICE_HANDLE hDev = new PYLON_DEVICE_HANDLE();    /* Handle for the pylon device. */
        PYLON_IMAGE_FORMAT_CONVERTER_HANDLE hConv;               /* The format converter is used mainly for coverting color images. It is not used for Mono8 or RGBA8packed images. */
        protected PYLON_STREAMGRABBER_HANDLE m_hGrabber;   /* Handle for the pylon stream grabber. */
        protected PYLON_WAITOBJECT_HANDLE m_hWait;        /* Handle used for waiting for a grab to be finished. */
        PylonBuffer<Byte> imgBuf = null;                         /* Buffer used for grabbing. */
        PylonBuffer<Byte> imgConvBuf = null;                     /* Reference to the buffer attached to the grab result handle. */
        protected Dictionary<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> m_buffers; /* Holds handles and buffers used for grabbing. */
        protected bool m_grabThreadRun = false;
        protected bool m_bStopFlag = false;
        protected bool m_bColorMode = false;
        protected uint m_numberOfBuffersUsed = 8;
        Thread m_grabThread;
        PylonGrabResult_t grabResult;
        bool isReady;                  /* Used as an output parameter. */
        int i = 0;
        uint payloadSize;
        uint numDevices;                                          /* Number of available devices. */
        
        public override bool Initializate()
        {
            Pylon.Initialize();
            bool result = FindCam();

            return result;
        }

        private bool FindCam()
        {
            try
            {
                numDevices = Pylon.EnumerateDevices();

                if (0 == numDevices)
                {
                    return false;
                }
                else
                {
                    hDev = Pylon.CreateDeviceByIndex(0);  // 카메라 한대

                    Pylon.DeviceOpen(hDev, Pylon.cPylonAccessModeControl | Pylon.cPylonAccessModeStream);
                    string deviceName = Pylon.DeviceFeatureToString(hDev, "DeviceModelName");

                    Pylon.DeviceFeatureFromString(hDev, "PixelFormat", "Mono12");

                    m_bColorMode = false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public override string Close()
        {
            try
            {
                if (m_hGrabber != null)
                {
                    if (this._isGrabberOpen == true)
                    {
                        Pylon.StreamGrabberFinishGrab(m_hGrabber);
                        Pylon.StreamGrabberClose(m_hGrabber);
                        Pylon.DeviceClose(hDev);
                        Pylon.DestroyDevice(hDev);
                    }
                }
            }
            catch (Exception)
            {
                return "Fail";
            }
            Pylon.Terminate();
            return "True";
        }

        public override ushort[] GetDisplayCalc16bitRawData()
        {
            throw new NotImplementedException();
        }

        public override int GetOrgHeight()
        {
            return _height;
        }

        public override int GetOrgWidth()
        {
            return _width;
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
                Pylon.DeviceFeatureFromString(hDev, "TriggerSelector", "FrameStart");
                Pylon.DeviceFeatureFromString(hDev, "TriggerSelector", "AcquisitionStart");
                Pylon.DeviceFeatureFromString(hDev, "TriggerMode", "Off");
                Pylon.DeviceFeatureFromString(hDev, "TriggerSource", "Software");

                Pylon.DeviceFeatureFromString(hDev, "OffsetX", "0");
                Pylon.DeviceFeatureFromString(hDev, "OffsetY", "0");

                Pylon.DeviceFeatureFromString(hDev, "Width", this._width.ToString());
                Pylon.DeviceFeatureFromString(hDev, "Height", this._height.ToString());

                GrabberOpen();
                //m_grabOnce = false;
                m_grabThreadRun = true;
                m_bStopFlag = true;      // Live 정지 Flag
                if (m_grabThread != null)
                {
                    // StopGrap();
                }

                m_numberOfBuffersUsed = 8; // avg ?
                m_buffers = new Dictionary<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>>();
                for (i = 0; i < m_numberOfBuffersUsed; ++i)
                {
                    PylonBuffer<Byte> buffer = new PylonBuffer<byte>(payloadSize, true);
                    PYLON_STREAMBUFFER_HANDLE handle = Pylon.StreamGrabberRegisterBuffer(m_hGrabber, ref buffer);
                    m_buffers.Add(handle, buffer);
                }
                i = 0;
                foreach (KeyValuePair<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> pair in m_buffers)
                {
                    Pylon.StreamGrabberQueueBuffer(m_hGrabber, pair.Key, i++);
                }

                base.ResetAvg = true;

                m_grabThread = new Thread(Grab);
                m_grabThread.Start();

                Pylon.DeviceExecuteCommandFeature(hDev, "AcquisitionStart");

                return "success";
            }
            catch (Exception)
            {
                return "Fail";
            }
        }

        private void GrabberOpen()
        {
            try
            {
                m_hGrabber = Pylon.DeviceGetStreamGrabber(hDev, 0);

                Pylon.StreamGrabberOpen(m_hGrabber);

                Pylon.DeviceFeatureFromString(hDev, "AcquisitionMode", "Continuous");

                m_hWait = Pylon.StreamGrabberGetWaitObject(m_hGrabber);
                payloadSize = checked((uint)Pylon.DeviceGetIntegerFeature(hDev, "PayloadSize"));

                Pylon.StreamGrabberSetMaxNumBuffer(m_hGrabber, m_numberOfBuffersUsed);

                Pylon.StreamGrabberSetMaxBufferSize(m_hGrabber, payloadSize);
                Pylon.StreamGrabberPrepareGrab(m_hGrabber);
                this._isGrabberOpen = true;
            }
            catch (Exception)
            {
            }
        }

        protected void Grab()
        {
            try
            {
                while (m_grabThreadRun)
                {
                    if (!Pylon.WaitObjectWait(m_hWait, 3000))
                    {
                        Console.WriteLine("A grab timeout occurred.");
                        throw new Exception("A grab timeout occurred.");
                    }
                    if (!Pylon.StreamGrabberRetrieveResult(m_hGrabber, out grabResult))
                    {
                        Console.WriteLine("Failed to retrieve a grab result.");
                        throw new Exception("Failed to retrieve a grab result.");
                    }
                    int bufferIndex = (int)grabResult.Context;

                    if (grabResult.Status == EPylonGrabStatus.Grabbed)
                    {
                        if (!m_buffers.TryGetValue(grabResult.hBuffer, out imgBuf))
                        {
                            throw new Exception("Failed to find the buffer associated with the handle returned in grab result.");
                        }

                        Display();
                        Pylon.StreamGrabberQueueBuffer(m_hGrabber, grabResult.hBuffer, (int)grabResult.Context);
                        if (m_bStopFlag == false)
                        {
                            Console.WriteLine("Stop Flag에 들어옴");
                            m_grabThreadRun = false;

                            Pylon.DeviceExecuteCommandFeature(hDev, "AcquisitionStop");
                            Pylon.StreamGrabberCancelGrab(m_hGrabber);

                            do
                            {
                                isReady = Pylon.StreamGrabberRetrieveResult(m_hGrabber, out grabResult);

                            } while (isReady);

                            foreach (KeyValuePair<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> pair in m_buffers)
                            {
                                Pylon.StreamGrabberDeregisterBuffer(m_hGrabber, pair.Key);
                                pair.Value.Dispose();
                            }
                            m_buffers = null;
                        }
                        // Pylon.StreamGrabberQueueBuffer(m_hGrabber, grabResult.hBuffer, (int)grabResult.Context);
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception)
            {
                m_grabThreadRun = false;
                Pylon.DeviceExecuteCommandFeature(hDev, "AcquisitionStop");
                Pylon.StreamGrabberCancelGrab(m_hGrabber);
                do
                {
                    isReady = Pylon.StreamGrabberRetrieveResult(m_hGrabber, out grabResult);

                } while (isReady);

                foreach (KeyValuePair<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> pair in m_buffers)
                {
                    Pylon.StreamGrabberDeregisterBuffer(m_hGrabber, pair.Key);
                    pair.Value.Dispose();
                }
                m_buffers = null;
            }
        }

        private void Display()
        {
            try
            {
                if (m_bColorMode == true)
                {
                    hConv = PylonC.NET.Pylon.ImageFormatConverterCreate();

                    PylonC.NET.Pylon.ImageFormatConverterSetOutputPixelFormat(hConv, EPylonPixelType.PixelType_BGR8packed);
                    PylonC.NET.Pylon.ImageFormatConverterConvert(hConv, ref imgConvBuf, imgBuf, grabResult.PixelType, (uint)grabResult.SizeX, (uint)grabResult.SizeY, (uint)grabResult.PaddingX, EPylonImageOrientation.ImageOrientation_TopDown);

                    ImgBuffer = imgConvBuf.Array;
                }
                else // 흑백 이미지
                {
                    ImgBuffer = imgBuf.Array;
                }
                base.CalcAvgBuffer(ImgBuffer.Select(x => (int)x).ToArray(), this._width, this._height, 16);
            }
            catch (Exception)
            {
            }
        }

        public override string StopGrap()
        {
            try
            {
                m_bStopFlag = false;
                m_grabThread.Join();
                GrabberClose();
                return "True";
            }
            catch (Exception)
            {
                return "Fail";
            }
        }

        private void GrabberClose()
        {
            m_hGrabber = Pylon.DeviceGetStreamGrabber(hDev, 0);
            Pylon.StreamGrabberClose(m_hGrabber);
            this._isGrabberOpen = false;
        }


        public override void TestAvg(byte[] buffer16Bit)
        {
            throw new NotImplementedException();
        }
    }
}
