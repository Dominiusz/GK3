using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3
{
    class Color_spaces
    {
        public abstract class Color_space
        {
            public abstract Color ToRGB();
            public abstract Color_space FromRGB(Color RGB_color);
        }

        public class CMYK:Color_space
        {
            public CMYK(double c, double m, double y, double k)
            {
                this.c = c;
                this.m = m;
                this.y = y;
                this.k = k;
            }

            private double c;
            public double C
            {
                get { return c; }
                set
                {
                    if (value > 1 || value < 0)
                        throw new ArgumentOutOfRangeException("CMYK parameters [0,1]");
                    c = value;
                }
            }
            private double m;
            public double M
            {
                get { return m; }
                set
                {
                    if (value > 1 || value < 0)
                        throw new ArgumentOutOfRangeException("CMYK parameters [0,1]");
                    m = value;
                }
            }
            private double y;
            public double Y
            {
                get { return y; }
                set
                {
                    if (value > 1 || value < 0)
                        throw new ArgumentOutOfRangeException("CMYK parameters [0,1]");
                    y = value;
                }
            }
            private double k;

            public double K
            {
                get { return k; }
                set
                {
                    if (value > 1 || value < 0)
                        throw new ArgumentOutOfRangeException("CMYK parameters [0,1]");
                    k = value;
                }
            }

            public override Color ToRGB()
            {
                int R = (int) (255 * (1 - C) * (1 - K));
                int G = (int) (255 * (1 - M) * (1 - K));
                int B = (int)(255 * (1 - Y) * (1 - K));
                return Color.FromArgb(R, G, B);
            }

            public override Color_space FromRGB(Color RGB_color)
            {
                double r = (double)RGB_color.R / 255;
                double g = (double)RGB_color.G / 255;
                double b = (double)RGB_color.B / 255;

                double k = 1 - Math.Max(Math.Max(r, g), b);
                double c = (1 - r - k) / (1 - k);
                double m = (1 - g - k) / (1 - k);
                double y = (1 - b - k) / (1 - k);

                return new CMYK(c,m,y,k);
            }
        }
    }
}
