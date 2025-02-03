﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace IMSpecification
{
    public partial class EditPastelMaster : Form
    {
        DataSet PastelMaster;
        PastelDataClass pdc;      
        bool bIsLoading ;

        //public LabelDictionary Labels;

        public EditPastelMaster()
        {
            InitializeComponent();
        }

        private void LoadPastelMaster()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                dgvEdit.Visible = false;
                bIsLoading = true;
                PastelMaster = new DataService.ProductDataService().GetPastelMaster();
                dgvEdit.DataSource = null;
                dgvEdit.Columns.Clear();
                dgvEdit.DataSource = PastelMaster.Tables[0];
                dgvEdit.EditMode = DataGridViewEditMode.EditOnEnter;
                dgvEdit.Columns["Description"].ReadOnly = true;
                dgvEdit.Columns["PastelID"].Visible = true;
                dgvEdit.Columns["last_updated_on"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";//"yyyy-MM-dd HH:mm:ss.fffffff";
                dgvEdit.Columns["last_updated_on"].Visible = true;
                dgvEdit.Columns["last_updated_by"].Visible = true;
                dgvEdit.Columns["CategoryID"].Visible = false;
                dgvEdit.Columns["CompanyOrder"].Visible = false;
                //dgvEdit.Columns["MaterialID"].Visible = false;
                dgvEdit.Columns["last_updated_by"].ReadOnly = true;
                dgvEdit.Columns["last_updated_on"].ReadOnly = true;

                dgvEdit.AutoResizeColumns();
                for (int i = 0; i < dgvEdit.ColumnCount; i++) { dgvEdit.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic; }
                bIsLoading = false;
                //dgvEdit.ResumeLayout(true);
                dgvEdit.Visible = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

        private void EditPastelMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (dgvEdit.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvEdit.EndEdit();
                DataService.ProductDataService ds = new DataService.ProductDataService();
                ds.UpdatePastelMaster(PastelMaster);
                this.Close();
            }
            catch
            {
                throw;
            }
        }
    

        private void EditPastelMaster_Load(object sender, EventArgs e)
        {
            LoadPastelMaster();
        }

        private void dgvEdit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DoEdit();
        }

        public void DoEdit()
        {
            try
            {
                EditPastelMasterRow subForm = new EditPastelMasterRow();
                PastelDataClass pdc = new PastelDataClass();
                if (dgvEdit.CurrentRow != null && dgvEdit.CurrentRow.DataBoundItem != null)
                {                    
                    int numValue;
                    if (Int32.TryParse((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["PastelID"].ToString(), out numValue))
                        pdc.PastelID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["PastelID"].ToString());
                    else pdc.PastelID = (int?)null;

                    if (Int32.TryParse((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["CategoryID"].ToString(), out numValue))
                        pdc.CategoryID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["CategoryID"].ToString());
                    else pdc.CategoryID = (int?)null;

                    if (Int32.TryParse((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["CtnQty"].ToString(), out numValue))
                        pdc.CtnQty = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["CtnQty"].ToString());
                    else pdc.CtnQty = (int?)null;

                    if (Int32.TryParse((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["MaterialID"].ToString(), out numValue))
                        pdc.MaterialID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["MaterialID"].ToString());
                    else pdc.MaterialID = (int?)null;

                    DateTime dtValue;
                    if (DateTime.TryParse((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["last_updated_on"].ToString(), out dtValue))
                        pdc.last_updated_on = Convert.ToDateTime((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["last_updated_on"]);
                    else pdc.last_updated_on = (DateTime?)null;


                    pdc.Code = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Code"].ToString();
                    pdc.Description = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Description"].ToString();
                    pdc.CtnSize = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["CtnSize"].ToString();
                    pdc.Grade = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Grade"].ToString();
                    pdc.last_updated_by = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Last_Updated_By"].ToString();
                }
                subForm.PDC = pdc;
                subForm.ShowDialog();
                if (subForm.DialogResult == DialogResult.OK)
                {
                    pdc = subForm.PDC;
                    UpdateGridRow(pdc);
                    if (dgvEdit.CurrentRow != null)
                    {
                        dgvEdit.NotifyCurrentCellDirty(true);
                        dgvEdit.EndEdit();
                        dgvEdit.NotifyCurrentCellDirty(false);
                    }
                    
                }                            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateGridRow(PastelDataClass pdc)
        {
            try
            {
                if (dgvEdit.CurrentRow == null)
                {
                    //if here, sub form was opened independent of this form, to edit a new record
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable(); //ds.Tables[0];
                    dt.Columns.Add("CategoryID", typeof(int));
                    dt.Columns.Add("Code", typeof(string));
                    dt.Columns.Add("CtnQty", typeof(int));
                    dt.Columns.Add("CtnSize", typeof(string));
                    dt.Columns.Add("Description", typeof(string));
                    dt.Columns.Add("Grade", typeof(string));
                    dt.Columns.Add("MaterialID", typeof(int));
                    dt.Columns.Add("last_updated_by", typeof(string));
                    dt.Columns.Add("last_updated_on", typeof(DateTime));
                    dt.Columns.Add("PastelID", typeof(int));
                    DataRow dr = dt.NewRow();
                    dr["CategoryID"]  =  pdc.CategoryID;
                    dr["Code"]  =  pdc.Code;
                    dr["CtnQty"]  =  pdc.CtnQty;
                    dr["CtnSize"]  =  pdc.CtnSize;
                    dr["Description"]  =  pdc.Description;
                    dr["Grade"]  =  pdc.Grade;
                    dr["MaterialID"] = pdc.MaterialID;
                    dr["last_updated_by"]  =  pdc.last_updated_by;
                    dr["last_updated_on"]  =  DBNull.Value;
                    dr["PastelID"]  =  DBNull.Value;
                    dt.Rows.Add(dr);
                    ds.Tables.Add(dt);

                    DataService.ProductDataService pds = new DataService.ProductDataService();
                    pds.UpdatePastelMaster(ds);
                }
                else
                {
                    //if here, sub form has been opened from double click on this form
                    if (pdc.PastelID.HasValue) dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["PastelID"].Value = pdc.PastelID.Value;
                    else dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["PastelID"].Value = DBNull.Value;

                    if (pdc.last_updated_on.HasValue) dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["last_updated_on"].Value = pdc.last_updated_on.Value;
                    else dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["last_updated_on"].Value = DBNull.Value;

                    if (pdc.CategoryID.HasValue) dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["CategoryID"].Value = pdc.CategoryID.Value;
                    else dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["CategoryID"].Value = DBNull.Value;

                    if (pdc.CtnQty.HasValue) dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["CtnQty"].Value = pdc.CtnQty.Value;
                    else dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["CtnQty"].Value = DBNull.Value;

                    if (pdc.MaterialID.HasValue) dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialID"].Value = pdc.MaterialID.Value;
                    else dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialID"].Value = DBNull.Value;

                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Code"].Value = pdc.Code;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["CtnSize"].Value = pdc.CtnSize;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Grade"].Value = pdc.Grade;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Description"].Value = pdc.Description;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["last_updated_by"].Value = pdc.last_updated_by;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = "Double-Click to Edit";
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEdit.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvEdit.EndEdit();
                DataService.ProductDataService ds = new DataService.ProductDataService();
                ds.UpdatePastelMaster(PastelMaster);
                this.Close();
                }
                catch
                {
                    throw;
                }            
        }
    }
}






