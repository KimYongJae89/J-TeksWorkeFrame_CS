using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Structure;

namespace KiyEmguCV.DIP
{
    /// <summary>
    /// Fast Fourier Transform(FFT)에 사용되는 클래스
    /// </summary>
    public class FourierTransformCV : IDisposable
    {
        private Mat cvMat, magnitudeMat, padded, zeroMat, complexI, fwdfourier;
        private VectorOfMat matVector;
        private int m, n;


        /// <summary>
        /// FourierTransformCV의 생성자
        /// </summary>
        /// <param name="srcMat">FFT를 실행할 대상 Mat객체</param>
        public FourierTransformCV(Mat srcMat)
        {
            InitializeDelete();
            this.cvMat = srcMat;
        }


        /// <summary>
        /// Forward FFT를 실행한다
        /// </summary>
        /// <returns>결과 이미지</returns>
        public Bitmap ForwardFFT()
        {
            InitializeAlloc();

            fwdfourier = new Mat(complexI.Size, DepthType.Cv32F, 2);
            CvInvoke.Dft(complexI, fwdfourier, DxtType.Forward, complexI.Rows);

            magnitudeMat = Magnitude(fwdfourier);
            magnitudeMat = new Mat(magnitudeMat, new Rectangle(0, 0, magnitudeMat.Cols & -2, magnitudeMat.Rows & -2));

            SwitchQuadrants(ref magnitudeMat);

            Mat rstMat = magnitudeMat.Clone();
            rstMat.SetTo(new MCvScalar(0));

            CvInvoke.Normalize(magnitudeMat, magnitudeMat, 1.0, 0.1, NormType.MinMax, DepthType.Cv32F);
            CvInvoke.Imshow("fwd", magnitudeMat);
            //CvInvoke.Normalize(magnitudeMat, rstMat, 0, 255, NormType.MinMax, DepthType.Cv8U);

            return rstMat.ToImage<Gray, byte>().ToBitmap();
        }

        /// <summary>
        /// Inverse FFT를 실행한다        
        /// </summary>
        /// <param name="normalizeMin">normalize(정규화)를 실행할 최소값</param>
        /// <param name="normalizeMax">normalize(정규화)를 실행할 최대값</param>
        /// <returns>결과 이미지</returns>
        public Bitmap InverseFFT(int normalizeMin = 0, int normalizeMax = 255)
        {
            if (magnitudeMat == null)
                throw new Exception("magnitudeMat is NULL");

            InitializeAlloc();

            //fwdfourier = new Mat(complexI.Size, DepthType.Cv32F, 2);
            Mat invsfourier = new Mat(complexI.Size, DepthType.Cv32F, 2);

            //fwdfourier = new Mat(complexI.Size, DepthType.Cv32F, 2);
            //CvInvoke.Dft(complexI, fwdfourier, DxtType.Forward, complexI.Rows);
            CvInvoke.Dft(fwdfourier, invsfourier, DxtType.Inverse, fwdfourier.Rows);

            magnitudeMat = Magnitude(invsfourier);
            magnitudeMat = new Mat(magnitudeMat, new Rectangle(0, 0, magnitudeMat.Cols & -2, magnitudeMat.Rows & -2));

            Mat rstMat = magnitudeMat.Clone();
            rstMat.SetTo(new MCvScalar(0));

            CvInvoke.Normalize(magnitudeMat, magnitudeMat, 1.0, 0.0, NormType.MinMax, DepthType.Cv32F);
            CvInvoke.Imshow("inv", magnitudeMat);
            //CvInvoke.Normalize(magnitudeMat, rstMat, 0, 255, NormType.MinMax, DepthType.Cv8U);

            return rstMat.ToImage<Gray, byte>().ToBitmap();
        }
        

