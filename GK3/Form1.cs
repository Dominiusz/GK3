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

namespace GK3
{
    public partial class Form1 : Form
    {
        Color current_color=Color.FromArgb(0,0,0);

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
            numericUpDown1.Value = trackBar1.Value;
            label_color.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            numericUpDown2.Value = trackBar2.Value;
            label_color.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            numericUpDown3.Value = trackBar3.Value;
            label_color.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = (int)numericUpDown1.Value;
            label_color.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
        }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            trackBar3.Value = (int)numericUpDown3.Value;
            label_color.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trackBar2.Value = (int)numericUpDown2.Value;
            label_color.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
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

        private void CreateLabLayout()
        {
            throw new NotImplementedException();
        }

        private void CreateYUVLayout()
        {
            throw new NotImplementedException();
        }

        private void CreateXYZLayout()
        {
            throw new NotImplementedException();
        }

        private void CreateHSLLayout()
        {
            throw new NotImplementedException();
        }

        private void CreateHSVLayout()
        {
            throw new NotImplementedException();
        }

        private void CreateYCbCrLayout()
        {
            throw new NotImplementedException();
        }

        private void CreateCMYKLayout()
        {
            trackBar4.Visible = true;
            numericUpDown4.Visible = true;
            label4.Visible = true;

            label1.Text = "C";
            label2.Text = "M";
            label3.Text = "Y";
            label4.Text = "K";
            trackBar1.Maximum = 1;
            trackBar2.Maximum = 1;
            trackBar3.Maximum = 1;
            trackBar4.Maximum = 1;
            trackBar1.Minimum = 0;
            trackBar2.Minimum = 0;
            trackBar3.Minimum = 0;
            trackBar4.Minimum = 0;
            numericUpDown1.Maximum = 1;
            numericUpDown2.Maximum = 1;
            numericUpDown3.Maximum = 1;
            numericUpDown4.Maximum = 1;
            numericUpDown1.Minimum = 0;
            numericUpDown2.Minimum = 0;
            numericUpDown3.Minimum = 0;
            numericUpDown4.Minimum = 0;
        }

        private void CreateRGBLayout()
        {
            label1.Text = "R";
            label2.Text = "G";
            label3.Text = "B";
            trackBar1.Maximum = 255;
            trackBar2.Maximum = 255;
            trackBar3.Maximum = 255;
            trackBar1.Minimum = 0;
            trackBar2.Minimum = 0;
            trackBar3.Minimum = 0;
            numericUpDown1.Maximum = 255;
            numericUpDown2.Maximum = 255;
            numericUpDown3.Maximum = 255;
            numericUpDown1.Minimum = 0;
            numericUpDown2.Minimum = 0;
            numericUpDown3.Minimum = 0;
            Hide4Elements();

        }

        private void Hide4Elements()
        {
            trackBar4.Visible = false;
            numericUpDown4.Visible = false;
            label4.Visible = false;
        }
    }
}
