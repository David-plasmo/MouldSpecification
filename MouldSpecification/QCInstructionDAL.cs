using DataService;
using DataService;
using System;
using System.Collections.Generic;
using System.Data;
using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    internal class QCInstructionDAL : DataAccessBase
    {
        public void UpdateFromPivotTable(DataSet ds)
        {
            try
            {
                //Process added rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables["QCInstruction"].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    //add new row
                    if (dr["QCInstructionID1"] != DBNull.Value && (int)dr["QCInstructionID1"] <= 0)
                    {
                        if (dr["ItemID1"] != DBNull.Value && (int)dr["ItemID1"] > 0)
                        {
                            QCInstructionDC dc = new QCInstructionDC();
                            dc.ItemID = (int)dr["ItemID1"];
                            dc.InstructionNo = (int)dr["InstructionNo1"];
                            dc.QCInstruction = dr["QCInstruction1"].ToString();
                            dc.QCImageFilepath = dr["QCImageFilePath1"].ToString();
                            QCInstruction_ups(dc);
                        }
                    }
                    if (dr["QCInstructionID2"] != DBNull.Value && (int)dr["QCInstructionID2"] <= 0)
                    {
                        if (dr["ItemID2"] != DBNull.Value && (int)dr["ItemID2"] > 0)
                        {
                            QCInstructionDC dc = new QCInstructionDC();
                            dc.ItemID = (int)dr["ItemID2"];
                            dc.InstructionNo = (int)dr["InstructionNo2"];
                            dc.QCInstruction = dr["QCInstruction2"].ToString();
                            dc.QCImageFilepath = dr["QCImageFilePath2"].ToString();
                            QCInstruction_ups(dc);
                        }
                    }
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables["QCInstruction"].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    //modify existing row
                    if (dr["QCInstructionID1"] != DBNull.Value && (int)dr["QCInstructionID1"] > 0)
                    {
                        if (dr["ItemID1"] != DBNull.Value && (int)dr["ItemID1"] > 0)
                        {
                            QCInstructionDC dc = new QCInstructionDC();
                            dc.QCInstructionID = (int)dr["QCInstructionID1"];
                            dc.ItemID = (int)dr["ItemID1"];
                            dc.InstructionNo = (int)dr["InstructionNo1"];
                            dc.QCInstruction = dr["QCInstruction1"].ToString();
                            dc.QCImageFilepath = dr["QCImageFilePath1"].ToString();
                            QCInstruction_ups(dc);
                        }
                    }
                    if (dr["QCInstructionID2"] != DBNull.Value && (int)dr["QCInstructionID2"] > 0)
                    {
                        if (dr["ItemID2"] != DBNull.Value && (int)dr["ItemID2"] > 0)
                        {
                            QCInstructionDC dc = new QCInstructionDC();
                            dc.QCInstructionID = (int)dr["QCInstructionID2"];
                            dc.ItemID = (int)dr["ItemID2"];
                            dc.InstructionNo = (int)dr["InstructionNo2"];
                            dc.QCInstruction = dr["QCInstruction2"].ToString();
                            dc.QCImageFilepath = dr["QCImageFilePath2"].ToString();
                            QCInstruction_ups(dc);
                        }
                    }                    
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables["QCInstruction"].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["QCInstructionID1", DataRowVersion.Original] != null)
                    {
                        QCInstructionDC dc = new QCInstructionDC();
                        dc.QCInstructionID = Convert.ToInt32(dr["QCInstructionID1", DataRowVersion.Original].ToString());
                        QCInstruction_del(dc);
                    }
                    if (dr["QCInstructionID2", DataRowVersion.Original] != null)
                    {
                        QCInstructionDC dc = new QCInstructionDC();
                        dc.QCInstructionID = Convert.ToInt32(dr["QCInstructionID2", DataRowVersion.Original].ToString());
                        QCInstruction_del(dc);
                    }
                }

                ds.AcceptChanges();
            }
            catch (Exception ex) { }

        }

        public void QCInstruction_ups(QCInstructionDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "QCInstruction_ups",
                   CreateParameter("@QCInstructionID", SqlDbType.Int, dc.QCInstructionID, ParameterDirection.InputOutput),
                   CreateParameter("@ItemID", SqlDbType.Int, dc.ItemID),
                   CreateParameter("@InstructionNo", SqlDbType.Int, dc.InstructionNo),
                   CreateParameter("@QCInstruction", SqlDbType.VarChar, dc.QCInstruction),
                   CreateParameter("@QCImageFilepath", SqlDbType.VarChar, dc.QCImageFilepath));


                dc.QCInstructionID = (int)cmd.Parameters["@QCInstructionID"].Value;
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public void QCInstruction_del(QCInstructionDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                ExecuteNonQuery(ref cmd, "QCInstruction_del",
                   CreateParameter("@QCInstructionID", SqlDbType.Int, dc.QCInstructionID));


            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
    }
}
