using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                button2.Enabled = true; 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap[] bimp = new Bitmap[openFileDialog1.SafeFileNames.Length];
            for (int i = 0; i < bimp.Length; i++)
            {
                bimp[i] = new Bitmap(openFileDialog1.FileNames[i]);
            }
            Bitmap resutl = new Bitmap(bimp[0].Width * bimp.Length, bimp[1].Height);
            Graphics q = Graphics.FromImage(resutl);
         //   q.ScaleTransform(0.5f, 0.5f); 
            for (int i = 0; i < bimp.Length; i++)
            {
                q.DrawImage((Image)bimp[i], new Point(bimp[i].Width * i, 0));  
            }
            q.Flush();
            if ((!checkBox1.Checked) && (saveFileDialog1.FileName != null))
            {
                resutl.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                resutl.Save(openFileDialog1.FileNames[0] + "RESULT.png", System.Drawing.Imaging.ImageFormat.Png);  
            }
            MessageBox.Show("Готово"); 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                saveFileDialog1.ShowDialog(); 
            }
        }
    }

}
