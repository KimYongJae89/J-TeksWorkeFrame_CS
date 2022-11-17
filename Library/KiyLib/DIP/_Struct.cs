using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KiyLib.DIP
{
    /// <summary>
    /// 칼라 이미지 구조체 (byte)
    /// </summary>
    public struct KColor
    {
        public byte B, G, R;

        public KColor(byte B, byte G, byte R)
        {
            this.B = B;
            this.G = G;
            this.R = R;
        }
    }

    /// <summary>
    /// 칼라 이미지 구조체 (float)
    /// </summary>
    public struct KColorF
    {
        public float B, G, R;

        public KColorF(float B, float G, float R)
        {
            this.B = B;
            this.G = G;
            this.R = R;
        }

        public static implicit operator KColorF(KColor clr)
        {
            return new KColorF(clr.B, clr.G, clr.R);
        }

        public static KColorF operator +(KColorF clr1, KColorF clr2)
        {
            var b = (float)Math.Round(clr1.B + clr2.B, 2);
            var g = (float)Math.Round(clr1.G + clr2.G, 2);
            var r = (float)Math.Round(clr1.R + clr2.R, 2);

            return new KColorF(b, g, r);
        }

        public static KColorF operator -(KColorF clr1, KColorF clr2)
        {
            var b = (float)Math.Round(clr1.B - clr2.B, 2);
            var g = (float)Math.Round(clr1.G - clr2.G, 2);
            var r = (float)Math.Round(clr1.R - clr2.R, 2);

            return new KColorF(b, g, r);
        }
    }
}
