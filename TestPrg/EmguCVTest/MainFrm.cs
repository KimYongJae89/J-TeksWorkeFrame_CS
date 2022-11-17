using Emgu.CV;
using Emgu.CV.Cvb;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using ImageManipulator;
using KiyEmguCV.DIP;
using KiyLib.DIP;
using KiyLib.General;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EmguCVTest
{
    public partial class MainFrm : Form
    {
        //private JImage img8, img16, img24;

        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            //img8 = new JImage(@"d:\4\8bitG.bmp");
            //img16 = new JImage(@"d:\4\16bitG.tif");
            //img24 = new JImage(@"d:\4\24bitC.bmp");

            //this.pbx.Image = img8.ToBitmap();
        }

        private void nuUpDn_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"D:\_TargetSample.TIF";

            var jImg = new JImage(path);
            jImg.GetROIInfo(new Rectangle());

            Mat mat = CvInvoke.Imread(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);
            Mat mat2 = new Mat(mat, new Rectangle(364, 686, 618, 201));
            var imgHist = mat2.ToImage<Gray, ushort>();

            int min, max, len = 65536;
            float[] histArr = new float[len];

            using (var hist = new DenseHistogram(len, new RangeF(0, len)))
            {
                histArr = new float[len];
                hist.Calculate(new Image<Gray, ushort>[] { imgHist }, true, null);
                hist.CopyTo(histArr);
            }

            KCommon.IndexOfFirstNonZeroAtBothSide(histArr, out min, out max);

            Console.WriteLine();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int min, max, min2, max2;
            string path;
            path = @"D:\_TargetSample.TIF";
            //path = @"D:\8bitG.bmp";
            //path = @"D:\8bitG.bmp";
            //path = @"D:\8bitG.bmp";
            //path = @"D:\8bitG.bmp";

            var jImg = new JImage(path);
            //var roi = jImg.GetROIInfo(new Rectangle(538, 668, 441, 141));
            //var roi = jImg.GetROIInfo(new Rectangle(168, 224, 38, 58));

            //roi.GetHistoFirstNonZeroAtBothSideGray(out min, out max);


            Stopwatch sw = new Stopwatch();
            sw.Start();
            jImg.GetROIInfo(new Rectangle(538, 668, 441, 141)).GetHistoFirstNonZeroAtBothSideGray(out min, out max);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Reset();

            var jImg2 = new JImage(path);

            sw.Start();
            jImg2.GetHistoFirstNonZeroAtBothSideGray(new Rectangle(538, 668, 441, 141), out min2, out max2);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            Console.WriteLine(string.Format("{0},{1} : {2},{3}", min, max, min2, max2));
            Console.WriteLine();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int min, max;
            double mean;

            //string path = @"D:\SNRTestSet.bmp";
            string path = @"D:\샘플 이미지 원본\8bitG.bmp";

            var region = new Rectangle(687, 638, 165, 367);
            var jImg = new JImage(path);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            //jImg.GetHistoFirstNonZeroAtBothSideGray(region, out min, out max);

            var roi = jImg.GetROIInfo(new Rectangle(105, 108, 201, 175));
            min = roi.Min;
            max = roi.Max;
            mean = roi.Mean;

            var stdev = roi.GetStandardDeviationGray();
            //var snr = roi.GetSNRGray();

            double mean2, stdev2, unSnr, snr;
            roi.GetSNRParamsGray(out mean2, out stdev2, out unSnr, out snr, 0.08);

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "ms");

            //this.pbx.Image = jImg.WndLvGray(min, max).ToBitmap();

            Console.WriteLine(string.Format("{0}, {1}", min, max));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string path = @"D:\8bitG.bmp";
            string path = @"D:\샘플 이미지 원본\8bitG.bmp";

            var img = new JImage(path);
            var avg = img.GetHistoAvgGray();
            //var data = KiyLib.DIP.KImage.Convert3Dto1D(img.data8);
            //var dataInt = Array.ConvertAll(data, val => (int)val);

            //var sum = dataInt.Sum();

            Console.WriteLine();


            ////string path = @"D:\새 폴더\8bitG_256.raw";
            //string path = @"D:\새 폴더\[0-1]_[110509].TIF";
            //var img1 = new JImage(path);

            //var data = KiyLib.DIP.KImage.Convert3Dto1D(img1.data16);
            //var dataInt = Array.ConvertAll(data, val => (int)val);


            ////Stopwatch sw = new Stopwatch();
            ////sw.Start();

            //var img2 = new JImage(1280, 1280, KiyLib.DIP.KDepthType.Dt_16, dataInt);

            ////sw.Stop();
            ////Console.WriteLine(sw.ElapsedMilliseconds + "ms");


            //pbx.Image = img2.ToBitmap();
            //img2.Save(@"D:\새 폴더\16test1234.tif");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string path = @"D:\BlobTest2.jpg";
            //string path = @"D:\BlobTest2Thres100.bmp";

            var src = new Image<Gray, byte>(path);
            var cvt2Gray = new Image<Gray, byte>(src.Size);
            var rstOverlay = new Image<Bgr, byte>(src.Size);

            SimpleBlobDetectorParams blobParam = new SimpleBlobDetectorParams();

            CvInvoke.CvtColor(src, cvt2Gray, ColorConversion.Bgr2Gray);
            // 어두운 얼룩 추출 : 0, 밝은 얼룩 추출 : 255
            blobParam.blobColor = 0;

            blobParam.ThresholdStep = 5;
            blobParam.MinThreshold = 100;
            blobParam.MaxThreshold = 255;

            blobParam.MinArea = 50;
            blobParam.MaxArea = 5000;
            blobParam.MinDistBetweenBlobs = 10;

            blobParam.FilterByArea = true;

            blobParam.FilterByConvexity = true;
            blobParam.MinConvexity = 0.87f;

            //p.FilterByCircularity = true;
            //p.MinCircularity = 0.1f;

            //p.FilterByInertia = true;
            //p.MinInertiaRatio = 0.01f;

            SimpleBlobDetector bloBdetector = new SimpleBlobDetector(blobParam);
            MKeyPoint[] findBlobPts = bloBdetector.Detect(src);
            VectorOfKeyPoint rstKeyPts = new Emgu.CV.Util.VectorOfKeyPoint(findBlobPts);

            //Features2DToolbox.DrawKeypoints(src, rstKeyPts, dst3, new Bgr(255, 0, 0), Features2DToolbox.KeypointDrawType.Default);

            var contours = new VectorOfVectorOfPoint();
            var hierarchy = new Mat();

            CvInvoke.FindContours(cvt2Gray, contours, hierarchy, RetrType.Ccomp, ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < contours.Size; i++)
            {
                var contour = contours[i];
                double area = CvInvoke.ContourArea(contour, false);

                var contourRect = CvInvoke.BoundingRectangle(contour);
                var strPath = string.Format(@"D:\contour{0}.bmp", i);
                //CvInvoke.DrawContours(dst3, contours, i, new MCvScalar(0, 0, 255));
                //CvInvoke.Rectangle(dst3, contourRect, new MCvScalar(0, 255, 0));
            }

            //CvInvoke.DrawContours(dst2, contours, 0, new MCvScalar(255));
            //this.pbx.Image = dst3.ToBitmap();

            Console.WriteLine();
        }

        // 색상 분리
        private void button6_Click(object sender, EventArgs e)
        {
            string path = @"d:\24bitC.bmp";

            JImage img = new JImage(path);

            var rst = img.ColorChannelSeparate();

            var bmpB = rst[0].ToBitmap();
            var bmpG = rst[1].ToBitmap();
            var bmpR = rst[2].ToBitmap();
        }

        // 색상 합치기
        private void button7_Click(object sender, EventArgs e)
        {
            string path = @"d:\24bitC.bmp";

            JImage img = new JImage(path);

            var rst = img.ColorChannelSeparate();

            JImage imgCombine = new JImage(img.Width, img.Height, KDepthType.Dt_24);
            var combinedImg = imgCombine.ColorChannelCombine(rst[0], rst[1], rst[2]);


            this.pbx.Image = combinedImg.ToBitmap();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //string path = @"D:\BlobTest2.jpg";
            //string path = @"D:\BlobTest2Thres100.bmp";
            string path = @"D:\BlobTest2_8U.bmp";
            //string path = @"D:\8bitG.bmp";
            //string path = @"D:\BlobTest3.bmp";
            //string path = @"D:\blob.bmp";

            JImage img = new JImage(path);

            var blobList = img.FindBlob(KBinaryColorType.Black, 128);
            var blobImg = img.DrawBlob(blobList, true);

            this.pbx.Image = blobImg.ToBitmap();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            /* var pt1 = new Point(-3, -2);
             var pt2 = new Point(-1, 4);
             var pt3 = new Point(6, 1);
             var pt4 = new Point(3, 10);
             var pt5 = new Point(-4, 9);

             Point[] pts = new Point[5];
             pts[0] = pt1;
             pts[1] = pt2;
             pts[2] = pt3;
             pts[3] = pt4;
             pts[4] = pt5;

             var area2 = KPolygonCV.CalcArea(pts);

             Console.WriteLine();*/

            string path = @"D:\4\8bitG.bmp";

            JImage img = new JImage(path);

            var roi = img.GetROIInfo(new Rectangle(152, 110, 114, 100));

            
            Console.WriteLine(roi.Min);
            Console.WriteLine(roi.Max);
            Console.WriteLine(roi.Mean);
        }
    }
}
