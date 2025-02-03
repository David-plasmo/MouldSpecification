using DataService;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MouldSpecification
{
    internal class IMSpecificationDAL : DataAccessBase
    {
        public DataSet SelectIMSpecification(int? itemID, int? custID)
        {
            try
            {
                return ExecuteDataSet("SelectInjectionMouldSpecification",
                    CreateParameter("@ItemID", SqlDbType.Int, itemID),
                    CreateParameter("@CustomerID", SqlDbType.Int, custID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void UpdateIMSpecification(DataSet ds, string optionalTableName = "default")
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    IMSpecificationDC dc = DAL.CreateItemFromRow<IMSpecificationDC>(dr);  //populate  dataclass                   
                    AddInjectionMouldSpecification(dc);

                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    IMSpecificationDC dc = DAL.CreateItemFromRow<IMSpecificationDC>(dr);  //populate  dataclass                   
                    UpdateInjectionMouldSpecification(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = (optionalTableName == "default")
                    ? ds.Tables[0].Select("", "", dvrs)
                    : ds.Tables[optionalTableName].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    if (dr["MouldID", DataRowVersion.Original] != null)
                    {
                        IMSpecificationDC dc = new IMSpecificationDC();
                        dc.MouldID = Convert.ToInt32(dr["MouldID", DataRowVersion.Original].ToString());
                        DeleteInjectionMouldSpecification(dc);
                    }
                }

                //ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        public static void AddInjectionMouldSpecification(IMSpecificationDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddInjectionMouldSpecification", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MouldID", SqlDbType.Int, 4);
                cmd.Parameters["@MouldID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@MouldID"].Value = dc.MouldID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@MouldNumber", SqlDbType.VarChar, 100);
                cmd.Parameters["@MouldNumber"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MouldNumber"].Value = dc.MouldNumber;
                cmd.Parameters.Add("@MouldLocation", SqlDbType.VarChar, 100);
                cmd.Parameters["@MouldLocation"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MouldLocation"].Value = dc.MouldLocation;
                cmd.Parameters.Add("@MouldOwner", SqlDbType.VarChar, 50);
                cmd.Parameters["@MouldOwner"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MouldOwner"].Value = dc.MouldOwner;
                cmd.Parameters.Add("@FamilyMould", SqlDbType.Bit, 1);
                cmd.Parameters["@FamilyMould"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FamilyMould"].Value = dc.FamilyMould;
                cmd.Parameters.Add("@NoOfCavities", SqlDbType.Int, 4);
                cmd.Parameters["@NoOfCavities"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NoOfCavities"].Value = dc.NoOfCavities;
                cmd.Parameters.Add("@NoOfPart", SqlDbType.Int, 4);
                cmd.Parameters["@NoOfPart"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NoOfPart"].Value = dc.NoOfPart;
                cmd.Parameters.Add("@PartSummary", SqlDbType.VarChar, 255);
                cmd.Parameters["@PartSummary"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PartSummary"].Value = dc.PartSummary;
                cmd.Parameters.Add("@Operation", SqlDbType.VarChar, 50);
                cmd.Parameters["@Operation"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Operation"].Value = dc.Operation;
                cmd.Parameters.Add("@OtherFeatures", SqlDbType.VarChar, 100);
                cmd.Parameters["@OtherFeatures"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@OtherFeatures"].Value = dc.OtherFeatures;
                cmd.Parameters.Add("@FixedHalf", SqlDbType.VarChar, 50);
                cmd.Parameters["@FixedHalf"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FixedHalf"].Value = dc.FixedHalf;
                cmd.Parameters.Add("@FixedHalfTemp", SqlDbType.VarChar, 50);
                cmd.Parameters["@FixedHalfTemp"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FixedHalfTemp"].Value = dc.FixedHalfTemp;
                cmd.Parameters.Add("@MovingHalf", SqlDbType.VarChar, 50);
                cmd.Parameters["@MovingHalf"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MovingHalf"].Value = dc.MovingHalf;
                cmd.Parameters.Add("@MovingHalfTemp", SqlDbType.VarChar, 50);
                cmd.Parameters["@MovingHalfTemp"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MovingHalfTemp"].Value = dc.MovingHalfTemp;
                cmd.Parameters.Add("@PremouldReq", SqlDbType.VarChar, 255);
                cmd.Parameters["@PremouldReq"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PremouldReq"].Value = dc.PremouldReq;
                cmd.Parameters.Add("@PostMouldReq", SqlDbType.VarChar, 255);
                cmd.Parameters["@PostMouldReq"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PostMouldReq"].Value = dc.PostMouldReq;
                cmd.Parameters.Add("@AdditionalLabourReqd", SqlDbType.Bit, 1);
                cmd.Parameters["@AdditionalLabourReqd"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditionalLabourReqd"].Value = dc.AdditionalLabourReqd;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.MouldID = (int)cmd.Parameters["@MouldID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateInjectionMouldSpecification(IMSpecificationDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateInjectionMouldSpecification", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MouldID", SqlDbType.Int, 4);
                cmd.Parameters["@MouldID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MouldID"].Value = dc.MouldID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                cmd.Parameters["@ItemID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemID"].Value = dc.ItemID;
                cmd.Parameters.Add("@MouldNumber", SqlDbType.VarChar, 100);
                cmd.Parameters["@MouldNumber"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MouldNumber"].Value = dc.MouldNumber;
                cmd.Parameters.Add("@MouldLocation", SqlDbType.VarChar, 100);
                cmd.Parameters["@MouldLocation"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MouldLocation"].Value = dc.MouldLocation;
                cmd.Parameters.Add("@MouldOwner", SqlDbType.VarChar, 50);
                cmd.Parameters["@MouldOwner"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MouldOwner"].Value = dc.MouldOwner;
                cmd.Parameters.Add("@FamilyMould", SqlDbType.Bit, 1);
                cmd.Parameters["@FamilyMould"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FamilyMould"].Value = dc.FamilyMould;
                cmd.Parameters.Add("@NoOfCavities", SqlDbType.Int, 4);
                cmd.Parameters["@NoOfCavities"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NoOfCavities"].Value = dc.NoOfCavities;
                cmd.Parameters.Add("@NoOfPart", SqlDbType.Int, 4);
                cmd.Parameters["@NoOfPart"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NoOfPart"].Value = dc.NoOfPart;
                cmd.Parameters.Add("@PartSummary", SqlDbType.VarChar, 255);
                cmd.Parameters["@PartSummary"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PartSummary"].Value = dc.PartSummary;
                cmd.Parameters.Add("@Operation", SqlDbType.VarChar, 50);
                cmd.Parameters["@Operation"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Operation"].Value = dc.Operation;
                cmd.Parameters.Add("@OtherFeatures", SqlDbType.VarChar, 100);
                cmd.Parameters["@OtherFeatures"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@OtherFeatures"].Value = dc.OtherFeatures;
                cmd.Parameters.Add("@FixedHalf", SqlDbType.VarChar, 50);
                cmd.Parameters["@FixedHalf"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FixedHalf"].Value = dc.FixedHalf;
                cmd.Parameters.Add("@FixedHalfTemp", SqlDbType.VarChar, 50);
                cmd.Parameters["@FixedHalfTemp"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FixedHalfTemp"].Value = dc.FixedHalfTemp;
                cmd.Parameters.Add("@MovingHalf", SqlDbType.VarChar, 50);
                cmd.Parameters["@MovingHalf"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MovingHalf"].Value = dc.MovingHalf;
                cmd.Parameters.Add("@MovingHalfTemp", SqlDbType.VarChar, 50);
                cmd.Parameters["@MovingHalfTemp"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MovingHalfTemp"].Value = dc.MovingHalfTemp;
                cmd.Parameters.Add("@PremouldReq", SqlDbType.VarChar, 255);
                cmd.Parameters["@PremouldReq"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PremouldReq"].Value = dc.PremouldReq;
                cmd.Parameters.Add("@PostMouldReq", SqlDbType.VarChar, 255);
                cmd.Parameters["@PostMouldReq"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PostMouldReq"].Value = dc.PostMouldReq;
                cmd.Parameters.Add("@AdditionalLabourReqd", SqlDbType.Bit, 1);
                cmd.Parameters["@AdditionalLabourReqd"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AdditionalLabourReqd"].Value = dc.AdditionalLabourReqd;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void DeleteInjectionMouldSpecification(IMSpecificationDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteInjectionMouldSpecification", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@MouldID", SqlDbType.Int, 4);
                cmd.Parameters["@MouldID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MouldID"].Value = dc.MouldID;

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
