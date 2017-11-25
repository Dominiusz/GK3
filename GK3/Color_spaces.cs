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
        }

        public class YUV : Color_space
        {
            private double y;
            private double u;
            private double v;

            public static readonly double V_max = 0.615;
            public static readonly double U_max = 0.436;

            public YUV(double y, double u, double v)
            {
                this.y = y;
                this.u = u;
                this.v = v;
            }

            public YUV(Color RGB_color)
            {
                double r = (double)RGB_color.R / 255;
                double g = (double)RGB_color.G / 255;
                double b = (double)RGB_color.B / 255;

                y = 0.299 * r + 0.587 * g + 0.114 * b;
                u = 0.492 * (b - y);
                v = 0.877 * (r - y);
            }

            public double Y
            {
                get { return y; }
                set { y = value; }
            }

            public double U
            {
                get { return u; }
                set { u = value; }
            }

            public double V
            {
                get { return v; }
                set { v = value; }
            }

            public override Color ToRGB()
            {
                double r = Y + 1.14 * V;
                double g = Y - 0.395 * U - 0.581 * V;
                double b = Y + 2.033 * U;

                if (r > 1) r = 1;
                if (g > 1) g = 1;
                if (b > 1) b = 1;
                if (r < 0) r = 0;
                if (g < 0) g = 0;
                if (b < 0) b = 0;

                return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
            }


        }

        public class CMYK : Color_space
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
                int R = (int)(255 * (1 - C) * (1 - K));
                int G = (int)(255 * (1 - M) * (1 - K));
                int B = (int)(255 * (1 - Y) * (1 - K));
                return Color.FromArgb(R, G, B);
            }

            public CMYK(Color RGB_color)
            {
                double r = (double)RGB_color.R / 255;
                double g = (double)RGB_color.G / 255;
                double b = (double)RGB_color.B / 255;

                k = 1 - Math.Max(Math.Max(r, g), b);
                if (Math.Abs(k - 1) < 0.001)
                {
                    c = m = y = 0;
                }
                else
                {
                    c = (1 - r - k) / (1 - k);
                    m = (1 - g - k) / (1 - k);
                    y = (1 - b - k) / (1 - k);
                }
            }
        }
    }
}
