using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouldSpecification
{
    public partial class PicturePopup : Form
    {
        public Image picture { get; set; }

        public PicturePopup(Image picture)
        {
            InitializeComponent();
            this.picture = picture;
            this.pictureBox1.Image = picture;
        }

        public PicturePopup()
        {
            
        }

        private void PicturePopup_Shown(object sender, EventArgs e)
        {
            pictureBox1.LostFocus += pictureBox1_LostFocus;
            this.CenterToParent();
            pictureBox1.Focus();
        }

        private void pictureBox1_LostFocus(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
