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
                    if (dr["QCInstructionID1"] == DBNull.Value)
                    {
                        if (dr["ItemID1"] != DBNull.Value)
                        {
                            QCInstructionDC dc = new QCInstructionDC();
                            dc.ItemID = (int)dr["ItemID1"];
                            dc.InstructionNo = (int)dr["InstructionNo1"];
                            dc.QCInstruction = dr["QCInstruction1"].ToString();
                            dc.QCImageFilepath = dr["QCImageFilePath1"].ToString();
                            AddQCInstruction(dc);
                        }
                    }
                    if (dr["QCInstructionID2"] == DBNull.Value)
                    {
                        if (dr["ItemID2"] != DBNull.Value)
                        {
                            QCInstructionDC dc = new QCInstructionDC();
                            dc.ItemID = (int)dr["ItemID2"];
                            dc.InstructionNo = (int)dr["InstructionNo2"];
                            dc.QCInstruction = dr["QCInstruction2"].ToString();
                            dc.QCImageFilepath = dr["QCImageFilePath2"].ToString();
                            AddQCInstruction(dc);
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
                    if (dr["QCInstructionID1"] != DBNull.Value)
                    {
                        if (dr["ItemID1"] != DBNull.Value)
                        {
                            QCInstructionDC dc = new QCInstructionDC();
                            dc.QCInstructionID = (int)dr["QCInstructionID1"];
                            dc.ItemID = (int)dr["ItemID1"];
                            dc.InstructionNo = (int)dr["InstructionNo1"];
                            dc.QCInstruction = dr["QCInstruction1"].ToString();
                            dc.QCImageFilepath = dr["QCImageFilePath1"].ToString();
                            UpdateQCInstruction(dc);
                        }
                    }
                    if (dr["QCInstructionID2"] != DBNull.Value)
                    {
                        if (dr["ItemID2"] != DBNull.Value)
                        {
                            QCInstructionDC dc = new QCInstructionDC();
                            dc.QCInstructionID = (int)dr["QCInstructionID2"];
                            dc.ItemID = (int)dr["ItemID2"];
                            dc.InstructionNo = (int)dr["InstructionNo2"];
                            dc.QCInstruction = dr["QCInstruction2"].ToString();
                            dc.QCImageFilepath = dr["QCImageFilePath2"].ToString();
                            UpdateQCInstruction(dc);
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
                        DeleteQCInstruction(dc);
                    }
                    if (dr["QCInstructionID2", DataRowVersion.Original] != null)
                    {
                        QCInstructionDC dc = new QCInstructionDC();
                        dc.QCInstructionID = Convert.ToInt32(dr["QCInstructionID2", DataRowVersion.Original].ToString());
                        DeleteQCInstruction(dc);
                    }
                }

                ds.AcceptChanges();
            }
            catch (Exception ex) { }

        }

        public void UpdateQCInstruction(DataSet ds)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                //MachineDAL md = new MachineDAL();

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    QCInstructionDC dc = DAL.CreateItemFromRow<QCInstructionDC>(dr);  //populate  dataclass                   
                    AddQCInstruction(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    QCInstructionDC dc = DAL.CreateItemFromRow<QCInstructionDC>(dr);  //populate  dataclass                   
                    UpdateQCInstruction(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["QCInstructionID", DataRowVersion.Original] != null)
                    {
                        QCInstructionDC dc = new QCInstructionDC();
                        dc.QCInstructionID = Convert.ToInt32(dr["QCInstructionID", DataRowVersion.Original].ToString());
                        DeleteQCInstruction(dc);
                    }
                }
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        public static void AddQCInstruction(QCInstructionDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddQCInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@QCInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@QCInstructionID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@QCInstructionID"].Value = dc.QCInstructionID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@InstructionNo", SqlDbType.Int, 4);
                cmd.Parameters["@InstructionNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@InstructionNo"].Value = dc.InstructionNo;
                cmd.Parameters.Add("@QCInstruction", SqlDbType.VarChar, 300);
                cmd.Parameters["@QCInstruction"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QCInstruction"].Value = dc.QCInstruction;
                cmd.Parameters.Add("@QCImageFilepath", SqlDbType.VarChar, 300);
                cmd.Parameters["@QCImageFilepath"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QCImageFilepath"].Value = dc.QCImageFilepath;

                cmd.ExecuteNonQuery();

                dc.QCInstructionID = (int)cmd.Parameters["@QCInstructionID"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateQCInstruction(QCInstructionDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateQCInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@QCInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@QCInstructionID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QCInstructionID"].Value = dc.QCInstructionID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@InstructionNo", SqlDbType.Int, 4);
                cmd.Parameters["@InstructionNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@InstructionNo"].Value = dc.InstructionNo;
                cmd.Parameters.Add("@QCInstruction", SqlDbType.VarChar, 300);
                cmd.Parameters["@QCInstruction"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QCInstruction"].Value = dc.QCInstruction;
                cmd.Parameters.Add("@QCImageFilepath", SqlDbType.VarChar, 300);
                cmd.Parameters["@QCImageFilepath"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QCImageFilepath"].Value = dc.QCImageFilepath;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void DeleteQCInstruction(QCInstructionDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteQCInstruction", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@QCInstructionID", SqlDbType.Int, 4);
                cmd.Parameters["@QCInstructionID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QCInstructionID"].Value = dc.QCInstructionID;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

    }
}
