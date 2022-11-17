using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    public partial class KImage
    {
        /// <summary>
        /// 이미지 버퍼 산술연산
        /// </summary>
        /// <param name="dstBuf">연산 결과를 저장할 버퍼</param>
        /// <param name="srcBuf1">피연산자 버퍼1</param>
        /// <param name="srcBuf2">피연산자 버퍼2</param>
        /// <param name="oper">실행할 연산의 종류</param>
        /// <param name="doFloatProc">소수점에서 반올림한 근사값으로 계산할지 여부(나누기만 해당)</param>
        public static void Arith(int[] dstBuf, int[] srcBuf1, int[] srcBuf2, KArith oper, bool doFloatProc = true)
        {
            int len = srcBuf2.Length;

            if (doFloatProc)
            {
                switch (oper)
                {
                    case DIP.KArith.Add:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = srcBuf1[i] + srcBuf2[i];
                        break;
                    case DIP.KArith.Sub:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = srcBuf1[i] - srcBuf2[i];
                        break;
                    case DIP.KArith.Mult:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = srcBuf1[i] * srcBuf2[i];
                        break;
                    case DIP.KArith.Div:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = (int)Math.Round(srcBuf1[i] / (double)srcBuf2[i]);
                        break;
                }
            }
            else
            {
                switch (oper)
                {
                    case DIP.KArith.Add:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = srcBuf1[i] + srcBuf2[i];
                        break;
                    case DIP.KArith.Sub:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = srcBuf1[i] - srcBuf2[i];
                        break;
                    case DIP.KArith.Mult:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = srcBuf1[i] * srcBuf2[i];
                        break;
                    case DIP.KArith.Div:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = (int)(srcBuf1[i] / (double)srcBuf2[i]);
                        break;
                }
            }
        }

        /// <summary>
        /// 이미지 버퍼 산술연산
        /// </summary>
        /// <param name="dstBuf">연산 결과를 저장할 버퍼</param>
        /// <param name="srcBuf">피연산자 버퍼</param>
        /// <param name="val">연산을 시행할 값</param>
        /// <param name="oper">실행할 연산의 종류</param>
        /// <param name="doFloatProc">소수점에서 반올림한 근사값으로 계산할지 여부, 연산속도는 더 느리나 더 정확한 값을 얻을수 있다.</param>
        public static void Arith(int[] dstBuf, int[] srcBuf, double val, KArith oper, bool doFloatProc = true)
        {
            int len = srcBuf.Length;

            if (doFloatProc)
            {
                switch (oper)
                {
                    case DIP.KArith.Add:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = (int)Math.Round(srcBuf[i] + val);
                        break;
                    case DIP.KArith.Sub:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = (int)Math.Round(srcBuf[i] - val);
                        break;
                    case DIP.KArith.Mult:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = (int)Math.Round(srcBuf[i] * val);
                        break;
                    case DIP.KArith.Div:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = (int)Math.Round(srcBuf[i] / val);
                        break;
                }
            }
            else
            {
                switch (oper)
                {
                    case DIP.KArith.Add:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = (int)(srcBuf[i] + val);
                        break;
                    case DIP.KArith.Sub:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = (int)(srcBuf[i] - val);
                        break;
                    case DIP.KArith.Mult:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = (int)(srcBuf[i] * val);
                        break;
                    case DIP.KArith.Div:
                        for (int i = 0; i < len; i++)
                            dstBuf[i] = (int)(srcBuf[i] / val);
                        break;
                }
            }
        }
    }
}
