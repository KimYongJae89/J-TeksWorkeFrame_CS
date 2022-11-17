using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.ExtensionMethod
{
    /// <summary>
    /// Shape에 관련된 클래스들(Point, Rectangle 등)의 확장메서드 구현을 위한 클래스
    /// </summary>
    public static class ShapeExt
    {
        /// <summary>
        /// Rectangle의 중심 좌표를 반환한다
        /// </summary>
        /// <param name="rect">대상 Rectangle</param>
        /// <returns>중심 좌표</returns>
        public static Point Center(this Rectangle rect)
        {
            int w = rect.Width;
            int h = rect.Height;

            w = (w % 2) == 0 ? w : w + 1;
            h = (h % 2) == 0 ? h : h + 1;

            return new Point(rect.Left + (w / 2),
                             rect.Top + (h / 2));
        }
    }
}
