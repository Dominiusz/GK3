using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3
{
    class ColorSpaces
    {
        public abstract class ColorSpace
        {
            public abstract Color ToRGB();
        }

        public class YCbCr : ColorSpace
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

        public class YUV : ColorSpace
        {
            private double y;
            private double u;
            private double v;

            public static readonly double V_max = 0.615;
            public static readonly double U_max = 0.436;

            public YUV(double y, double u, double v)
            {
                if (y < 0 || y > 1 || v < -V_max || v > V_max || u < -U_max || u > U_max)
                    throw new ArgumentOutOfRangeException();

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

        public class HSL : ColorSpace
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

        public class HSV : ColorSpace
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

        public class XYZ : ColorSpace
        {
            private double x;
            private double y;
            private double z;

            public double X => x;

            public double Y => y;

            public double Z => z;

            public XYZ(double x, double y, double z)
            {
                if (x < 0 || y < 0 || z < 0 || x > 0.9505 || y > 1 || z > 1.089)
                    throw new ArgumentOutOfRangeException();

                this.x = x;
                this.y = y;
                this.z = z;
            }

            public XYZ(Color RGB_color)
            {
                double rLinear = RGB_color.R / 255.0;
                double gLinear = RGB_color.G / 255.0;
                double bLinear = RGB_color.B / 255.0;

                double r = (rLinear > 0.04045) ? Math.Pow((rLinear + 0.055) / (1 + 0.055), 2.2) : (rLinear / 12.92);
                double g = (gLinear > 0.04045) ? Math.Pow((gLinear + 0.055) / (1 + 0.055), 2.2) : (gLinear / 12.92);
                double b = (bLinear > 0.04045) ? Math.Pow((bLinear + 0.055) / (1 + 0.055), 2.2) : (bLinear / 12.92);

                x = r * 0.4124 + g * 0.3576 + b * 0.1805;
                y = r * 0.2126 + g * 0.7152 + b * 0.0722;
                z = r * 0.0193 + g * 0.1192 + b * 0.9505;

            }

            public override Color ToRGB()
            {
                double[] Clinear = new double[3];
                Clinear[0] = x * 3.2410 - y * 1.5374 - z * 0.4986; // red
                Clinear[1] = -x * 0.9692 + y * 1.8760 - z * 0.0416; // green
                Clinear[2] = x * 0.0556 - y * 0.2040 + z * 1.0570; // blue

                for (int i = 0; i < 3; i++)
                {
                    Clinear[i] = (Clinear[i] <= 0.0031308) ? 12.92 * Clinear[i] : (1 + 0.055) * Math.Pow(Clinear[i], (1.0 / 2.4)) - 0.055;
                }

                int r = Convert.ToInt32(Clinear[0] * 255.0);
                int g = Convert.ToInt32(Clinear[1] * 255.0);
                int b = Convert.ToInt32(Clinear[2] * 255.0);

                if (r > 255) r = 255;
                if (g > 255) g = 255;
                if (b > 255) b = 255;
                if (r < 0) r = 0;
                if (g < 0) g = 0;
                if (b < 0) b = 0;

                return Color.FromArgb(r, g, b);
            }
        }

        public class CMYK : ColorSpace
        {
            public CMYK(double c, double m, double y, double k)
            {
                if (c < 0 || c > 1 || m < 0 || m > 1 || y < 0 || y > 1 || k < 0 || k > 1)
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

        public class Lab : ColorSpace
        {
            private double l;
            private double a;
            private double b;

            public double L => l;

            public double A => a;

            public double B => b;

            public Lab(double l, double a, double b)
            {
                if (l < 0 || l > 100 || a > 98 || a < -86 || b > 94 || b < -107)
                    throw new ArgumentOutOfRangeException();

                this.l = l;
                this.a = a;
                this.b = b;
            }

            public Lab(Color RGB_color)
            {

                double r = RGB_color.R / 255.0;
                double g = RGB_color.G / 255.0;
                double b1 = RGB_color.B / 255.0;
                double x, y, z;

                r = (r > 0.04045) ? Math.Pow((r + 0.055) / 1.055, 2.4) : r / 12.92;
                g = (g > 0.04045) ? Math.Pow((g + 0.055) / 1.055, 2.4) : g / 12.92;
                b1 = (b1 > 0.04045) ? Math.Pow((b1 + 0.055) / 1.055, 2.4) : b1 / 12.92;

                x = (r * 0.4124 + g * 0.3576 + b1 * 0.1805) / 0.95047;
                y = (r * 0.2126 + g * 0.7152 + b1 * 0.0722) / 1.00000;
                z = (r * 0.0193 + g * 0.1192 + b1 * 0.9505) / 1.08883;

                x = (x > 0.008856) ? Math.Pow(x, 1 / 3.0) : (7.787 * x) + 16 / 116.0;
                y = (y > 0.008856) ? Math.Pow(y, 1 / 3.0) : (7.787 * y) + 16 / 116.0;
                z = (z > 0.008856) ? Math.Pow(z, 1 / 3.0) : (7.787 * z) + 16 / 116.0;

                l = 116 * y - 16;
                a = 500 * (x - y);
                b = 200 * (y - z);

                a = Math.Min(98, Math.Max(a, -86));
                b = Math.Min(94, Math.Max(b, -107));
            }

            public override Color ToRGB()
            {
                double y = (l + 16) / 116;
                double x = a / (500 + y);
                double z = y - b / 500;
                double r, g, b1;

                x = 0.95047 * ((x * x * x > 0.008856) ? x * x * x : (x - 16 / 116.0) / 7.787);
                y = 1.00000 * ((y * y * y > 0.008856) ? y * y * y : (y - 16 / 116.0) / 7.787);
                z = 1.08883 * ((z * z * z > 0.008856) ? z * z * z : (z - 16 / 116.0) / 7.787);

                r = x * 3.2406 + y * -1.5372 + z * -0.4986;
                g = x * -0.9689 + y * 1.8758 + z * 0.0415;
                b1 = x * 0.0557 + y * -0.2040 + z * 1.0570;

                r = (r > 0.0031308) ? (1.055 * Math.Pow(r, 1 / 2.4) - 0.055) : 12.92 * r;
                g = (g > 0.0031308) ? (1.055 * Math.Pow(g, 1 / 2.4) - 0.055) : 12.92 * g;
                b1 = (b1 > 0.0031308) ? (1.055 * Math.Pow(b1, 1 / 2.4) - 0.055) : 12.92 * b1;

                r = Math.Max(0, Math.Min(1, r)) * 255;
                g = Math.Max(0, Math.Min(1, g)) * 255;
                b1 = Math.Max(0, Math.Min(1, b1)) * 255;

                return Color.FromArgb((int)r, (int)g, (int)b1);
            }
        }
    }
}
