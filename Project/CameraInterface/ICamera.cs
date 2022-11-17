using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraInterface
{
    public enum eCameraType
    {
        NONE,
        ViewWorks_FXDD_0606CA,
        Thales_2121S,
        Basler_acA1300_30gm,
        Basler_acA1300_60gm,
        Basler_acA2040_25gmNIR
    }
   
    public abstract class ICamera
    {
        public delegate void SendDisplayBufferDele(int[] buffer, int width, int height, int bit);
        /// <summary>
        /// MainForm에 그려줄 8bit RawData
        /// </summary>
        /// <param name="callbackFunc"></param>
        public SendDisplayBufferDele callbackFunc;
        public abstract bool Initializate();
        public abstract string StartContinuousGrap();
        public abstract string StopGrap();
        public abstract void ShowParamForm();
        public abstract void SetParam(object param);
        public abstract string GetParamValue(object param);
        public abstract string Close();
        public abstract ushort[] GetDisplayCalc16bitRawData();
        public abstract void TestAvg(byte[] buffer16Bit);
        public abstract int GetOrgWidth();
        public abstract int GetOrgHeight();
        //Avg
        public byte[] rstBuffer;     //Avg처리된 16bit에서 8bit로 변환한후 저장되는, 최종 이미지 버퍼
        public int[] tempBuffer;     //카메라로부터 받은 원본 데이터 ex)image_info.pImage 카피
        public int[] avgSumArr;    //Avg를 위해 영상 합산한 결과 임시저장
        public int[] avgResultArr; //Avg합산된 이미지를 계산하여 최종 Avg이미지 배열 저장

        public int AvgCount = 1;
        private int avgCurrentCount = 0;
        public bool ResetAvg = true;
        public void BufferInitializate(int width, int height, int bit)
        {
            this.rstBuffer = new byte[width * height];
            this.tempBuffer = new int[width * height];
            this.avgSumArr = new int[width * height];
            this.avgResultArr = new int[width * height];
        }
        public void CalcAvgBuffer(int[] inputBuffer, int width, int height, int bit)
        {
            ////버퍼 초기화
            if (AvgCount == 1)
            {
                Array.Copy(inputBuffer, avgResultArr, inputBuffer.Length);
            }
            else
            {
                //Avg 결과 버퍼 초기화후 다시 계산.
                if (ResetAvg)
                {
                    avgCurrentCount = 0;
                    ResetAvg = false;
                    Array.Clear(avgSumArr, 0, inputBuffer.Length);
                    Array.Clear(avgResultArr, 0, inputBuffer.Length);
                }
                if (AvgCount > avgCurrentCount)
                {
                    avgCurrentCount++;

                    KImage.Arith(avgSumArr, tempBuffer, avgSumArr, KArith.Add);
                    KImage.Arith(avgResultArr, avgSumArr, avgCurrentCount, KArith.Div);
                }
                else
                {
                    double ratio = (AvgCount - 1) / (double)AvgCount;

                    KImage.Arith(avgSumArr, avgSumArr, ratio, KArith.Mult);
                    KImage.Arith(avgSumArr, tempBuffer, avgSumArr, KArith.Add);
                    KImage.Arith(avgResultArr, avgSumArr, avgCurrentCount, KArith.Div);
                }
            }
            if (callbackFunc != null)
                callbackFunc(avgResultArr, width, height, bit);
        }
        public void ResetAvgCount()
        {
            ResetAvg = true;
        }
        public void RegisterCallBack(SendDisplayBufferDele callbackFunc)
        {
            callbackFunc = new SendDisplayBufferDele(callbackFunc);
        }
    }
}