        /// <summary>
        /// 복소수를 대수로 변환한다
        /// </summary>
        /// <param name="fftData">변환할 복소수 FFT Mat객체</param>
        /// <returns>변환된 대수 Mat객체</returns>
        private Mat Magnitude(Mat fftData)
        {
            Mat real = new Mat(fftData.Size, DepthType.Cv32F, 1);
            Mat imaginary = new Mat(fftData.Size, DepthType.Cv32F, 1);
            VectorOfMat channels = new VectorOfMat();

            CvInvoke.Split(fftData, channels); //멀티 채널 mat를 여러 개의 단일 채널 mat으로 분리

            real = channels.GetOutputArray().GetMat(0);
            imaginary = channels.GetOutputArray().GetMat(1);

            CvInvoke.Pow(real, 2.0, real);
            CvInvoke.Pow(imaginary, 2.0, imaginary);
            CvInvoke.Add(real, imaginary, real);
            CvInvoke.Pow(real, 0.5, real);

            Mat onesMat = Mat.Ones(real.Rows, real.Cols, DepthType.Cv32F, 1);

            CvInvoke.Add(real, onesMat, real);
            CvInvoke.Log(real, real); //자연 대수

            return real;
        }

        /// <summary>
        /// 원점이 이미지의 중앙에 오도록 사분면을 다시 정렬한다
        /// </summary>
        /// <param name="mat">정렬한 Mat 객체</param>
        private void SwitchQuadrants(ref Mat mat)
        {
            int cx = mat.Cols / 2;
            int cy = mat.Rows / 2;

            Mat q0 = new Mat(mat, new Rectangle(0, 0, cx, cy)); //ROI 왼쪽 상단 영역
            Mat q1 = new Mat(mat, new Rectangle(cx, 0, cx, cy)); //ROI 오른쪽 상단 영역
            Mat q2 = new Mat(mat, new Rectangle(0, cy, cx, cy)); //ROI 왼쪽 하단 영역
            Mat q3 = new Mat(mat, new Rectangle(cx, cy, cx, cy)); //ROI 오른쪽 하단 영역

            Mat temp = new Mat(q0.Size, DepthType.Cv32F, 1);

            //왼쪽 상단 및 하단 오른쪽 교환)
            q0.CopyTo(temp);
            q3.CopyTo(q0);
            temp.CopyTo(q3);

            //오른쪽 상단 및 하단 왼쪽 교환
            q1.CopyTo(temp);
            q2.CopyTo(q1);
            temp.CopyTo(q2);
        }
        
        /// <summary>
        /// 사용전, 후 초기화 함수
        /// </summary>
        private void InitializeDelete()
        {
            cvMat?.Dispose();
            magnitudeMat?.Dispose();
            padded?.Dispose();
            zeroMat?.Dispose();
            complexI?.Dispose();

            matVector?.Dispose();

            m = n = 0;
        }

        /// <summary>
        /// FFT에 사용할 변수들을 초기화한다
        /// </summary>
        private void InitializeAlloc()
        {
            if (cvMat == null)
                throw new Exception("cvMat is NULL");

            padded = new Mat();
            matVector = new VectorOfMat(); //Mat 유형 벡터 만들기

            m = CvInvoke.GetOptimalDFTSize(cvMat.Rows);
            n = CvInvoke.GetOptimalDFTSize(cvMat.Cols);

            CvInvoke.CopyMakeBorder(cvMat, padded, 0, m - cvMat.Rows, 0, n - cvMat.Cols, BorderType.Constant);
            padded.ConvertTo(padded, DepthType.Cv32F); //padded를 Fourier 변환의 실수 부분인 Cv32F 유형으로 변환
            zeroMat = Mat.Zeros(padded.Rows, padded.Cols, DepthType.Cv32F, 1);

            matVector.Push(padded);  //padded를 matVector에 Push
            matVector.Push(zeroMat); //zeroMat를 matVector에 Push

            complexI = new Mat(padded.Size, DepthType.Cv32F, 2);
            CvInvoke.Merge(matVector, complexI); //matVector에 저장된 2 개의 mat를 complexI에 병합
        }


        /// <summary>
        /// 사용된 리소스를 해제한다
        /// </summary>
        public void Dispose()
        {
            InitializeDelete();
        }
    }
}
