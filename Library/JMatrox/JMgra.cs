using Matrox.MatroxImagingLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMatrox
{
    public static class KMgra
    {
        public static void SetColor(double color)
        {
            MIL.MgraColor(MIL.M_DEFAULT, color);
        }

        public static void SetBackColor(double color)
        {
            MIL.MgraBackColor(MIL.M_DEFAULT, color);
        }


        public static void DrawText(MIL_ID dstImgBufOrListGraID, int x, int y, string str)
        {
            DrawText(dstImgBufOrListGraID, new Point(x, y), str);
        }

        public static void DrawText(MIL_ID dstImgBufOrListGraID, Point Position, string str)
        {
            MIL.MgraText(MIL.M_DEFAULT, dstImgBufOrListGraID, Position.X, Position.Y, str);
        }


        public static void DrawRect(MIL_ID dstImgBufOrListGraID, Rectangle roi)
        {
            DrawRect(dstImgBufOrListGraID, roi.X, roi.Y, roi.X + roi.Width, roi.Y + roi.Height);
        }

        public static void DrawRect(MIL_ID dstImgBufOrListGraID, int xStart, int yStart, int xEnd, int yEnd)
        {
            MIL.MgraRect(MIL.M_DEFAULT, dstImgBufOrListGraID, xStart, yStart, xEnd, yEnd);
        }
    }
}
