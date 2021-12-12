using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayTracing
{
    public partial class FormResult : Form
    {
        Bitmap bitmap;
        public FormResult(Bitmap bitmap)
        {
            InitializeComponent();
            this.bitmap = bitmap;
            pictureBox.Image = bitmap;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
