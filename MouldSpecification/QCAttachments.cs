using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Utils;

namespace MouldSpecification
{
    public partial class QCAttachments : Form
    {
        public QualityControlDC dc = null;
        public QCAttachments(QualityControlDC qcDC)
        {
            InitializeComponent();
            dc = qcDC;
        }

        public QCAttachments()
        {
            
        }

        private void QCAttachments_Load(object sender, EventArgs e)
        {
            this.btnPromptQC1.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnPromptQC1.Height - 8, btnPromptQC1.Height - 8);
            this.btnPromptQC1.ImageAlign = ContentAlignment.MiddleRight;
            this.btnPromptQCImage2.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnPromptQCImage2.Height - 8, btnPromptQCImage2.Height - 8);
            this.btnPromptQCImage2.ImageAlign = ContentAlignment.MiddleRight;
            this.btnPromptQCImage3.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnPromptQCImage3.Height - 8, btnPromptQCImage3.Height - 8);
            this.btnPromptQCImage3.ImageAlign = ContentAlignment.MiddleRight;
            this.btnPromptQCImage4.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnPromptQCImage4.Height - 8, btnPromptQCImage4.Height - 8);
            this.btnPromptQCImage4.ImageAlign = ContentAlignment.MiddleRight;
            this.btnLabelIcon.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnLabelIcon.Height - 8, btnLabelIcon.Height - 8);
            this.btnLabelIcon.ImageAlign = ContentAlignment.MiddleRight;
            this.btnSpecialInstructionDoc.Image = DrawingUtils.GetImage(DrawingUtils.ButtonOp.Browse, btnSpecialInstructionDoc.Height - 8, btnSpecialInstructionDoc.Height - 8);
            this.btnSpecialInstructionDoc.ImageAlign = ContentAlignment.MiddleRight;

            linkQCImage1.TextAlign = ContentAlignment.MiddleLeft;
            linkQCImage1.FlatStyle = FlatStyle.Flat;
            linkQCImage2.TextAlign = ContentAlignment.MiddleLeft;
            linkQCImage2.FlatStyle = FlatStyle.Flat;
            linkQCImage3.TextAlign = ContentAlignment.MiddleLeft;
            linkQCImage3.FlatStyle = FlatStyle.Flat;
            linkQCImage4.TextAlign = ContentAlignment.MiddleLeft;
            linkQCImage4.FlatStyle = FlatStyle.Flat;
            linkLabelIcon.TextAlign = ContentAlignment.MiddleLeft;
            linkLabelIcon.FlatStyle = FlatStyle.Flat;
            linkSpecialInstructionDoc.TextAlign = ContentAlignment.MiddleLeft;
            linkSpecialInstructionDoc.FlatStyle = FlatStyle.Flat;

            BindControls();
        }

        private void BindControls()
        {
            //string filePath = dc.QCImage1;
            //linkQCImage1.Text = filePath;
            //if (File.Exists(filePath))
            //    picQCImage1.Image = DrawingUtils.GetImage(filePath, picQCImage1.Width, picQCImage1.Height);

            //filePath = dc.QCImage2;
            //linkQCImage2.Text = filePath;
            //if (File.Exists(filePath))
            //    picQCImage2.Image = DrawingUtils.GetImage(filePath, picQCImage2.Width, picQCImage2.Height);

            //filePath = dc.QCImage3;
            //linkQCImage3.Text = filePath;
            //if (File.Exists(filePath))
            //    picQCImage3.Image = DrawingUtils.GetImage(filePath, picQCImage3.Width, picQCImage3.Height);

            //filePath = dc.QCImage4;
            //linkQCImage4.Text = filePath;
            //if (File.Exists(filePath))
            //    picQCImage4.Image = DrawingUtils.GetImage(filePath, picQCImage4.Width, picQCImage4.Height);

            //filePath = dc.LabelIcon;
            //linkLabelIcon.Text = filePath;
            //if (File.Exists(filePath))
            //    picLabelIcon.Image = DrawingUtils.GetImage(filePath, picLabelIcon.Width, picLabelIcon.Height);

            //filePath = dc.SpecialInstructionDoc;
            //linkSpecialInstructionDoc.Text = filePath;
        }

        private void btnPromptQC1_Click(object sender, EventArgs e)
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
            ofd.Filter = "jpg files (*.jpg)|*.jpg|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkQCImage1.Text = filePath;
                picQCImage1.Image = DrawingUtils.GetImage(filePath, picQCImage1.Width, picQCImage1.Height);

            }
        }

        private void linkQCImage1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkQCImage1.LinkVisited = true;
            System.Diagnostics.Process.Start(linkQCImage1.Text);
        }

        private void btnPromptQCImage2_Click(object sender, EventArgs e)
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
            ofd.Filter = "jpg files (*.jpg)|*.jpg|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkQCImage2.Text = filePath;
                picQCImage2.Image = DrawingUtils.GetImage(filePath, picQCImage2.Width, picQCImage2.Height);
            }
        }

        private void linkQCImage2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkQCImage2.LinkVisited = true;
            System.Diagnostics.Process.Start(linkQCImage2.Text);
        }

        private void btnPromptQCImage3_Click(object sender, EventArgs e)
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
            ofd.Filter = "jpg files (*.jpg)|*.jpg|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkQCImage3.Text = filePath;
                picQCImage3.Image = DrawingUtils.GetImage(filePath, picQCImage3.Width, picQCImage3.Height);

            }
        }

        private void linkQCImage3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkQCImage3.LinkVisited = true;
            System.Diagnostics.Process.Start(linkQCImage3.Text);
        }

        private void btnPromptQCImage4_Click(object sender, EventArgs e)
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
            ofd.Filter = "jpg files (*.jpg)|*.jpg|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkQCImage4.Text = filePath;
                picQCImage4.Image = DrawingUtils.GetImage(filePath, picQCImage4.Width, picQCImage4.Height);

            }
        }

        private void linkQCImage4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkQCImage4.LinkVisited = true;
            System.Diagnostics.Process.Start(linkQCImage4.Text);
        }

        private void btnLabelIcon_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            var appSettings = ConfigurationManager.AppSettings;
            string key = "QCImageFolderDir";
            string result = appSettings[key] ?? "Not Found";
            if (appSettings.Count == 0)
                ofd.InitialDirectory = "C:\\";
            else
            {
                ofd.InitialDirectory = result;  // doesn't work -- always goes to last selected directory 
                if (!ofd.CheckPathExists)
                    ofd.InitialDirectory = "C:\\";
            }
            ofd.Filter = "jpg files (*.jpg)|*.jpg|tif files (*.tif)|*.tif|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkLabelIcon.Text = filePath;
                picLabelIcon.Image = DrawingUtils.GetImage(filePath, picQCImage4.Width, picQCImage4.Height);

            }
        }

        private void linkLabelIcon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabelIcon.LinkVisited = true;
            System.Diagnostics.Process.Start(linkLabelIcon.Text);
        }

        private void btnSpecialInstructionDoc_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            var appSettings = ConfigurationManager.AppSettings;
            string key = "QCSpecialInstructionDir";
            string result = appSettings[key] ?? "Not Found";
            if (appSettings.Count == 0)
                ofd.InitialDirectory = "C:\\";
            else
            {
                ofd.InitialDirectory = result;
                if (!ofd.CheckPathExists)
                    ofd.InitialDirectory = "C:\\";
            }
            ofd.Filter = "jpg files (*.jpg)|*.jpg|doc files (*.doc)|*.doc|pdf files (*.pdf)|*.pdf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                linkSpecialInstructionDoc.Text = filePath;
                //picQCImage4.Image = DrawingUtils.GetImage(filePath, picQCImage4.Width, picQCImage4.Height);

            }
        }

        private void linkSpecialInstructionDoc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkSpecialInstructionDoc.LinkVisited = true;
            System.Diagnostics.Process.Start(linkSpecialInstructionDoc.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            ////pass edits back to parent form
            //dc.QCImage1 = linkQCImage1.Text;
            //dc.QCImage2 = linkQCImage2.Text;
            //dc.QCImage3 = linkQCImage3.Text;
            //dc.QCImage4 = linkQCImage4.Text;
            //dc.LabelIcon = linkLabelIcon.Text;
            //dc.SpecialInstructionDoc = linkSpecialInstructionDoc.Text;
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void chkQCImage1_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkQCImage1.Checked)
            //{
            //    linkQCImage1.Text = null;
            //    picQCImage1.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.QCImage1;
            //    linkQCImage1.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picQCImage1.Image = DrawingUtils.GetImage(filePath, picQCImage1.Width, picQCImage1.Height);
            //}
        }

        private void chkQCImage2_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkQCImage2.Checked)
            //{
            //    linkQCImage2.Text = null;
            //    picQCImage2.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.QCImage2;
            //    linkQCImage2.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picQCImage2.Image = DrawingUtils.GetImage(filePath, picQCImage2.Width, picQCImage2.Height);
            //}
        }

        private void chkQCImage3_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkQCImage3.Checked)
            //{
            //    linkQCImage3.Text = null;
            //    picQCImage3.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.QCImage3;
            //    linkQCImage3.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picQCImage3.Image = DrawingUtils.GetImage(filePath, picQCImage3.Width, picQCImage3.Height);
            //}
        }

        private void chkQCImage4_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkQCImage4.Checked)
            //{
            //    linkQCImage4.Text = null;
            //    picQCImage4.Image = null;
            //}
            //else
            //{
            //    string filePath = dc.QCImage4;
            //    linkQCImage4.Text = filePath;
            //    if (filePath != null && filePath.Length != 0)
            //        picQCImage4.Image = DrawingUtils.GetImage(filePath, picQCImage4.Width, picQCImage4.Height);
            //}
        }

        private void chkLabelIcon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLabelIcon.Checked)
            {
                linkLabelIcon.Text = null;
                picLabelIcon.Image = null;
            }
            else
            {
                string filePath = dc.LabelIcon;
                linkLabelIcon.Text = filePath;
                if (filePath != null && filePath.Length != 0)
                    picLabelIcon.Image = DrawingUtils.GetImage(filePath, picLabelIcon.Width, picLabelIcon.Height);
            }
        }

        private void chkSpecialInstruction_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkSpecialInstruction.Checked)
            //{
            //    linkSpecialInstructionDoc.Text = null;
            //}
            //else
            //{
            //    string filePath = dc.SpecialInstructionDoc;
            //    linkSpecialInstructionDoc.Text = filePath;

            //}
        }
    }
}
