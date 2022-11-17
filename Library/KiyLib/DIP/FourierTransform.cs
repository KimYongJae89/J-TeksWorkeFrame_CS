using KiyLib.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KiyLib.DIP
{
    /// <summary>
    /// 퓨리에 변환에 사용되는 연산을 제공하는 클래스
    /// </summary>
    public class FourierTransform
    {
        /// <summary>
        /// 이산 퓨리에 변환   (개선 필요: 비효율적)
        /// </summary>
        /// <param name="G">실수=원영상의 픽셀값, 허수=0.0</param>
        /// <param name="width">가로 길이</param>
        /// <param name="height">세로 길이</param>
        /// <returns></returns>
        public static Complex[,] BasicDFT(Complex[,] G, int width, int height)
        {
            Complex[,] R = new Complex[height, width];
            int x, y, u, v;
            double theta, wh;
            Complex exp = new Complex();
            Complex imexp = new Complex();
            Complex iFuv = new Complex();

            wh = (double)(width * height);
            for (v = 0; v < height; v++)
            {
                for (u = 0; u < width; u++)
                {
                    iFuv.Real = 0.0;
                    iFuv.Imaginary = 0.0;

                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            theta = 2.0 * Math.PI * ((u * x / (double)width) +
                                                     (v * y / (double)height));
                            exp.Real = Math.Cos(theta);
                            exp.Imaginary = -Math.Sin(theta);
                            imexp = Complex.Multiply(G[y, x], exp);
                            iFuv.Real += imexp.Real;
                            iFuv.Imaginary += imexp.Imaginary;
                        }
                    }

                    R[v, u] = new Complex(iFuv.Real / wh, iFuv.Imaginary / wh);
                }
            }

            return R;
        }

        /// <summary>
        /// 이산 퓨리에 역변환  (개선 필요: 비효율적)
        /// </summary>
        /// <param name="G">실수=원영상의 픽셀값, 허수=0.0</param>
        /// <param name="width">가로 길이</param>
        /// <param name="height">세로 길이</param>
        /// <returns></returns>
        public static Complex[,] InverseDFT(Complex[,] G, int width, int height)
        {
            Complex[,] R = new Complex[height, width];
            int x, y, u, v;
            double theta, wh;
            Complex exp = new Complex();
            Complex imexp = new Complex();
            Complex iFuv = new Complex();

            wh = (double)(width * height);
            for (v = 0; v < height; v++)
            {
                for (u = 0; u < width; u++)
                {
                    iFuv.Real = 0.0;
                    iFuv.Imaginary = 0.0;

                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            theta = 2.0 * Math.PI * ((u * x / (double)width) +
                                                     (v * y / (double)height));
                            exp.Real = Math.Cos(theta);
                            exp.Imaginary = Math.Sin(theta);
                            imexp = Complex.Multiply(G[y, x], exp);
                            iFuv.Real += imexp.Real;
                            iFuv.Imaginary += imexp.Imaginary;
                        }
                    }

                    R[v, u] = new Complex(iFuv.Real / wh, iFuv.Imaginary / wh);
                }
            }

            return R;
        }

        public static int[,] ViewFourier(Complex[,] G, int width, int height)
        {
            int[,] R = new int[height, width];

            double[,] t = new double[height, width];
            double[,] m = new double[height, width];
            int x, y;

            for (y = 0; y < height; y++)
                for (x = 0; x < width; x++)
                    m[y, x] = Math.Sqrt(G[y, x].Real * G[y, x].Real + G[y, x].Imaginary * G[y, x].Imaginary);

            for (y = 0; y < height; y++)
                for (x = 0; x < width; x++)
                    t[y, x] = Math.Log10(1 + m[y, x]);

            double dmax = double.MinValue;

            for (y = 0; y < height; y++)
                for (x = 0; x < width; x++)
                    if (t[y, x] > dmax) dmax = t[y, x];

            double dConst = 255.0 / dmax;

            for (y = 0; y < height; y++)
                for (x = 0; x < width; x++)
                    R[y, x] = (int)(dConst * t[y, x]);

            return R;
        }

        public static int[,] ViewCenter(int[,] G, int width, int height)
        {
            int[,] R = new int[height, width];
            int x, y;
            int xMid = width / 2;
            int yMid = height / 2;

            //좌상 원점 대칭이동
            for (y = 0; y < yMid; y++)
                for (x = 0; x < xMid; x++)
                    R[yMid - 1 - y, xMid - 1 - x] = G[y, x];

            //좌하 원점 대칭이동
            for (y = yMid; y < height; y++)
                for (x = 0; x < xMid; x++)
                    R[height - 1 - y + yMid, xMid - 1 - x] = G[y, x];

            //우상 원점 대칭이동
            for (y = 0; y < yMid; y++)
                for (x = xMid; x < width; x++)
                    R[yMid - 1 - y, width - 1 - x + xMid] = G[y, x];

            //우하 원점 대칭이동
            for (y = yMid; y < height; y++)
                for (x = xMid; x < width; x++)
                    R[height - 1 - y + yMid, width - 1 - x + xMid] = G[y, x];

            return R;
        }

        public static Complex[,] FFT_2D(Complex[,] G, int width, int height, int M, int N)
        {
            var w = width;
            var h = height;

            Complex[,] R = new Complex[h, w];
            Complex[] OneDimColData = new Complex[h];
            Complex[] OneDimRowData = new Complex[w];

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    OneDimRowData[x] = new Complex();

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    OneDimRowData[x].Real = G[y, x].Real;
                    OneDimRowData[x].Imaginary = G[y, x].Imaginary;
                }

                Complex[] FourierRow = FFT(OneDimRowData, M, w);

                for (int x = 0; x < w; x++)
                    R[y, x] = new Complex(FourierRow[x].Real, FourierRow[x].Imaginary);
            }

            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    OneDimColData[y] = new Complex();

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    OneDimColData[y].Real = R[y, x].Real;
                    OneDimColData[y].Imaginary = R[y, x].Imaginary;
                }

                Complex[] FourierCol = FFT(OneDimColData, N, h);

                for (int y = 0; y < h; y++)
                {
                    R[y, x].Real = FourierCol[y].Real;
                    R[y, x].Imaginary = FourierCol[y].Imaginary;
                }
            }

            return R;
        }

        public static Complex[,] IFFT_2D(Complex[,] G, int width, int height, int M, int N)
        {
            var w = width;
            var h = height;

            Complex[,] R = new Complex[h, w];
            Complex[] OneDimColData = new Complex[h];
            Complex[] OneDimRowData = new Complex[w];

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    OneDimRowData[x] = new Complex();

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    OneDimRowData[x].Real = G[y, x].Real;
                    OneDimRowData[x].Imaginary = G[y, x].Imaginary;
                }

                Complex[] FourierRow = IFFT(OneDimRowData, M, w);

                for (int x = 0; x < w; x++)
                    R[y, x] = new Complex(FourierRow[x].Real, FourierRow[x].Imaginary);
            }

            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    OneDimColData[y] = new Complex();

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    OneDimColData[y].Real = R[y, x].Real;
                    OneDimColData[y].Imaginary = R[y, x].Imaginary;
                }

                Complex[] FourierCol = IFFT(OneDimColData, N, h);

                for (int y = 0; y < h; y++)
                {
                    R[y, x].Real = FourierCol[y].Real;
                    R[y, x].Imaginary = FourierCol[y].Imaginary;
                }
            }

            return R;
        }

        public static Complex[] FFT(Complex[] G, int log2N, int length)
        {
            Complex[] scrambleG = Scramble(G, length);
            Complex[] R = Butterflies(scrambleG, log2N, length);

            return R;
        }

        public static Complex[] IFFT(Complex[] G, int log2N, int length)
        {
            Complex[] scrambleG = Scramble(G, length);
            Complex[] R = IButterflies(scrambleG, log2N, length);

            return R;
        }

        public static Complex[] Scramble(Complex[] G, int length)
        {
            int i, j, m;
            double temp;
            Complex[] R = new Complex[length];

            for (i = 0; i < length; i++)
                R[i] = new Complex(G[i].Real, G[i].Imaginary);

            j = 0;

            for (i = 0; i < length; i++)
            {
                if (i > j)
                {
                    temp = R[j].Real;
                    R[j].Real = R[i].Real;
                    R[i].Real = temp;

                    temp = R[j].Imaginary;
                    R[j].Imaginary = R[i].Imaginary;
                    R[i].Imaginary = temp;
                }

                m = length >> 1;
                while ((j >= m) & (m > 1))
                {
                    j -= m;
                    m >>= 1;
                }
                j += m;
            }

            return R;
        }

        public static Complex[] Butterflies(Complex[] G, int log2N, int length)
        {
            int i, j, k, offset;
            int n, halfN;
            double theta, tdbl;

            Complex exp = new Complex();
            Complex iw = new Complex();
            Complex w = new Complex();
            Complex[] R = new Complex[length];

            for (i = 0; i < length; i++)
                R[i] = new Complex(G[i].Real, G[i].Imaginary);

            n = 1;

            for (i = 0; i < log2N; i++)
            {
                halfN = n;
                n <<= 1;
                theta = -2.0 * Math.PI / (double)n;
                tdbl = Math.Sin(0.5 * theta);
                exp.Real = -2.0 * tdbl * tdbl;
                exp.Imaginary = Math.Sin(theta);
                w.Real = 1.0;
                w.Imaginary = 0.0;

                for (offset = 0; offset < halfN; offset++)
                {
                    for (k = offset; k < length; k += n)
                    {
                        j = k + halfN;
                        iw = Complex.Multiply(w, R[j]);
                        R[j].Real = R[k].Real - iw.Real;
                        R[k].Real += iw.Real;
                        R[j].Imaginary = R[k].Imaginary - iw.Imaginary;
                        R[k].Imaginary += iw.Imaginary;
                    }
                    tdbl = w.Real;
                    w.Real = tdbl * exp.Real - w.Imaginary * exp.Imaginary + w.Real;
                    w.Imaginary = w.Imaginary * exp.Real + tdbl * exp.Imaginary + w.Imaginary;
                }
            }

            for (i = 0; i < length; i++)
            {
                R[i].Real /= (double)length;
                R[i].Imaginary /= (double)length;
            }

            return R;
        }

        public static Complex[] IButterflies(Complex[] G, int log2N, int length)
        {
            int i, j, k, offset;
            int n, halfN;
            double theta, tdbl;

            Complex exp = new Complex();
            Complex iw = new Complex();
            Complex w = new Complex();
            Complex[] R = new Complex[length];

            for (i = 0; i < length; i++)
                R[i] = new Complex(G[i].Real, G[i].Imaginary);

            n = 1;

            for (i = 0; i < log2N; i++)
            {
                halfN = n;
                n <<= 1;
                theta = 2.0 * Math.PI / (double)n;
                tdbl = Math.Sin(0.5 * theta);
                exp.Real = -2.0 * tdbl * tdbl;
                exp.Imaginary = Math.Sin(theta);
                w.Real = 1.0;
                w.Imaginary = 0.0;

                for (offset = 0; offset < halfN; offset++)
                {
                    for (k = offset; k < length; k += n)
                    {
                        j = k + halfN;
                        iw = Complex.Multiply(w, R[j]);
                        R[j].Real = R[k].Real - iw.Real;
                        R[k].Real += iw.Real;
                        R[j].Imaginary = R[k].Imaginary - iw.Imaginary;
                        R[k].Imaginary += iw.Imaginary;
                    }
                    tdbl = w.Real;
                    w.Real = tdbl * exp.Real - w.Imaginary * exp.Imaginary + w.Real;
                    w.Imaginary = w.Imaginary * exp.Real + tdbl * exp.Imaginary + w.Imaginary;
                }
            }

            for (i = 0; i < length; i++)
            {
                R[i].Real /= (double)length;
                R[i].Imaginary /= (double)length;
            }

            return R;
        }
    }
}
