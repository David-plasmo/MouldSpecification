using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Utils;

namespace MouldSpecification
{
    public partial class PackagingAttachment : Form
    {
        PackagingDC dc = null;
        public PackagingAttachment(PackagingDC dc)
        {
            InitializeComponent();
            this.dc = dc;
        }

        public PackagingAttachment()
        {
           
        }

        private void PackagingAttachment_Load(object sender, EventArgs e)
        {
            this.btnPackingImage1.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnPackingImage1.Height - 8, btnPackingImage1.Height - 8);
            this.btnPackingImage1.ImageAlign = ContentAlignment.MiddleRight;
            this.btnPackingImage2.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnPackingImage2.Height - 8, btnPackingImage2.Height - 8);
            this.btnPackingImage2.ImageAlign = ContentAlignment.MiddleRight;
            this.btnPackingImage3.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnPackingImage3.Height - 8, btnPackingImage3.Height - 8);
            this.btnPackingImage3.ImageAlign = ContentAlignment.MiddleRight;
            this.btnAssemblyImage1.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnAssemblyImage1.Height - 8, btnAssemblyImage1.Height - 8);
            this.btnAssemblyImage1.ImageAlign = ContentAlignment.MiddleRight;
            this.btnAssemblyImage2.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnAssemblyImage2.Height - 8, btnAssemblyImage2.Height - 8);
            this.btnAssemblyImage2.ImageAlign = ContentAlignment.MiddleRight;
            this.btnAssemblyImage3.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnAssemblyImage3.Height - 8, btnAssemblyImage3.Height - 8);
            this.btnAssemblyImage3.ImageAlign = ContentAlignment.MiddleRight;
            this.btnAssemblyImage4.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnAssemblyImage4.Height - 8, btnAssemblyImage4.Height - 8);
            this.btnAssemblyImage4.ImageAlign = ContentAlignment.MiddleRight;
            this.btnAssemblyImage5.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnAssemblyImage5.Height - 8, btnAssemblyImage5.Height - 8);
            this.btnAssemblyImage5.ImageAlign = ContentAlignment.MiddleRight;
            this.btnAssemblyImage6.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnAssemblyImage6.Height - 8, btnAssemblyImage6.Height - 8);
            this.btnAssemblyImage6.ImageAlign = ContentAlignment.MiddleRight;

            linkPackingImage1.TextAlign = ContentAlignment.MiddleLeft;
            linkPackingImage1.FlatStyle = FlatStyle.Flat;
            linkPackingImage2.TextAlign = ContentAlignment.MiddleLeft;
            linkPackingImage2.FlatStyle = FlatStyle.Flat;
            linkPackingImage3.TextAlign = ContentAlignment.MiddleLeft;
            linkPackingImage3.FlatStyle = FlatStyle.Flat;
            linkAssemblyImage1.TextAlign = ContentAlignment.MiddleLeft;
            linkAssemblyImage1.FlatStyle = FlatStyle.Flat;
            linkAssemblyImage2.TextAlign = ContentAlignment.MiddleLeft;
            linkAssemblyImage2.FlatStyle = FlatStyle.Flat;
            linkAssemblyImage3.TextAlign = ContentAlignment.MiddleLeft;
            linkAssemblyImage3.FlatStyle = FlatStyle.Flat;
            linkAssemblyImage4.TextAlign = ContentAlignment.MiddleLeft;
            linkAssemblyImage4.FlatStyle = FlatStyle.Flat;
            linkAssemblyImage5.TextAlign = ContentAlignment.MiddleLeft;
            linkAssemblyImage5.FlatStyle = FlatStyle.Flat;
            linkAssemblyImage6.TextAlign = ContentAlignment.MiddleLeft;
            linkAssemblyImage6.FlatStyle = FlatStyle.Flat;
            BindControls();
        }

        private void BindControls()
        {
            //string filePath = dc.PackingImage1;
            //linkPackingImage1.Text = filePath;
            //if (File.Exists(filePath))
            //    picPackingImage1.Image = DrawingUtils.GetImage(filePath, picPackingImage1.Width, picPackingImage1.Height);

            //filePath = dc.PackingImage2;
            //linkPackingImage2.Text = filePath;
            //if (File.Exists(filePath))
            //    picPackingImage2.Image = DrawingUtils.GetImage(filePath, picPackingImage2.Width, picPackingImage2.Height);

            //filePath = dc.PackingImage3;
            //linkPackingImage3.Text = filePath;
            //if (File.Exists(filePath))
            //    picPackingImage3.Image = DrawingUtils.GetImage(filePath, picPackingImage3.Width, picPackingImage3.Height);

            //filePath = dc.AssemblyImage1;
            //linkAssemblyImage1.Text = filePath;
            //if (File.Exists(filePath))
            //    picAssemblyImage1.Image = DrawingUtils.GetImage(filePath, picAssemblyImage1.Width, picAssemblyImage1.Height);

            //filePath = dc.AssemblyImage2;
            //linkAssemblyImage2.Text = filePath;
            //if (File.Exists(filePath))
            //    picAssemblyImage2.Image = DrawingUtils.GetImage(filePath, picAssemblyImage2.Width, picAssemblyImage1.Height);

            //filePath = dc.AssemblyImage3;
            //linkAssemblyImage3.Text = filePath;
            //if (File.Exists(filePath))
            //    picAssemblyImage3.Image = DrawingUtils.GetImage(filePath, picAssemblyImage3.Width, picAssemblyImage3.Height);

            //filePath = dc.AssemblyImage4;
            //linkAssemblyImage4.Text = filePath;
            //if (File.Exists(filePath))
            //    picAssemblyImage4.Image = DrawingUtils.GetImage(filePath, picAssemblyImage4.Width, picAssemblyImage4.Height);

            //filePath = dc.AssemblyImage5;
            //linkAssemblyImage5.Text = filePath;
            //if (File.Exists(filePath))
            //    picAssemblyImage5.Image = DrawingUtils.GetImage(filePath, picAssemblyImage5.Width, picAssemblyImage5.Height);

            //filePath = dc.AssemblyImage6;
            //linkAssemblyImage6.Text = filePath;
            //if (File.Exists(filePath))
            //    picAssemblyImage6.Image = DrawingUtils.GetImage(filePath, picAssemblyImage6.Width, picAssemblyImage6.Height);

        }

        private void btnPackingImage1_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            var appSettings = ConfigurationManager.AppSettings;
            string initialDir = appSettings["QCImageFolderDir"];
            ofd.InitialDirectory = initialDir;
            if (!ofd.CheckPathExists)
            {
                MessageBox.Show("Initial folder not found");
                ofd.InitialDirectory = "C:\\";
            }
            ofd.Filter = "jpg files (*.jpg)|*.jpg|tin files (*.tin)|*.tin|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkPackingImage1.Text = filePath;
                picPackingImage1.Image = DrawingUtils.GetImage(filePath, picPackingImage1.Width, picPackingImage1.Height);

            }
        }

        private void btnPackingImage2_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            var appSettings = ConfigurationManager.AppSettings;
            string initialDir = appSettings["QCImageFolderDir"];
            ofd.InitialDirectory = initialDir;
            if (!ofd.CheckPathExists)
            {
                MessageBox.Show("Initial folder not found");
                ofd.InitialDirectory = "C:\\";
            }
            ofd.Filter = "jpg files (*.jpg)|*.jpg|tin files (*.tin)|*.tin|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkPackingImage2.Text = filePath;
                picPackingImage2.Image = DrawingUtils.GetImage(filePath, picPackingImage2.Width, picPackingImage2.Height);

            }
        }

        private void btnPackingImage3_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            var appSettings = ConfigurationManager.AppSettings;
            string initialDir = appSettings["QCImageFolderDir"];
            ofd.InitialDirectory = initialDir;
            if (!ofd.CheckPathExists)
            {
                MessageBox.Show("Initial folder not found");
                ofd.InitialDirectory = "C:\\";
            }
            ofd.Filter = "jpg files (*.jpg)|*.jpg|tin files (*.tin)|*.tin|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkPackingImage3.Text = filePath;
                picPackingImage3.Image = DrawingUtils.GetImage(filePath, picPackingImage3.Width, picPackingImage3.Height);

            }
        }

        private void btnAssemblyImage1_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            var appSettings = ConfigurationManager.AppSettings;
            string initialDir = appSettings["QCImageFolderDir"];
            ofd.InitialDirectory = initialDir;
            if (!ofd.CheckPathExists)
            {
                MessageBox.Show("Initial folder not found");
                ofd.InitialDirectory = "C:\\";
            }
            ofd.Filter = "jpg files (*.jpg)|*.jpg|tin files (*.tin)|*.tin|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkAssemblyImage1.Text = filePath;
                picAssemblyImage1.Image = DrawingUtils.GetImage(filePath, picAssemblyImage1.Width, picAssemblyImage1.Height);
            }
        }

        private void btnAssemblyImage2_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            var appSettings = ConfigurationManager.AppSettings;
            string initialDir = appSettings["QCImageFolderDir"];
            ofd.InitialDirectory = initialDir;
            if (!ofd.CheckPathExists)
            {
                MessageBox.Show("Initial folder not found");
                ofd.InitialDirectory = "C:\\";
            }
            ofd.Filter = "jpg files (*.jpg)|*.jpg|tin files (*.tin)|*.tin|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkAssemblyImage2.Text = filePath;
                picAssemblyImage2.Image = DrawingUtils.GetImage(filePath, picAssemblyImage2.Width, picAssemblyImage2.Height);
            }
        }

        private void btnAssemblyImage3_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            var appSettings = ConfigurationManager.AppSettings;
            string initialDir = appSettings["QCImageFolderDir"];
            ofd.InitialDirectory = initialDir;
            if (!ofd.CheckPathExists)
            {
                MessageBox.Show("Initial folder not found");
                ofd.InitialDirectory = "C:\\";
            }
            ofd.Filter = "jpg files (*.jpg)|*.jpg|tin files (*.tin)|*.tin|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkAssemblyImage3.Text = filePath;
                picAssemblyImage3.Image = DrawingUtils.GetImage(filePath, picAssemblyImage3.Width, picAssemblyImage3.Height);
            }
        }

        private void btnAssemblyImage4_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            var appSettings = ConfigurationManager.AppSettings;
            string initialDir = appSettings["QCImageFolderDir"];
            ofd.InitialDirectory = initialDir;
            if (!ofd.CheckPathExists)
            {
                MessageBox.Show("Initial folder not found");
                ofd.InitialDirectory = "C:\\";
            }
            ofd.Filter = "jpg files (*.jpg)|*.jpg|tin files (*.tin)|*.tin|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkAssemblyImage4.Text = filePath;
                picAssemblyImage4.Image = DrawingUtils.GetImage(filePath, picAssemblyImage4.Width, picAssemblyImage4.Height);
            }
        }

        private void btnAssemblyImage5_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            var appSettings = ConfigurationManager.AppSettings;
            string initialDir = appSettings["QCImageFolderDir"];
            ofd.InitialDirectory = initialDir;
            if (!ofd.CheckPathExists)
            {
                MessageBox.Show("Initial folder not found");
                ofd.InitialDirectory = "C:\\";
            }
            ofd.Filter = "jpg files (*.jpg)|*.jpg|tin files (*.tin)|*.tin|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkAssemblyImage5.Text = filePath;
                picAssemblyImage5.Image = DrawingUtils.GetImage(filePath, picAssemblyImage5.Width, picAssemblyImage5.Height);
            }
        }

        private void btnAssemblyImage6_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            var appSettings = ConfigurationManager.AppSettings;
            string initialDir = appSettings["QCImageFolderDir"];
            ofd.InitialDirectory = initialDir;
            if (!ofd.CheckPathExists)
            {
                MessageBox.Show("Initial folder not found");
                ofd.InitialDirectory = "C:\\";
            }
            ofd.Filter = "jpg files (*.jpg)|*.jpg|tin files (*.tin)|*.tin|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkAssemblyImage6.Text = filePath;
                picAssemblyImage6.Image = DrawingUtils.GetImage(filePath, picAssemblyImage6.Width, picAssemblyImage6.Height);
            }
        }

        private void linkPackingImage1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkPackingImage1.LinkVisited = true;
            System.Diagnostics.Process.Start(linkPackingImage1.Text);
        }

        private void linkPackingImage2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkPackingImage2.LinkVisited = true;
            System.Diagnostics.Process.Start(linkPackingImage2.Text);
        }

        private void linkPackingImage3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkPackingImage3.LinkVisited = true;
            System.Diagnostics.Process.Start(linkPackingImage3.Text);
        }

        private void linkAssemblyImage1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkAssemblyImage1.LinkVisited = true;
            System.Diagnostics.Process.Start(linkAssemblyImage1.Text);
        }

        private void linkAssemblyImage2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkAssemblyImage2.LinkVisited = true;
            System.Diagnostics.Process.Start(linkAssemblyImage2.Text);
        }

        private void linkAssemblyImage3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkAssemblyImage3.LinkVisited = true;
            System.Diagnostics.Process.Start(linkAssemblyImage3.Text);
        }

        private void linkAssemblyImage4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkAssemblyImage4.LinkVisited = true;
            System.Diagnostics.Process.Start(linkAssemblyImage4.Text);
        }

        private void linkAssemblyImage5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkAssemblyImage5.LinkVisited = true;
            System.Diagnostics.Process.Start(linkAssemblyImage5.Text);
        }

        private void linkAssemblyImage6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkAssemblyImage6.LinkVisited = true;
            System.Diagnostics.Process.Start(linkAssemblyImage6.Text);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            ////pass edits back to parent form
            //dc.PackingImage1 = linkPackingImage1.Text;
            //dc.PackingImage2 = linkPackingImage2.Text;
            //dc.PackingImage3 = linkPackingImage3.Text;
            //dc.AssemblyImage1 = linkAssemblyImage1.Text;
            //dc.AssemblyImage2 = linkAssemblyImage2.Text;
            //dc.AssemblyImage3 = linkAssemblyImage3.Text;
            //dc.AssemblyImage4 = linkAssemblyImage4.Text;
            //dc.AssemblyImage5 = linkAssemblyImage5.Text;
            //dc.AssemblyImage6 = linkAssemblyImage6.Text;

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void chkPackagingImage1_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkPackagingImage1.Checked)
            //{
            //    linkPackingImage1.Text = null;
            //    picPackingImage1.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.PackingImage1;
            //    linkPackingImage1.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picPackingImage1.Image = DrawingUtils.GetImage(filePath, picPackingImage1.Width, picPackingImage1.Height);
            //}
        }

        private void chkPackingImage2_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkPackingImage2.Checked)
            //{
            //    linkPackingImage2.Text = null;
            //    picPackingImage2.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.PackingImage2;
            //    linkPackingImage2.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picPackingImage2.Image = DrawingUtils.GetImage(filePath, picPackingImage2.Width, picPackingImage2.Height);
            //}
        }

        private void chkPackingImage3_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkPackingImage3.Checked)
            //{
            //    linkPackingImage3.Text = null;
            //    picPackingImage3.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.PackingImage3;
            //    linkPackingImage3.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picPackingImage3.Image = DrawingUtils.GetImage(filePath, picPackingImage3.Width, picPackingImage3.Height);
            //}
        }

        private void chkAssemblyImage1_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAssemblyImage1.Checked)
            //{
            //    linkAssemblyImage1.Text = null;
            //    picAssemblyImage1.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.AssemblyImage1;
            //    linkAssemblyImage1.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picAssemblyImage1.Image = DrawingUtils.GetImage(filePath, picAssemblyImage1.Width, picAssemblyImage1.Height);
            //}
        }

        private void chkAssemblyImage2_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAssemblyImage2.Checked)
            //{
            //    linkAssemblyImage2.Text = null;
            //    picAssemblyImage2.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.AssemblyImage2;
            //    linkAssemblyImage2.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picAssemblyImage2.Image = DrawingUtils.GetImage(filePath, picAssemblyImage2.Width, picAssemblyImage2.Height);
            //}
        }

        private void chkAssemblyImage3_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAssemblyImage3.Checked)
            //{
            //    linkAssemblyImage3.Text = null;
            //    picAssemblyImage3.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.AssemblyImage3;
            //    linkAssemblyImage3.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picAssemblyImage3.Image = DrawingUtils.GetImage(filePath, picAssemblyImage3.Width, picAssemblyImage3.Height);
            //}
        }

        private void chkAssemblyImage4_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAssemblyImage4.Checked)
            //{
            //    linkAssemblyImage4.Text = null;
            //    picAssemblyImage4.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.AssemblyImage4;
            //    linkAssemblyImage4.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picAssemblyImage4.Image = DrawingUtils.GetImage(filePath, picAssemblyImage4.Width, picAssemblyImage4.Height);
            //}
        }

        private void chkAssemblyImage5_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAssemblyImage5.Checked)
            //{
            //    linkAssemblyImage5.Text = null;
            //    picAssemblyImage5.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.AssemblyImage5;
            //    linkAssemblyImage5.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picAssemblyImage5.Image = DrawingUtils.GetImage(filePath, picAssemblyImage5.Width, picAssemblyImage5.Height);
            //}
        }

        private void chkAssemblyImage6_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAssemblyImage6.Checked)
            //{
            //    linkAssemblyImage6.Text = null;
            //    picAssemblyImage6.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.AssemblyImage6;
            //    linkAssemblyImage6.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picAssemblyImage6.Image = DrawingUtils.GetImage(filePath, picAssemblyImage6.Width, picAssemblyImage6.Height);
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
