using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GK3.Color_spaces;

namespace GK3
{
    public partial class Form1 : Form
    {
        Color current_color = Color.FromArgb(0, 0, 0);
        private bool setting_tb_and_nup = false;

        public Form1()
        {
            InitializeComponent();
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            comboBox1.SelectedIndex = 0;

            label4.Visible = false;
            trackBar4.Visible = false;
            numericUpDown4.Visible = false;

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (!setting_tb_and_nup)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            numericUpDown1.Value = trackBar1.Value;
                            current_color = label_color.BackColor =
                                Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
                        }
                        break;
                    case 1:
                        {

                            numericUpDown1.Value = (decimal)trackBar1.Value / 255;
                            current_color = label_color.BackColor =
                                new CMYK((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value, (double)numericUpDown4.Value).ToRGB();
                        }
                        break;
                    case 2:
                        {
                            numericUpDown1.Value = (decimal)trackBar1.Value / 255;
                            current_color = label_color.BackColor =
                                new YCbCr((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 3:
                        CreateHSVLayout();
                        break;
                    case 4:
                        CreateHSLLayout();
                        break;
                    case 5:
                        CreateXYZLayout();
                        break;
                    case 6:
                        {
                            numericUpDown1.Value = (decimal)trackBar1.Value / 255;
                            current_color = label_color.BackColor =
                                new YUV((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 7:
                        CreateLabLayout();
                        break;
                }
                SetLabels();
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (!setting_tb_and_nup)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            numericUpDown2.Value = trackBar2.Value;
                            current_color = label_color.BackColor =
                                Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
                        }
                        break;
                    case 1:
                        {
                            numericUpDown2.Value = (decimal)trackBar2.Value / 255;
                            current_color = label_color.BackColor =
                                new CMYK((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value, (double)numericUpDown4.Value).ToRGB();
                        }
                        break;
                    case 2:
                        {
                            numericUpDown2.Value = (decimal)trackBar2.Value / 255;
                            current_color = label_color.BackColor = new YCbCr((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 3:
                        CreateHSVLayout();
                        break;
                    case 4:
                        CreateHSLLayout();
                        break;
                    case 5:
                        CreateXYZLayout();
                        break;
                    case 6:
                        {
                            numericUpDown2.Value = (decimal)(((double)trackBar2.Value / 255 * 2 * YUV.U_max) - YUV.U_max);
                            current_color = label_color.BackColor =
                                new YUV((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 7:
                        CreateLabLayout();
                        break;
                }
                SetLabels();
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (!setting_tb_and_nup)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            numericUpDown3.Value = trackBar3.Value;
                            current_color = label_color.BackColor =
                                Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
                        }
                        break;
                    case 1:
                        {

                            numericUpDown3.Value = (decimal)trackBar3.Value / 255;
                            current_color = label_color.BackColor =
                                new CMYK((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value, (double)numericUpDown4.Value).ToRGB();
                            SetCMYKLabels();
                        }
                        break;
                    case 2:
                        {
                            numericUpDown3.Value = (decimal)trackBar3.Value / 255;
                            current_color = label_color.BackColor = new YCbCr((double)numericUpDown1.Value,
                                (double)numericUpDown2.Value,
                                (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 3:
                        CreateHSVLayout();
                        break;
                    case 4:
                        CreateHSLLayout();
                        break;
                    case 5:
                        CreateXYZLayout();
                        break;
                    case 6:
                        {
                            numericUpDown3.Value = (decimal)(((double)trackBar3.Value / 255 * 2 * YUV.V_max) - YUV.V_max);
                            current_color = label_color.BackColor =
                                new YUV((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 7:
                        CreateLabLayout();
                        break;
                }
                SetLabels();
            }
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (!setting_tb_and_nup)
            {
                switch (comboBox1.SelectedIndex)
                {
                    //case 0:
                    //{
                    //    numericUpDown1.Value = trackBar1.Value;
                    //    current_color = label_color.BackColor =
                    //        Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
                    //}
                    //    break;
                    case 1:
                        {
                            numericUpDown4.Value = (decimal)trackBar4.Value / 255;
                            current_color = label_color.BackColor =
                                new CMYK((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value, (double)numericUpDown4.Value).ToRGB();
                            SetCMYKLabels();
                        }
                        break;
                    case 2:
                        //CreateYCbCrLayout();
                        break;
                    case 3:
                        CreateHSVLayout();
                        break;
                    case 4:
                        CreateHSLLayout();
                        break;
                    case 5:
                        CreateXYZLayout();
                        break;
                    case 6:
                        // CreateYUVLayout();
                        break;
                    case 7:
                        CreateLabLayout();
                        break;
                }
                SetLabels();
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (!setting_tb_and_nup)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            trackBar1.Value = (int)numericUpDown1.Value;
                            current_color = label_color.BackColor =
                                Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
                        }
                        break;
                    case 1:
                        {
                            trackBar1.Value = (int)(numericUpDown1.Value * 255);
                            current_color = label_color.BackColor =
                                new CMYK((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value, (double)numericUpDown4.Value).ToRGB();
                        }
                        break;
                    case 2:
                        {
                            trackBar1.Value = (int)(numericUpDown1.Value * 255);
                            current_color = label_color.BackColor =
                                new YCbCr((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 3:
                        CreateHSVLayout();
                        break;
                    case 4:
                        CreateHSLLayout();
                        break;
                    case 5:
                        CreateXYZLayout();
                        break;
                    case 6:
                        {
                            trackBar1.Value = (int)(numericUpDown1.Value * 255);
                            current_color = label_color.BackColor =
                                new YUV((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 7:
                        CreateLabLayout();
                        break;
                }
                SetLabels();
            }
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (!setting_tb_and_nup)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            trackBar2.Value = (int)numericUpDown2.Value;
                            current_color = label_color.BackColor =
                                Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
                        }
                        break;
                    case 1:
                        {
                            trackBar2.Value = (int)(numericUpDown2.Value * 255);
                            current_color = label_color.BackColor =
                                new CMYK((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value, (double)numericUpDown4.Value).ToRGB();
                        }
                        break;
                    case 2:
                        {
                            trackBar2.Value = (int)(numericUpDown2.Value * 255);
                            current_color = label_color.BackColor =
                                new YCbCr((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 3:
                        CreateHSVLayout();
                        break;
                    case 4:
                        CreateHSLLayout();
                        break;
                    case 5:
                        CreateXYZLayout();
                        break;
                    case 6:
                        {
                            trackBar2.Value = (int)(((double)numericUpDown2.Value + YUV.U_max) / (2 * YUV.U_max) * 255);
                            current_color = label_color.BackColor =
                                new YUV((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 7:
                        CreateLabLayout();
                        break;

                }
                SetLabels();
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (!setting_tb_and_nup)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            trackBar3.Value = (int)numericUpDown3.Value;
                            current_color = label_color.BackColor =
                                Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
                        }
                        break;
                    case 1:
                        {
                            trackBar3.Value = (int)(numericUpDown3.Value * 255);
                            current_color = label_color.BackColor =
                                new CMYK((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value, (double)numericUpDown4.Value).ToRGB();
                        }
                        break;
                    case 2:
                        {
                            trackBar3.Value = (int)(numericUpDown3.Value * 255);
                            current_color = label_color.BackColor =
                                new YCbCr((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 3:
                        CreateHSVLayout();
                        break;
                    case 4:
                        CreateHSLLayout();
                        break;
                    case 5:
                        CreateXYZLayout();
                        break;
                    case 6:
                        {
                            trackBar3.Value = (int)(((double)numericUpDown3.Value + YUV.V_max) / (2 * YUV.V_max) * 255);
                            current_color = label_color.BackColor =
                                new YUV((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value).ToRGB();
                        }
                        break;
                    case 7:
                        CreateLabLayout();
                        break;
                }
                SetLabels();
            }
        }
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (!setting_tb_and_nup)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            trackBar4.Value = (int)numericUpDown4.Value;
                            current_color = label_color.BackColor =
                                Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
                        }
                        break;
                    case 1:
                        {
                            trackBar4.Value = (int)(numericUpDown4.Value * 255);
                            current_color = label_color.BackColor =
                                new CMYK((double)numericUpDown1.Value, (double)numericUpDown2.Value,
                                    (double)numericUpDown3.Value, (double)numericUpDown4.Value).ToRGB();
                            SetCMYKLabels();
                        }
                        break;
                    case 2:
                        //CreateYCbCrLayout();
                        break;
                    case 3:
                        CreateHSVLayout();
                        break;
                    case 4:
                        CreateHSLLayout();
                        break;
                    case 5:
                        CreateXYZLayout();
                        break;
                    case 6:
                        //CreateYUVLayout();
                        break;
                    case 7:
                        CreateLabLayout();
                        break;
                }
                SetLabels();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    CreateRGBLayout();
                    break;
                case 1:
                    CreateCMYKLayout();
                    break;
                case 2:
                    CreateYCbCrLayout();
                    break;
                case 3:
                    CreateHSVLayout();
                    break;
                case 4:
                    CreateHSLLayout();
                    break;
                case 5:
                    CreateXYZLayout();
                    break;
                case 6:
                    CreateYUVLayout();
                    break;
                case 7:
                    CreateLabLayout();
                    break;
            }

        }

        #region CreatingLayouts

        private void CreateLabLayout()
        {
            //throw new NotImplementedException();
        }

        private void CreateYUVLayout()
        {
            Hide4Elements();

            YUV YUV_color = new YUV(current_color);

            label1.Text = "Y";
            label2.Text = "U";
            label3.Text = "V";

            setting_tb_and_nup = true;

            numericUpDown1.Maximum = 1;
            numericUpDown2.Maximum = (decimal)YUV.U_max;
            numericUpDown3.Maximum = (decimal)YUV.V_max;

            numericUpDown1.Minimum = 0;
            numericUpDown2.Minimum = (decimal)-YUV.U_max;
            numericUpDown3.Minimum = (decimal)-YUV.V_max;

            numericUpDown1.Increment = new decimal(0.01);
            numericUpDown2.Increment = new decimal(0.01);
            numericUpDown3.Increment = new decimal(0.01);


            trackBar1.Value = (int)(YUV_color.Y * 255);
            trackBar2.Value = (int)((YUV_color.U + YUV.U_max) / 2 * YUV.U_max * 255);
            trackBar3.Value = (int)((YUV_color.V + YUV.V_max) / 2 * YUV.V_max * 255);

            setting_tb_and_nup = false;

            numericUpDown1.Value = (decimal)YUV_color.Y;
            numericUpDown2.Value = (decimal)YUV_color.U;
            numericUpDown3.Value = (decimal)YUV_color.V;

        }

        private void CreateXYZLayout()
        {
            //throw new NotImplementedException();
        }

        private void CreateHSLLayout()
        {
            //throw new NotImplementedException();
        }

        private void CreateHSVLayout()
        {
            //throw new NotImplementedException();
        }

        private void CreateYCbCrLayout()
        {
            Hide4Elements();

            label1.Text = "Y";
            label2.Text = "Cb";
            label3.Text = "Cr";
            YCbCr YCbCrColor = new YCbCr(current_color);

            setting_tb_and_nup = true;

            numericUpDown1.Maximum = 1;
            numericUpDown2.Maximum = 1;
            numericUpDown3.Maximum = 1;
            numericUpDown1.Minimum = 0;
            numericUpDown2.Minimum = 0;
            numericUpDown3.Minimum = 0;
            numericUpDown1.Increment = new decimal(0.01);
            numericUpDown2.Increment = new decimal(0.01);
            numericUpDown3.Increment = new decimal(0.01);

            trackBar1.Value = (int)(255 * YCbCrColor.Y);
            trackBar2.Value = (int)(255 * YCbCrColor.Cb);
            trackBar3.Value = (int)(255 * YCbCrColor.Cr);
            numericUpDown1.Value = (decimal)YCbCrColor.Y;
            numericUpDown2.Value = (decimal)YCbCrColor.Cb;
            numericUpDown3.Value = (decimal)YCbCrColor.Cr;

            setting_tb_and_nup = false;

            SetYCbCrLabels();

        }

        private void CreateCMYKLayout()
        {
            Show4Elements();

            CMYK CmykColor = new CMYK(current_color);

            label1.Text = "C";
            label2.Text = "M";
            label3.Text = "Y";
            label4.Text = "K";

            setting_tb_and_nup = true;

            numericUpDown1.Maximum = 1;
            numericUpDown2.Maximum = 1;
            numericUpDown3.Maximum = 1;
            numericUpDown4.Maximum = 1;
            numericUpDown1.Minimum = 0;
            numericUpDown2.Minimum = 0;
            numericUpDown3.Minimum = 0;
            numericUpDown4.Minimum = 0;
            numericUpDown1.Increment = new decimal(0.01);
            numericUpDown2.Increment = new decimal(0.01);
            numericUpDown3.Increment = new decimal(0.01);
            numericUpDown4.Increment = new decimal(0.01);

            trackBar1.Value = (int)(CmykColor.C * 255);
            trackBar2.Value = (int)(CmykColor.M * 255);
            trackBar3.Value = (int)(CmykColor.Y * 255);
            trackBar4.Value = (int)(CmykColor.K * 255);

            setting_tb_and_nup = false;

            numericUpDown1.Value = (decimal)CmykColor.C;
            numericUpDown2.Value = (decimal)CmykColor.M;
            numericUpDown3.Value = (decimal)CmykColor.Y;
            numericUpDown4.Value = (decimal)CmykColor.K;


            SetCMYKLabels(CmykColor);
            //numericUpDown1_ValueChanged(null,null);

        }

        private void CreateRGBLayout()
        {
            setting_tb_and_nup = true;
            label1.Text = "R";
            label2.Text = "G";
            label3.Text = "B";

            numericUpDown1.Maximum = 255;
            numericUpDown2.Maximum = 255;
            numericUpDown3.Maximum = 255;
            numericUpDown1.Minimum = 0;
            numericUpDown2.Minimum = 0;
            numericUpDown3.Minimum = 0;

            numericUpDown1.Increment = 1;
            numericUpDown2.Increment = 1;
            numericUpDown3.Increment = 1;
            

            numericUpDown1.Value = current_color.R;
            numericUpDown2.Value = current_color.G;
            numericUpDown3.Value = current_color.B;

            trackBar1.Value = current_color.R;
            trackBar2.Value = current_color.G;
            trackBar3.Value = current_color.B;
            setting_tb_and_nup = false;

            Hide4Elements();
        }
        #endregion

        private void Hide4Elements()
        {
            trackBar4.Visible = false;
            numericUpDown4.Visible = false;
            label4.Visible = false;
        }

        private void Show4Elements()
        {
            trackBar4.Visible = true;
            numericUpDown4.Visible = true;
            label4.Visible = true;
        }

        #region SettingLabels

        private void SetLabels()
        {
            SetRGBLabels();
            SetCMYKLabels();
            SetYCbCrLabels();
            SetHSVLabels();
            SetHSLLabels();
            SetXYZLabels();
            SetYUVLabels();
            SetLabLabels();
        }

        private void SetCMYKLabels(CMYK cmykColor)
        {
            label_CMYK_C.Text = "C: " + cmykColor.C.ToString("0.000");
            label_CMYK_M.Text = "M: " + cmykColor.M.ToString("0.000");
            label_CMYK_Y.Text = "Y: " + cmykColor.Y.ToString("0.000");
            label_CMYK_K.Text = "K: " + cmykColor.K.ToString("0.000");
        }

        private void SetCMYKLabels()
        {
            CMYK cmykColor = new CMYK(current_color);

            label_CMYK_C.Text = "C: " + cmykColor.C.ToString("0.000");
            label_CMYK_M.Text = "M: " + cmykColor.M.ToString("0.000");
            label_CMYK_Y.Text = "Y: " + cmykColor.Y.ToString("0.000");
            label_CMYK_K.Text = "K: " + cmykColor.K.ToString("0.000");
        }

        private void SetLabLabels()
        {
            //throw new NotImplementedException();
        }

        private void SetYUVLabels()
        {
            YUV yuvColor = new YUV(current_color);

            label_YUV_Y.Text = "Y: " + yuvColor.Y.ToString("0.000");
            label_YUV_U.Text = "U: " + yuvColor.U.ToString("0.000");
            label_YUV_V.Text = "V: " + yuvColor.V.ToString("0.000");
        }

        private void SetXYZLabels()
        {
            //throw new NotImplementedException();
        }

        private void SetHSLLabels()
        {
            //throw new NotImplementedException();
        }

        private void SetHSVLabels()
        {
            //throw new NotImplementedException();
        }

        private void SetYCbCrLabels()
        {
            YCbCr yCbCrColor = new YCbCr(current_color);

            label_YCbCr_Y.Text = "Y: " + yCbCrColor.Y.ToString("0.000");
            label_YCbCr_Cb.Text = "Cb: " + yCbCrColor.Cb.ToString("0.000");
            label_YCbCr_Cr.Text = "Cr: " + yCbCrColor.Cr.ToString("0.000");

        }

        private void SetRGBLabels()
        {
            label_RGB_R.Text = "R: " + current_color.R;
            label_RGB_G.Text = "G: " + current_color.G;
            label_RGB_B.Text = "B: " + current_color.B;
        }
        #endregion

    }
}
