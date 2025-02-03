using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataService;

namespace MouldSpecification
{
    public partial class ProductGradeForm : Form
    {
        bool bIsLoading = true;
        string curImagePath;

        DataSet dsProductGrade;
        public ProductGradeForm()
        {
            InitializeComponent();
        }

        private void ProductGradeForm_Load(object sender, EventArgs e)
        {
            bIsLoading = true;

            LoadGrid();
            bIsLoading = false;
        }
        private void LoadGrid()
        {
            ProductDataService pds = new ProductDataService();
            dsProductGrade = pds.GetProductGradeRef();
            dgvEdit.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvEdit.AutoGenerateColumns = true;
            dgvEdit.DataSource = dsProductGrade.Tables[0];
            dgvEdit.Columns[0].Visible = false;
            dgvEdit.Columns[3].Width = 300;
        }

        private void ProductGradeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //bClosing = true;
                if (dgvEdit.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvEdit.EndEdit();
                dgvEdit.DataSource = null;
                DataService.ProductDataService ds = new DataService.ProductDataService();
                ds.UpdateProductGrade(dsProductGrade);
            }
            catch
            {
                throw;
            }
        }

        private void dgvEdit_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!e.Row.IsNewRow)
            {
                DialogResult response = MessageBox.Show("Are you sure?", "Delete row?",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);

                if (response == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("todo:  open file");  
            strip.SendToBack();
            string fileName = dgvEdit.CurrentCell.Value.ToString();
            ProcessStartInfo pi = new ProcessStartInfo(fileName);
            pi.Arguments = Path.GetFileName(fileName);
            pi.UseShellExecute = true;
            pi.WorkingDirectory = Path.GetDirectoryName(fileName);
            pi.FileName = fileName;
            pi.Verb = "OPEN";
            Process.Start(pi);
            strip.SendToBack();
        }

        private void locateFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("todo:  browse for file");
            opf.InitialDirectory = "\\\\plasmo-fp-01\\Data\\CONSOLIDATED PLASTICS\\INJECTION MOULDING\\Database\\Images";
            //\\plasmo-fp-01\Data\CONSOLIDATED PLASTICS\INJECTION MOULDING\Database\Images
            strip.SendToBack();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    curImagePath = opf.FileName;
                    strip.Items[2].Enabled = true;
                    ProcessStartInfo pi = new ProcessStartInfo(curImagePath);
                    pi.Arguments = Path.GetFileName(curImagePath);
                    pi.UseShellExecute = true;
                    pi.WorkingDirectory = Path.GetDirectoryName(curImagePath);
                    pi.FileName = curImagePath;
                    pi.Verb = "OPEN";
                    //Process.Start(pi).WaitForExit();
                    Process.Start(pi);
                    strip.SendToBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void saveLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("todo:  save/replace link");
            dgvEdit.CurrentCell.Value = curImagePath;
            strip.Items[0].Enabled = true;
            strip.Items[2].Enabled = true;
            strip.Items[3].Enabled = true;
            strip.Items[5].Enabled = true;
            strip.Items[6].Enabled = true;
            //dgvEdit.CurrentCell.ContextMenuStrip = strip;            
            //strip.Show();
        }

        private void removeLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("todo:  remove link");
            curImagePath = null;
            dgvEdit.CurrentCell.Value = curImagePath;
            strip.Items[0].Enabled = false;
            strip.Items[2].Enabled = false;
            strip.Items[3].Enabled = false;
            strip.Items[5].Enabled = false;
            strip.Items[6].Enabled = false;
        }

        private void copyLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curImagePath = dgvEdit.CurrentCell.Value.ToString();
            strip.Items[5].Enabled = (curImagePath != null);
            strip.Items[6].Enabled = (curImagePath != null);
            strip.Close();
        }

        private void pasteLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //curImagePath = dgvEdit.CurrentCell.Value.ToString();
            dgvEdit.CurrentCell.Value = curImagePath;
            strip.Items[0].Enabled = true;
            strip.Items[2].Enabled = true;
            strip.Items[3].Enabled = true;
            strip.Items[5].Enabled = true;
            strip.Items[6].Enabled = true;
            strip.Close();
        }

        private void closeMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            strip.Close();
        }

        private void dgvEdit_DoubleClick(object sender, EventArgs e)
        {
            if (dgvEdit.CurrentRow.Index == -1 || dgvEdit.CurrentCell.ColumnIndex == -1)
                return;
            if (dgvEdit.CurrentCell.ColumnIndex > 2)
            {
                //MessageBox.Show("todo:  open file");
                string fileName = dgvEdit.CurrentCell.Value.ToString();
                if (fileName.Length > 0)
                {
                    try
                    {
                        ProcessStartInfo pi = new ProcessStartInfo(fileName);
                        pi.Arguments = Path.GetFileName(fileName);
                        pi.UseShellExecute = true;
                        pi.WorkingDirectory = Path.GetDirectoryName(fileName);
                        pi.FileName = fileName;
                        pi.Verb = "OPEN";
                        Process.Start(pi);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
            }
        }

        private void dgvEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F10 && e.Shift) || e.KeyCode == Keys.Apps)
            {
                e.SuppressKeyPress = true;
                DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
                if (currentCell != null)
                {
                    ContextMenuStrip cms = currentCell.ContextMenuStrip;
                    if (cms != null)
                    {
                        Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
                        Point p = new Point(r.X + r.Width, r.Y + r.Height);
                        cms.Show(currentCell.DataGridView, p);
                    }
                }
            }
        }

        private void dgvEdit_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                if (!c.Selected)
                {
                    c.DataGridView.ClearSelection();
                    c.DataGridView.CurrentCell = c;
                    c.Selected = true;
                }
            }
        }

        private void dgvEdit_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            if (e.ColumnIndex == 3)
            {
                e.ContextMenuStrip = strip;
                strip.Items[0].Enabled = (dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length > 0);
                strip.Items[2].Enabled = (dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length > 0);
                strip.Items[3].Enabled = (dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length > 0);
                strip.Items[5].Enabled = (strip.Items[2].Enabled || curImagePath != null);
                strip.Items[6].Enabled = (strip.Items[2].Enabled || curImagePath != null);
                strip.AutoClose = false;
            }
        }
    }
}
