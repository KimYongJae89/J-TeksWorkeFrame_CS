using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiyLib.DIP
{
    /// <summary>
    /// 현재는 사용되지 않는다
    /// </summary>
    public class KDisplay : KDisplayBase, IKDisplay
    {
        public KDisplay()
        {
            CrosshairColor = Color.Red;
        }

        // IDsiplay 
        public void RenderDisplay(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //g.DrawImage(Image, 0, 0);

            //g.draw

            if (useCrossHair)
                DrawCrosshair(g);
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            var pt = KImage.ConvertToRelativePoint(new Point(e.X, e.Y), ZoomFactor);

            string xStr = pt.X.ToString(pt.X % 1 == 0 ? "F0" : "F2");
            string yStr = pt.Y.ToString(pt.Y % 1 == 0 ? "F0" : "F2");

            Console.WriteLine("{0}, {1}", xStr, yStr);
        }

        public void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)    //+
            {
                if (zoomFactor > 10)
                    return;

                ZoomFactor += 0.1f;
            }

            if (e.Delta < 0)    //-
            {
                if (zoomFactor < 0.1)
                    return;

                ZoomFactor -= 0.1f;
            }

            Zoom(ZoomFactor);
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
                isShiftPressed = true;
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
                isShiftPressed = false;
        }
    }
}
