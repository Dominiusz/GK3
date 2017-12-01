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

        public class YCbCr : Color_space
        {
            private double y;
            private double cb;
            private double cr;


            public double Y => y;

            public double Cb => cb;

            public double Cr => cr;

            public YCbCr(double y, double cb, double cr)
            {
                if (y > 1 || y < 0 || cb < 0 || cb > 1 || cr < 0 || cr > 1)
                    throw new ArgumentOutOfRangeException();

                this.y = y;
                this.cb = cb;
                this.cr = cr;
            }

            public YCbCr(Color RGB_color)
            {
                double r = (double)RGB_color.R / 255;
                double g = (double)RGB_color.G / 255;
                double b = (double)RGB_color.B / 255;

                y = 0.299 * r + 0.587 * g + 0.114 * b;
                cb = (b - y) / 1.772 + 0.5;
                cr = (r - y) / 1.402 + 0.5;
            }

            public override Color ToRGB()
            {
                double U = (cb - 0.5) * 0.492 * 1.772;
                double V = (cr - 0.5) * 0.877 * 1.402;

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

            public double Y => y;

            public double U => u;

            public double V => v;

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

        public class HSL : Color_space
        {
            private double h;
            private double s;
            private double l;

            public double H => h;

            public double S => s;

            public double L => l;

            public HSL(double h, double s, double l)
            {
                if (h < 0 || h > 360 || s < 0 || s > 1 || l < 0 || l > 1)
                    throw new ArgumentOutOfRangeException();

                this.h = h;
                this.s = s;
                this.l = l;
            }

            public HSL(Color RGB_color)
            {
                double r = (double)RGB_color.R / 255;
                double g = (double)RGB_color.G / 255;
                double b = (double)RGB_color.B / 255;
                double C_max = Math.Max(Math.Max(r, g), b);
                double C_min = Math.Min(Math.Min(r, g), b);
                double delta = C_max - C_min;

                if (Math.Abs(delta) < 0.000001)
                {
                    h = 0;
                }
                else if (C_max == r)
                {
                    h = 60 * (((g - b) / delta + 6) % 6);
                }
                else if (C_max == g)
                {
                    h = 60 * ((b - r) / delta + 2);
                }
                else
                {
                    h = 60 * ((r - g) / delta + 4);
                }

                l = (C_max + C_min) / 2;

                if (Math.Abs(delta) < 0.000001)
                {
                    s = 0;
                }
                else
                {
                    s = delta / (1 - Math.Abs(2 * l - 1));
                }
            }

            public override Color ToRGB()
            {
                double C = (1 - Math.Abs(2 * L - 1)) * S;
                double X = C * (1 - Math.Abs((H / 60) % 2 - 1));
                double m = L - C / 2;

                double r = 0, g = 0, b = 0;

                if (0 <= h && h < 60)
                {
                    r = C;
                    g = X;
                }
                else if (60 <= h && h < 120)
                {
                    r = X;
                    g = C;
                }
                else if (120 <= h && h < 180)
                {
                    g = C;
                    b = X;
                }
                else if (180 <= h && h < 240)
                {
                    g = X;
                    b = C;
                }
                else if (240 <= h && h < 300)
                {
                    r = X;
                    b = C;
                }
                else
                {
                    r = C;
                    b = X;
                }

                return Color.FromArgb((int)((r + m) * 255), (int)((g + m) * 255), (int)((b + m) * 255));

            }
        }

        public class HSV : Color_space
        {
            private double h;
            private double s;
            private double v;

            public double H => h;

            public double S => s;

            public double V => v;

            public HSV(double h, double s, double v)
            {
                if (h < 0 || h > 360 || s < 0 || s > 1 || v < 0 || v > 1)
                    throw new ArgumentOutOfRangeException();

                this.h = h;
                this.s = s;
                this.v = v;
            }

            public HSV(Color RGB_color)
            {
                double r = (double)RGB_color.R / 255;
                double g = (double)RGB_color.G / 255;
                double b = (double)RGB_color.B / 255;
                double C_max = Math.Max(Math.Max(r, g), b);
                double C_min = Math.Min(Math.Min(r, g), b);
                double delta = C_max - C_min;

                if (Math.Abs(delta) < 0.000001)
                {
                    h = 0;
                }
                else if (C_max == r)
                {
                    h = 60 * (((g - b) / delta+6)% 6);
                }
                else if (C_max == g)
                {
                    h = 60 * ((b - r) / delta + 2);
                }
                else
                {
                    h = 60 * ((r - g) / delta + 4);
                }

                if (Math.Abs(C_max) < 0.000001)
                {
                    s = 0;
                }
                else
                {
                    s = delta / C_max;
                }
                v = C_max;
            }

            public override Color ToRGB()
            {
                double C = v * s;
                double X = C * (1 - Math.Abs((h / 60) % 2 - 1));
                double m = V - C;

                double r=0, g=0, b = 0;

                if (0 <= h && h < 60)
                {
                    r = C;
                    g = X;
                }
                else if (60 <= h && h < 120)
                {
                    r = X;
                    g = C;
                }
                else if (120 <= h && h < 180)
                {
                    g = C;
                    b = X;
                }
                else if (180 <= h && h < 240)
                {
                    g = X;
                    b = C;
                }
                else if (240 <= h && h < 300)
                {
                    r = X;
                    b = C;
                }
                else
                {
                    r = C;
                    b = X;
                }

                return Color.FromArgb((int) ((r + m) * 255), (int)((g + m) * 255), (int)((b + m) * 255));

            }
        }

        public class CMYK : Color_space
        {
            public CMYK(double c, double m, double y, double k)
            {
                if(c<0||c>1||m<0||m>1||y<0||y>1||k<0||k>1)
                    throw new ArgumentOutOfRangeException();

                this.c = c;
                this.m = m;
                this.y = y;
                this.k = k;
            }

            private double c;
            
            private double m;
           
            private double y;

            private double k;

            public double C => c;

            public double M => m;

            public double Y => y;

            public double K => k;

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
