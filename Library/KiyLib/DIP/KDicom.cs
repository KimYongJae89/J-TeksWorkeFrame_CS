using ClearCanvas.Dicom;
using KiyLib.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    /// <summary>
    /// Dicom포맷을 지원하기 위한 클래스
    /// </summary>
    public class KDicom
    {
        private DicomFile dcf;
        private ushort bitsPerPixel = 0;
        private int width;
        private int height;
        private string photometricInterpretation;
        private int samplePerPixel = 1;
        private string manufacturer = "J-Teks";
        public byte[] data8;
        private ushort[] data16;

        public int Width
        {
            get { return width; }
            private set { width = value; }
        }
        public int Height
        {
            get { return height; }
            private set { height = value; }
        }
        public string PhotometricInterpretation
        {
            get { return photometricInterpretation; }
            private set { photometricInterpretation = value; }
        }
        public int NumberOfChannels
        {
            get { return samplePerPixel; }
            set { samplePerPixel = value; }
        }
        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }
        public int NumberOfFrames
        {
            get
            {
                if (dcf != null)
                {
                    int tempFrameCnt;
                    dcf.DataSet[DicomTags.NumberOfFrames].TryGetInt32(0, out tempFrameCnt);

                    //NumberOfFrames 태그가 없는 경우에는 최소값 1로 리턴
                    return tempFrameCnt <= 0 ? 1 : tempFrameCnt;
                }

                return -1;
            }
        }
        public KDepthType Depth
        {
            get
            {
                if (dcf != null)
                {
                    int bitAllocated;
                    dcf.DataSet[DicomTags.BitsAllocated].TryGetInt32(0, out bitAllocated);

                    return (KDepthType)(bitAllocated * samplePerPixel);
                }

                return KDepthType.None;
            }
        }
        /// <summary>
        /// 칼라타입, 현재는 photometricInterpretation 태그값 [MONOCHROME2, RGB] 두가지로만 판별하고있다. 
        /// 추후에 확장하면서 수정가능성 있음
        /// </summary>
        public KColorType Color
        {
            get
            {
                string grayStr = "MONOCHROME2";
                string clrStr = "RGB";
                string str = this.photometricInterpretation;

                if (str == grayStr)
                    return KColorType.Gray;
                if (str == clrStr)
                    return KColorType.Color;

                return KColorType.Gray;
            }
            private set
            {
                this.photometricInterpretation = (value == KColorType.Gray) ? "MONOCHROME2" : "RGB";
            }
        }
        //public dynamic[] Data
        //{
        //    get
        //    {
        //        if (data8 == null && data16 == null)
        //            return null;

        //        if (data8 != null)
        //        {
        //            int len = data8.Length;
        //            dynamic[] rt = new dynamic[len];

        //            for (int i = 0; i < len; i++)
        //                rt[i] = data8[i];

        //            return rt;
        //        }
        //        else
        //        {
        //            int len = data16.Length;
        //            dynamic[] rt = new dynamic[len];

        //            for (int i = 0; i < len; i++)
        //                rt[i] = data16[i];

        //            return rt;
        //        }
        //    }
        //    private set
        //    {
        //        if (data8 != null)
        //        {
        //            for (int i = 0; i < value.Length; i++)
        //                data8[i] = (byte)value[i];
        //        }
        //        else
        //        {
        //            for (int i = 0; i < value.Length; i++)
        //                data16[i] = (ushort)value[i];
        //        }
        //    }
        //}


        public KDicom(int width, int height, KDepthType depth, KColorType color)
        {
            dcf = new DicomFile();
            this.width = width;
            this.height = height;
            this.Color = color;

            data8 = null;
            data16 = null;

            switch (depth)
            {
                case KDepthType.Dt_8:
                    bitsPerPixel = 8;
                    data8 = new byte[width * height];
                    break;

                case KDepthType.Dt_16:
                    bitsPerPixel = 16;
                    data16 = new ushort[width * height];
                    break;

                case KDepthType.Dt_24:
                    bitsPerPixel = 8;
                    data8 = new byte[width * height * 3];
                    break;

                case KDepthType.None:
                default:
                    throw new Exception("Depth는 None일수 없습니다");
            }

            SetDatasetByDefault();
        }

        public KDicom(string path)
        {
            dcf = new DicomFile();

            data8 = null;
            data16 = null;

            this.Load(path);
        }


        public void Load(string path)
        {
            try
            {
                if (dcf == null)
                    dcf = new DicomFile();

                dcf.Load(path);

                dcf.DataSet[DicomTags.Columns].TryGetInt32(0, out width);
                dcf.DataSet[DicomTags.Rows].TryGetInt32(0, out height);
                dcf.DataSet[DicomTags.PhotometricInterpretation].TryGetString(0, out photometricInterpretation);
                dcf.DataSet[DicomTags.SamplesPerPixel].TryGetInt32(0, out samplePerPixel);

                if (photometricInterpretation != "RGB" &&
                    photometricInterpretation != "MONOCHROME2")
                {
                    throw new FormatException(
                        string.Format("{KDicom: \"{0}\" 지원하지 않는 photometricInterpretation 형식입니다.}",
                        photometricInterpretation));
                }

                int pixelCntEachFrame = width * height;

                switch (Depth)
                {
                    case KDepthType.Dt_16:
                        data16 = KImage.Convert2ByteToUShort((byte[])dcf.DataSet[DicomTags.PixelData].Values);
                        break;

                    case KDepthType.Dt_8:
                    case KDepthType.Dt_24: //Data 순서는 RGB
                        data8 = (byte[])dcf.DataSet[DicomTags.PixelData].Values;
                        break;

                    case KDepthType.None:
                    default:
                        throw new Exception("KDicom: DepthType가 None 입니다.");
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw (err);
            }
        }

        public void Save(string path)
        {
            try
            {
                dcf.Save(path);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

        /* public void Save(string path, dynamic[] imageArr)
         {
             try
             {
                 int stride = width * NumberOfChannels;

                 switch (Depth)
                 {
                     case KDepthType.Dt_16:
                         WriteDicom<ushort>(path, dcf, imageArr, stride);
                         break;
                     case KDepthType.Dt_8:
                     case KDepthType.Dt_24:
                         WriteDicom<byte>(path, dcf, imageArr, stride);
                         break;
                     case KDepthType.None:
                     default:
                         break;
                 }
             }
             catch (Exception err)
             {
                 Console.WriteLine(err);
                 throw err;
             }
         }

         public void Save(string path, dynamic[] imageArr, KDepthType depth)
         {
             int stride = width * NumberOfChannels;

             switch (Depth)
             {
                 case KDepthType.Dt_16:
                     //WriteDicom<ushort>(path, dcf, imageArr, stride, depth);
                     break;
                 case KDepthType.Dt_8:
                 case KDepthType.Dt_24:
                     //WriteDicom<byte>(path, dcf, imageArr, stride, depth);
                     break;
                 case KDepthType.None:
                 default:
                     break;
             }
         }*/

        public void Save(string path, byte[] imageArr, KColorType color)
        {
            int stride = width * NumberOfChannels;
            this.Color = color;

            WriteDicom(path, imageArr);
        }

        public void Save(string path, ushort[] imageArr)
        {
            int stride = width * NumberOfChannels;

            WriteDicom(path, imageArr);
        }

        public T[,,] GetFrameData<T>(int frameNumberToGet = 0, bool orderIsRGB = true)
        {
            if (frameNumberToGet >= this.NumberOfFrames || frameNumberToGet < 0)
                throw new IndexOutOfRangeException("KDicom - GetData(..): 존재하지않는 Frame입니다.");

            T[,,] rtData = new T[this.height, this.width, this.samplePerPixel];

            switch (Depth)
            {
                case KDepthType.Dt_8:
                    var frameData8 = KImage.GetFrameData(data8, width, Height, NumberOfChannels, frameNumberToGet);
                    rtData = KImage.Convert1Dto3D<byte, T>(frameData8, width, height, KColorType.Gray);
                    break;

                case KDepthType.Dt_16:
                    var frameData16 = KImage.GetFrameData(data16, width, Height, NumberOfChannels, frameNumberToGet);
                    rtData = KImage.Convert1Dto3D<ushort, T>(frameData16, width, height, KColorType.Gray);
                    break;

                case KDepthType.Dt_24:
                    var frameData24 = KImage.GetFrameData(data8, width, Height, NumberOfChannels, frameNumberToGet);
                    var cvt3dData = KImage.Convert1Dto3D<byte, T>(frameData24, width, height, KColorType.Color);

                    if (orderIsRGB)
                        rtData = cvt3dData;
                    else //채널 순서BGR로 변경(현재 클래스는 칼라는 RGB순서만 지원)
                        rtData = KImage.ReverseRGBChannel(cvt3dData);

                    break;

                case KDepthType.None:
                default:
                    break;
            }

            return rtData;
        }

        private void SetDatasetByDefault()
        {
            string studyId = DateTime.Now.ToString("yyyyMM");
            string date = DateTime.Now.ToString("yyyyMMdd");
            string time = DateTime.Now.ToString("HHmmss");

            dcf.DataSet[DicomTags.StudyDate].SetDateTime(0, DateTime.Now);
            dcf.DataSet[DicomTags.StudyTime].SetDateTime(0, DateTime.Now);
            dcf.DataSet[DicomTags.AccessionNumber].SetStringValue("NONE");
            dcf.DataSet[DicomTags.ReferringPhysiciansName].SetStringValue("NONE");
            dcf.DataSet[DicomTags.PatientsName].SetStringValue("NONE");
            dcf.DataSet[DicomTags.PatientId].SetStringValue("NONE");
            dcf.DataSet[DicomTags.PatientsBirthDate].SetDateTime(0, DateTime.Now);
            dcf.DataSet[DicomTags.StudyId].SetStringValue(studyId);
            dcf.DataSet[DicomTags.SeriesNumber].SetStringValue("1");
            dcf.DataSet[DicomTags.PatientOrientation].SetStringValue("A");
            dcf.DataSet[DicomTags.InstanceNumber].SetStringValue("1");
            dcf.DataSet[DicomTags.StudyInstanceUid].SetStringValue(DicomUid.GenerateUid().UID);
            dcf.DataSet[DicomTags.SeriesInstanceUid].SetStringValue(DicomUid.GenerateUid().UID);
            dcf.DataSet[DicomTags.PatientsBirthTime].SetDateTime(0, DateTime.Now);
            dcf.DataSet[DicomTags.PatientsSex].SetStringValue("O");
            dcf.DataSet[DicomTags.PatientsAge].SetStringValue("1Y");

            dcf.DataSet[DicomTags.BitsAllocated].SetUInt16(0, bitsPerPixel);
            dcf.DataSet[DicomTags.BitsStored].SetUInt16(0, bitsPerPixel);
            dcf.DataSet[DicomTags.HighBit].SetUInt16(0, (ushort)(bitsPerPixel - 1));
            dcf.DataSet[DicomTags.Columns].SetInt32(0, width);
            dcf.DataSet[DicomTags.Rows].SetInt32(0, height);
            dcf.DataSet[DicomTags.Manufacturer].SetStringValue(manufacturer);
            dcf.DataSet[DicomTags.ManufacturersModelName].SetStringValue(manufacturer);
            dcf.DataSet[DicomTags.NumberOfFrames].SetStringValue("1");

            int sPp = (Color == KColorType.Gray) ? 1 : 3;
            dcf.DataSet[DicomTags.SamplesPerPixel].SetUInt16(0, (ushort)sPp);

            string pt = (Color == KColorType.Gray) ? "MONOCHROME2" : "RGB";
            dcf.DataSet[DicomTags.PhotometricInterpretation].SetStringValue(pt);
        }

        /*private void SetDatasetByDefault()
         {
             string studyId = DateTime.Now.ToString("yyyyMM");
             string date = DateTime.Now.ToString("yyyyMMdd");
             string time = DateTime.Now.ToString("HHmmss");

             dcf.MediaStorageSopClassUid = SopClass.DigitalXRayImageStorageForPresentation.Uid;
             dcf.DataSet[DicomTags.SopClassUid].SetStringValue(SopClass.DigitalXRayImageStorageForPresentation.Uid);
             dcf.TransferSyntax = TransferSyntax.ExplicitVrLittleEndian;
             dcf.ImplementationClassUid = "2.16.840.1.00000000000"; //TODO: fill it with appropriate value
             dcf.ImplementationVersionName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
             dcf.SourceApplicationEntityTitle = "KDicom";

             DicomUid sopInstanceUid = DicomUid.GenerateUid();
             dcf.MediaStorageSopInstanceUid = sopInstanceUid.UID;
             dcf.DataSet[DicomTags.SopInstanceUid].SetStringValue(sopInstanceUid.UID);
             dcf.DataSet[DicomTags.StudyInstanceUid].SetStringValue(DicomUid.GenerateUid().UID);
             dcf.DataSet[DicomTags.SeriesInstanceUid].SetStringValue(DicomUid.GenerateUid().UID);

             //dcf.DataSet[DicomTags.Modality].SetEmptyValue();
             dcf.DataSet[DicomTags.Modality].SetString(0, "CT");
             dcf.DataSet[DicomTags.SpecificCharacterSet].SetEmptyValue();
             dcf.DataSet[DicomTags.SoftwareVersions].SetString(0, dcf.ImplementationVersionName);
             dcf.DataSet[DicomTags.StationName].SetStringValue(Environment.MachineName);
             //dcf.DataSet[DicomTags.Manufacturer].SetStringValue(manufactrer);
             dcf.DataSet[DicomTags.ManufacturersModelName].SetStringValue("J-Teks");

             // Pixel Data
             dcf.DataSet[DicomTags.ImageType].SetStringValue(@"ORIGINAL\PRIMARY");
             dcf.DataSet[DicomTags.Columns].SetInt32(0, width);
             dcf.DataSet[DicomTags.Rows].SetInt32(0, height);
             //dcf.DataSet[DicomTags.BitsStored].SetInt16(0, bitsPerPixel);
             //dcf.DataSet[DicomTags.BitsAllocated].SetInt16(0, 16);

             dcf.DataSet[DicomTags.BitsStored].SetUInt16(0, bitsPerPixel);
             dcf.DataSet[DicomTags.BitsAllocated].SetUInt16(0, bitsPerPixel);
             dcf.DataSet[DicomTags.HighBit].SetUInt16(0, (ushort)(bitsPerPixel - 1));

             if (data8 == null && data16 == null)
                 throw new Exception("data가 Null입니다");
             if (data8 != null)
                 dcf.DataSet[DicomTags.PixelData].Values = data8;
             if (data16 != null)
                 dcf.DataSet[DicomTags.PixelData].Values = data16;

             dcf.DataSet[DicomTags.AccessionNumber].SetEmptyValue();             //
             dcf.DataSet[DicomTags.ReferringPhysiciansName].SetEmptyValue();     //
             dcf.DataSet[DicomTags.PatientsName].SetEmptyValue();                //
             dcf.DataSet[DicomTags.PatientId].SetEmptyValue();                   //
             dcf.DataSet[DicomTags.PatientsBirthDate].SetEmptyValue();           //

             dcf.DataSet[DicomTags.PatientsSex].SetEmptyValue();
             dcf.DataSet[DicomTags.PatientsWeight].SetFloat32(0, 0f);
             dcf.DataSet[DicomTags.MedicalAlerts].SetEmptyValue();
             dcf.DataSet[DicomTags.Allergies].SetEmptyValue();
             dcf.DataSet[DicomTags.PregnancyStatus].SetEmptyValue();

             dcf.DataSet[DicomTags.RequestingPhysician].SetEmptyValue();
             dcf.DataSet[DicomTags.RequestedProcedureDescription].SetEmptyValue();
             dcf.DataSet[DicomTags.AdmissionId].SetEmptyValue();
             dcf.DataSet[DicomTags.SpecialNeeds].SetEmptyValue();
             dcf.DataSet[DicomTags.CurrentPatientLocation].SetEmptyValue();

             dcf.DataSet[DicomTags.PatientState].SetEmptyValue();
             dcf.DataSet[DicomTags.StudyId].SetStringValue(studyId);
             //dicomFile.DataSet[DicomTags.ItemDelimitationItem];
             //dicomFile.DataSet[DicomTags.SequenceDelimitationItem];
             dcf.DataSet[DicomTags.RequestedProcedureId].SetEmptyValue();

             dcf.DataSet[DicomTags.StudyDate].SetStringValue(date);  //
             dcf.DataSet[DicomTags.StudyTime].SetStringValue(time);  //
             dcf.DataSet[DicomTags.SeriesDate].SetStringValue(date);
             dcf.DataSet[DicomTags.SeriesTime].SetStringValue(time);
             dcf.DataSet[DicomTags.AcquisitionDate].SetStringValue(date);

             dcf.DataSet[DicomTags.AcquisitionTime].SetStringValue(time);
             dcf.DataSet[DicomTags.ContentDate].SetStringValue(date);
             dcf.DataSet[DicomTags.ContentTime].SetStringValue(time);
             dcf.DataSet[DicomTags.InstitutionName].SetEmptyValue();// SetStringValue("");
             dcf.DataSet[DicomTags.StudyDescription].SetEmptyValue();

             dcf.DataSet[DicomTags.SeriesDescription].SetEmptyValue();
             dcf.DataSet[DicomTags.SeriesNumber].SetEmptyValue();
             dcf.DataSet[DicomTags.AcquisitionNumber].SetEmptyValue();
             dcf.DataSet[DicomTags.InstanceNumber].SetEmptyValue();

             //dcf.DataSet[DicomTags.WindowWidth].SetInt32(0, width);
             //dcf.DataSet[DicomTags.WindowCenter].SetInt32(0, height);

             int sPp = (Color == KColorType.Gray) ? 1 : 3;
             dcf.DataSet[DicomTags.SamplesPerPixel].SetInt16(0, (short)sPp);

             //string pt = (Color == KColorType.Gray) ? "MONOCHROME2" : "RGB";
             //dcf.DataSet[DicomTags.PhotometricInterpretation].SetStringValue(pt);
         }*/

        //check - test
        /*private void WriteDicom<T>(string path, DicomFile dcm, dynamic[] imageArr, int stride)
        {
            int tSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
            T[] cvtImgArr = new T[imageArr.Length];

            Data = imageArr;

            dcf.DataSet[DicomTags.PixelData].Values = KImage.ConvertType<dynamic, T>(Data);

            dcf.Save(path);
        }*/

        //8, 24bit 저장
        private void WriteDicom(string path, byte[] imageArr)
        {
            data8 = imageArr;

            if (data16 != null)
                Array.Clear(data16, 0, data16.Length);

            if (data8 == null && data16 == null)
                throw new Exception("data가 Null입니다");
            if (data8 != null && data16 != null)
                throw new Exception("data8 data16이 둘다 Null이 아닙니다");

            if (Color == KColorType.Gray)
            {
                dcf.DataSet[DicomTags.PixelData].Values = imageArr;
            }
            else //color
            {
                //칼라 영상일때 설정해야 하는 tag
                dcf.DataSet[DicomTags.PlanarConfiguration].SetUInt16(0, 0);

                byte[,,] d3Data = KImage.Convert1Dto3D<byte, byte>(data8, this.Width, this.height, Color);
                byte[,,] cvtData = KImage.ReverseRGBChannel<byte>(d3Data);

                dcf.DataSet[DicomTags.PixelData].Values = KImage.Convert3Dto1D(cvtData);
            }

            dcf.Save(path);
        }

        //16bit로 저장
        private void WriteDicom(string path, ushort[] imageArr)
        {
            data16 = imageArr;

            if (data8 != null)
                Array.Clear(data8, 0, data16.Length);

            if (data8 == null && data16 == null)
                throw new Exception("data가 Null입니다");
            if (data8 != null && data16 != null)
                throw new Exception("data8 data16이 둘다 Null이 아닙니다");

            var cvtData = KImage.ConvertUSortTo2Byte(data16);
            dcf.DataSet[DicomTags.PixelData].Values = cvtData;

            dcf.Save(path);
        }
    }
}
