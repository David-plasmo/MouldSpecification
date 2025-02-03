using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataService;

namespace MouldSpecification
{
    public class BMSetupDAL
    {
        public static int UpdateBlowMouldMachineSetup(BMSetupData dc)
       
        {
            int RETURN_VALUE = 0;
            System.Data.SqlClient.SqlCommand cmd = null;
            //System.Data.SqlClient.SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
            if ((connection == null))
            {
                throw new System.ArgumentException("The connection object cannot be null");
            }
            else
            {
                if ((connection.State == System.Data.ConnectionState.Closed))
                {
                    connection.Open();
                    cmd = new System.Data.SqlClient.SqlCommand("UpdateBlowMouldMachineSetup", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RETURN_VALUE", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@RETURN_VALUE"].Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters["@RETURN_VALUE"].Value = RETURN_VALUE;
                    cmd.Parameters.Add("@SetupID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@SetupID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@SetupID"].Value = dc.SetupID;
                    cmd.Parameters.Add("@MachineID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MachineID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MachineID"].Value = dc.MachineID;
                    cmd.Parameters.Add("@PmID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@PmID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PmID"].Value = dc.PmID;
                    cmd.Parameters.Add("@MaterialID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialID"].Value = dc.MaterialID;
                    cmd.Parameters.Add("@GradeID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@GradeID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@GradeID"].Value = dc.GradeID;
                    cmd.Parameters.Add("@CUSTNMBR", System.Data.SqlDbType.Char, 15);
                    cmd.Parameters["@CUSTNMBR"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CUSTNMBR"].Value = dc.CUSTNMBR;
                    cmd.Parameters.Add("@MaterialIDA", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialIDA"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialIDA"].Value = dc.MaterialIDA;
                    cmd.Parameters.Add("@MaterialAPC", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialAPC"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialAPC"].Value = dc.MaterialAPC;
                    cmd.Parameters.Add("@MaterialIDB", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialIDB"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialIDB"].Value = dc.MaterialIDB;
                    cmd.Parameters.Add("@MaterialBPC", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialBPC"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialBPC"].Value = dc.MaterialBPC;
                    cmd.Parameters.Add("@MasterBatchIDA", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MasterBatchIDA"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MasterBatchIDA"].Value = dc.MasterBatchIDA;
                    cmd.Parameters.Add("@MasterBatchIDB", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MasterBatchIDB"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MasterBatchIDB"].Value = dc.MasterBatchIDB;
                    cmd.Parameters.Add("@AdditiveID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@AdditiveID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@AdditiveID"].Value = dc.AdditiveID;
                    cmd.Parameters.Add("@MaterialNote", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@MaterialNote"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialNote"].Value = dc.MaterialNote;
                    cmd.Parameters.Add("@WeightLower", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@WeightLower"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@WeightLower"].Value = dc.WeightLower;
                    cmd.Parameters.Add("@WeightHigher", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@WeightHigher"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@WeightHigher"].Value = dc.WeightHigher;
                    cmd.Parameters.Add("@TotalShot", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@TotalShot"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@TotalShot"].Value = dc.TotalShot;
                    cmd.Parameters.Add("@PackingStyle", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@PackingStyle"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PackingStyle"].Value = dc.PackingStyle;
                    cmd.Parameters.Add("@Lined", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@Lined"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Lined"].Value = dc.Lined;
                    cmd.Parameters.Add("@Wrapping", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Wrapping"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Wrapping"].Value = dc.Wrapping;
                    cmd.Parameters.Add("@PalletType", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@PalletType"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PalletType"].Value = dc.PalletType;
                    cmd.Parameters.Add("@PackingNotes", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@PackingNotes"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PackingNotes"].Value = dc.PackingNotes;
                    //cmd.Parameters.Add("@MasterBatchA", System.Data.SqlDbType.VarChar, 50);
                    //cmd.Parameters["@MasterBatchA"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@MasterBatchA"].Value = dc.MasterBatchA;
                    //cmd.Parameters.Add("@MasterBatchACode", System.Data.SqlDbType.VarChar, 50);
                    //cmd.Parameters["@MasterBatchACode"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@MasterBatchACode"].Value = dc.MasterBatchACode;
                    cmd.Parameters.Add("@MasterBatchPCA", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@MasterBatchPCA"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MasterBatchPCA"].Value = dc.MasterBatchPCA;
                    //cmd.Parameters.Add("@MasterBatchB", System.Data.SqlDbType.VarChar, 50);
                    //cmd.Parameters["@MasterBatchB"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@MasterBatchB"].Value = dc.MasterBatchB;
                    //cmd.Parameters.Add("@MasterBatchBCode", System.Data.SqlDbType.VarChar, 50);
                    //cmd.Parameters["@MasterBatchBCode"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@MasterBatchBCode"].Value = dc.MasterBatchBCode;
                    cmd.Parameters.Add("@MasterBatchPCB", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@MasterBatchPCB"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MasterBatchPCB"].Value = dc.MasterBatchPCB;
                    cmd.Parameters.Add("@MasterBatchNotes", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@MasterBatchNotes"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MasterBatchNotes"].Value = dc.MasterBatchNotes;
                    //cmd.Parameters.Add("@Additive", System.Data.SqlDbType.VarChar, 20);
                    //cmd.Parameters["@Additive"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@Additive"].Value = dc.Additive;
                    cmd.Parameters.Add("@BlowingTime", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@BlowingTime"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@BlowingTime"].Value = dc.BlowingTime;
                    cmd.Parameters.Add("@AdditivePC", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@AdditivePC"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@AdditivePC"].Value = dc.AdditivePC;
                    cmd.Parameters.Add("@CycleTimeLower", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@CycleTimeLower"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CycleTimeLower"].Value = dc.CycleTimeLower;
                    cmd.Parameters.Add("@CycleTimeUpper", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@CycleTimeUpper"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CycleTimeUpper"].Value = dc.CycleTimeUpper;
                    cmd.Parameters.Add("@ZoneTemperature", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@ZoneTemperature"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ZoneTemperature"].Value = dc.ZoneTemperature;
                    cmd.Parameters.Add("@MouldCombination", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@MouldCombination"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MouldCombination"].Value = dc.MouldCombination;
                    cmd.Parameters.Add("@Extru_Pin", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@Extru_Pin"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Extru_Pin"].Value = dc.Extru_Pin;
                    cmd.Parameters.Add("@Extru_Die", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@Extru_Die"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Extru_Die"].Value = dc.Extru_Die;
                    cmd.Parameters.Add("@BlowPin", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@BlowPin"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@BlowPin"].Value = dc.BlowPin;
                    cmd.Parameters.Add("@CuttingRing", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@CuttingRing"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CuttingRing"].Value = dc.CuttingRing;
                    cmd.Parameters.Add("@Collar", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Collar"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Collar"].Value = dc.Collar;
                    cmd.Parameters.Add("@HotKnife", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@HotKnife"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@HotKnife"].Value = dc.HotKnife;
                    cmd.Parameters.Add("@StripperPlate", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@StripperPlate"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@StripperPlate"].Value = dc.StripperPlate;
                    cmd.Parameters.Add("@Insert", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@Insert"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Insert"].Value = dc.Insert;
                    cmd.Parameters.Add("@Punch", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@Punch"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Punch"].Value = dc.Punch;
                    cmd.Parameters.Add("@ParisonControl", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@ParisonControl"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ParisonControl"].Value = dc.ParisonControl;
                    cmd.Parameters.Add("@RotaryKnife", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@RotaryKnife"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@RotaryKnife"].Value = dc.RotaryKnife;
                    cmd.Parameters.Add("@PinchingGear", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@PinchingGear"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PinchingGear"].Value = dc.PinchingGear;
                    cmd.Parameters.Add("@AccessoriesNotes1", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@AccessoriesNotes1"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@AccessoriesNotes1"].Value = dc.AccessoriesNotes1;
                    cmd.Parameters.Add("@AccessoriesNotes2", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@AccessoriesNotes2"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@AccessoriesNotes2"].Value = dc.AccessoriesNotes2;
                    cmd.Parameters.Add("@LeakDetector", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@LeakDetector"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@LeakDetector"].Value = dc.LeakDetector;
                    cmd.Parameters.Add("@ChargePressure", System.Data.SqlDbType.VarChar, 10);
                    cmd.Parameters["@ChargePressure"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ChargePressure"].Value = dc.ChargePressure;
                    cmd.Parameters.Add("@TestTime", System.Data.SqlDbType.VarChar, 10);
                    cmd.Parameters["@TestTime"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@TestTime"].Value = dc.TestTime;
                    cmd.Parameters.Add("@PassPressure", System.Data.SqlDbType.VarChar, 10);
                    cmd.Parameters["@PassPressure"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PassPressure"].Value = dc.PassPressure;
                    cmd.Parameters.Add("@PressureTest", System.Data.SqlDbType.VarChar, 20);
                    cmd.Parameters["@PressureTest"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PressureTest"].Value = dc.PressureTest;
                    cmd.Parameters.Add("@ProductionSample", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@ProductionSample"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ProductionSample"].Value = dc.ProductionSample;
                    cmd.Parameters.Add("@ConformanceCertificate", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@ConformanceCertificate"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ConformanceCertificate"].Value = dc.ConformanceCertificate;
                    cmd.Parameters.Add("@SetupNote1", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@SetupNote1"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@SetupNote1"].Value = dc.SetupNote1;
                    cmd.Parameters.Add("@SetupNote2", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@SetupNote2"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@SetupNote2"].Value = dc.SetupNote2;
                    cmd.Parameters.Add("@SetupNote3", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@SetupNote3"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@SetupNote3"].Value = dc.SetupNote3;
                    cmd.Parameters.Add("@CurrentAsOf", System.Data.SqlDbType.Date, 0);
                    cmd.Parameters["@CurrentAsOf"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CurrentAsOf"].Value = dc.CurrentAsOf;
                    cmd.Parameters.Add("@CtnID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@CtnID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CtnID"].Value = dc.CtnID;
                    cmd.Parameters.Add("@last_updated_on", System.Data.SqlDbType.DateTime2, 0);
                    cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                    cmd.Parameters.Add("@last_updated_by", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;

                    cmd.ExecuteNonQuery();
              
                    //dc.SetupID = ((int)(cmd.Parameters["@SetupID"].Value));                                                           
                    dc.last_updated_on = ((System.DateTime)(cmd.Parameters["@last_updated_on"].Value));
                    dc.last_updated_by = ((string)(cmd.Parameters["@last_updated_by"].Value));
                    connection.Close();
                    RETURN_VALUE = ((int)(cmd.Parameters["@RETURN_VALUE"].Value));
                    return RETURN_VALUE;
                }
                else
                {
                    throw new System.ArgumentException("The connection must be closed when calling this method.");
                }
            }
        }
        public static int InsertBlowMouldMachineSetup(BMSetupData dc)        
        {
            int RETURN_VALUE = 0;
            System.Data.SqlClient.SqlCommand cmd = null;
            //System.Data.SqlClient.SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
            if ((connection == null))
            {
                throw new System.ArgumentException("The connection object cannot be null");
            }
            else
            {
                if ((connection.State == System.Data.ConnectionState.Closed))
                {
                    connection.Open();
                    cmd = new System.Data.SqlClient.SqlCommand("InsertBlowMouldMachineSetup", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RETURN_VALUE", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@RETURN_VALUE"].Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters["@RETURN_VALUE"].Value = RETURN_VALUE;
                    cmd.Parameters.Add("@SetupID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@SetupID"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@SetupID"].Value = dc.SetupID;
                    cmd.Parameters.Add("@MachineID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MachineID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MachineID"].Value = dc.MachineID;
                    cmd.Parameters.Add("@PmID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@PmID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PmID"].Value = dc.PmID;
                    cmd.Parameters.Add("@MaterialID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialID"].Value = dc.MaterialID;
                    cmd.Parameters.Add("@GradeID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@GradeID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@GradeID"].Value = dc.GradeID;
                    cmd.Parameters.Add("@CUSTNMBR", System.Data.SqlDbType.Char, 15);
                    cmd.Parameters["@CUSTNMBR"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CUSTNMBR"].Value = dc.CUSTNMBR;
                    cmd.Parameters.Add("@MaterialIDA", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialIDA"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialIDA"].Value = dc.MaterialIDA;
                    cmd.Parameters.Add("@MaterialAPC", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialAPC"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialAPC"].Value = dc.MaterialAPC;
                    cmd.Parameters.Add("@MaterialIDB", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialIDB"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialIDB"].Value = dc.MaterialIDB;
                    cmd.Parameters.Add("@MaterialBPC", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MaterialBPC"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialBPC"].Value = dc.MaterialBPC;
                    cmd.Parameters.Add("@MasterBatchIDA", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MasterBatchIDA"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MasterBatchIDA"].Value = dc.MasterBatchIDA;
                    cmd.Parameters.Add("@MasterBatchIDB", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@MasterBatchIDB"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MasterBatchIDB"].Value = dc.MasterBatchIDB;
                    cmd.Parameters.Add("@AdditiveID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@AdditiveID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@AdditiveID"].Value = dc.AdditiveID;
                    cmd.Parameters.Add("@MaterialNote", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@MaterialNote"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MaterialNote"].Value = dc.MaterialNote;
                    cmd.Parameters.Add("@WeightLower", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@WeightLower"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@WeightLower"].Value = dc.WeightLower;
                    cmd.Parameters.Add("@WeightHigher", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@WeightHigher"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@WeightHigher"].Value = dc.WeightHigher;
                    cmd.Parameters.Add("@TotalShot", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@TotalShot"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@TotalShot"].Value = dc.TotalShot;
                    cmd.Parameters.Add("@PackingStyle", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@PackingStyle"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PackingStyle"].Value = dc.PackingStyle;
                    cmd.Parameters.Add("@Lined", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@Lined"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Lined"].Value = dc.Lined;
                    cmd.Parameters.Add("@Wrapping", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Wrapping"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Wrapping"].Value = dc.Wrapping;
                    cmd.Parameters.Add("@PalletType", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@PalletType"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PalletType"].Value = dc.PalletType;
                    cmd.Parameters.Add("@PackingNotes", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@PackingNotes"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PackingNotes"].Value = dc.PackingNotes;
                    //cmd.Parameters.Add("@MasterBatchA", System.Data.SqlDbType.VarChar, 50);
                    //cmd.Parameters["@MasterBatchA"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@MasterBatchA"].Value = dc.MasterBatchA;
                    //cmd.Parameters.Add("@MasterBatchACode", System.Data.SqlDbType.VarChar, 50);
                    //cmd.Parameters["@MasterBatchACode"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@MasterBatchACode"].Value = dc.MasterBatchACode;
                    cmd.Parameters.Add("@MasterBatchPCA", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@MasterBatchPCA"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MasterBatchPCA"].Value = dc.MasterBatchPCA;
                    //cmd.Parameters.Add("@MasterBatchB", System.Data.SqlDbType.VarChar, 50);
                    //cmd.Parameters["@MasterBatchB"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@MasterBatchB"].Value = dc.MasterBatchB;
                    //cmd.Parameters.Add("@MasterBatchBCode", System.Data.SqlDbType.VarChar, 50);
                    //cmd.Parameters["@MasterBatchBCode"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@MasterBatchBCode"].Value = dc.MasterBatchBCode;
                    cmd.Parameters.Add("@MasterBatchPCB", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@MasterBatchPCB"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MasterBatchPCB"].Value = dc.MasterBatchPCB;
                    cmd.Parameters.Add("@MasterBatchNotes", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@MasterBatchNotes"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MasterBatchNotes"].Value = dc.MasterBatchNotes;
                    //cmd.Parameters.Add("@Additive", System.Data.SqlDbType.VarChar, 20);
                    //cmd.Parameters["@Additive"].Direction = System.Data.ParameterDirection.Input;
                    //cmd.Parameters["@Additive"].Value = dc.Additive;
                    cmd.Parameters.Add("@BlowingTime", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@BlowingTime"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@BlowingTime"].Value = dc.BlowingTime;
                    cmd.Parameters.Add("@AdditivePC", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@AdditivePC"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@AdditivePC"].Value = dc.AdditivePC;
                    cmd.Parameters.Add("@CycleTimeLower", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@CycleTimeLower"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CycleTimeLower"].Value = dc.CycleTimeLower;
                    cmd.Parameters.Add("@CycleTimeUpper", System.Data.SqlDbType.Real, 0);
                    cmd.Parameters["@CycleTimeUpper"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CycleTimeUpper"].Value = dc.CycleTimeUpper;
                    cmd.Parameters.Add("@ZoneTemperature", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@ZoneTemperature"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ZoneTemperature"].Value = dc.ZoneTemperature;
                    cmd.Parameters.Add("@MouldCombination", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@MouldCombination"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@MouldCombination"].Value = dc.MouldCombination;
                    cmd.Parameters.Add("@Extru_Pin", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@Extru_Pin"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Extru_Pin"].Value = dc.Extru_Pin;
                    cmd.Parameters.Add("@Extru_Die", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@Extru_Die"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Extru_Die"].Value = dc.Extru_Die;
                    cmd.Parameters.Add("@BlowPin", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@BlowPin"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@BlowPin"].Value = dc.BlowPin;
                    cmd.Parameters.Add("@CuttingRing", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@CuttingRing"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CuttingRing"].Value = dc.CuttingRing;
                    cmd.Parameters.Add("@Collar", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@Collar"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Collar"].Value = dc.Collar;
                    cmd.Parameters.Add("@HotKnife", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@HotKnife"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@HotKnife"].Value = dc.HotKnife;
                    cmd.Parameters.Add("@StripperPlate", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@StripperPlate"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@StripperPlate"].Value = dc.StripperPlate;
                    cmd.Parameters.Add("@Insert", System.Data.SqlDbType.VarChar, 100);
                    cmd.Parameters["@Insert"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Insert"].Value = dc.Insert;
                    cmd.Parameters.Add("@Punch", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@Punch"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@Punch"].Value = dc.Punch;
                    cmd.Parameters.Add("@ParisonControl", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@ParisonControl"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ParisonControl"].Value = dc.ParisonControl;
                    cmd.Parameters.Add("@RotaryKnife", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@RotaryKnife"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@RotaryKnife"].Value = dc.RotaryKnife;
                    cmd.Parameters.Add("@PinchingGear", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@PinchingGear"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PinchingGear"].Value = dc.PinchingGear;
                    cmd.Parameters.Add("@AccessoriesNotes1", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@AccessoriesNotes1"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@AccessoriesNotes1"].Value = dc.AccessoriesNotes1;
                    cmd.Parameters.Add("@AccessoriesNotes2", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@AccessoriesNotes2"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@AccessoriesNotes2"].Value = dc.AccessoriesNotes2;
                    cmd.Parameters.Add("@LeakDetector", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@LeakDetector"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@LeakDetector"].Value = dc.LeakDetector;
                    cmd.Parameters.Add("@ChargePressure", System.Data.SqlDbType.VarChar, 10);
                    cmd.Parameters["@ChargePressure"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ChargePressure"].Value = dc.ChargePressure;
                    cmd.Parameters.Add("@TestTime", System.Data.SqlDbType.VarChar, 10);
                    cmd.Parameters["@TestTime"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@TestTime"].Value = dc.TestTime;
                    cmd.Parameters.Add("@PassPressure", System.Data.SqlDbType.VarChar, 10);
                    cmd.Parameters["@PassPressure"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PassPressure"].Value = dc.PassPressure;
                    cmd.Parameters.Add("@PressureTest", System.Data.SqlDbType.VarChar, 20);
                    cmd.Parameters["@PressureTest"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@PressureTest"].Value = dc.PressureTest;
                    cmd.Parameters.Add("@ProductionSample", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@ProductionSample"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ProductionSample"].Value = dc.ProductionSample;
                    cmd.Parameters.Add("@ConformanceCertificate", System.Data.SqlDbType.Bit, 0);
                    cmd.Parameters["@ConformanceCertificate"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@ConformanceCertificate"].Value = dc.ConformanceCertificate;
                    cmd.Parameters.Add("@SetupNote1", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@SetupNote1"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@SetupNote1"].Value = dc.SetupNote1;
                    cmd.Parameters.Add("@SetupNote2", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@SetupNote2"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@SetupNote2"].Value = dc.SetupNote2;
                    cmd.Parameters.Add("@SetupNote3", System.Data.SqlDbType.VarChar, 200);
                    cmd.Parameters["@SetupNote3"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@SetupNote3"].Value = dc.SetupNote3;
                    cmd.Parameters.Add("@CurrentAsOf", System.Data.SqlDbType.Date, 0);
                    cmd.Parameters["@CurrentAsOf"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CurrentAsOf"].Value = dc.CurrentAsOf;
                    cmd.Parameters.Add("@CtnID", System.Data.SqlDbType.Int, 0);
                    cmd.Parameters["@CtnID"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters["@CtnID"].Value = dc.CtnID;
                    cmd.Parameters.Add("@last_updated_on", System.Data.SqlDbType.DateTime2, 0);
                    cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                    cmd.Parameters.Add("@last_updated_by", System.Data.SqlDbType.VarChar, 50);
                    cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                    cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                    
                    cmd.ExecuteNonQuery();
                    
                    // The Parameter @RETURN_VALUE is not an output type
                    dc.SetupID = ((int)(cmd.Parameters["@SetupID"].Value));                    
                    dc.last_updated_on = ((System.DateTime)(cmd.Parameters["@last_updated_on"].Value));
                    dc.last_updated_by = ((string)(cmd.Parameters["@last_updated_by"].Value));
                    connection.Close();
                    RETURN_VALUE = ((int)(cmd.Parameters["@RETURN_VALUE"].Value));
                    return RETURN_VALUE;
                }
                else
                {
                    throw new System.ArgumentException("The connection must be closed when calling this method.");
                }
            }
        }

    }
}



