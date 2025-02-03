using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
	public class BMSetupData
	{
		public int SetupID { get; set; }
		public int? MachineID { get; set; }
		public int? PmID { get; set; }
		public int? MaterialID { get; set; }
		public int? GradeID { get; set; }
		public string CUSTNMBR { get; set; }
		public int? MaterialIDA { get; set; }
		public int? MaterialAPC { get; set; }
		public int? MaterialIDB { get; set; }
		public int? MaterialBPC { get; set; }
		public int? MasterBatchIDA { get; set; }
		public int? MasterBatchIDB { get; set; }
		public int? AdditiveID { get; set; }
		public string MaterialNote { get; set; }
		public Single? WeightLower { get; set; }
		public Single? WeightHigher { get; set; }
		public Single? TotalShot { get; set; }
		public string PackingStyle { get; set; }
		public bool? Lined { get; set; }
		public string Wrapping { get; set; }
		public string PalletType { get; set; }
		public string PackingNotes { get; set; }
		public Single? MasterBatchPCA { get; set; }
		public Single? MasterBatchPCB { get; set; }
		public string MasterBatchNotes { get; set; }
		public int? BlowingTime { get; set; }
		public int? AdditivePC { get; set; }
		public int? AdditiveBID { get; set; }
		public int? AdditiveBPC { get; set; }
		public Single? CycleTimeLower { get; set; }
		public Single? CycleTimeUpper { get; set; }
		public string ZoneTemperature { get; set; }
		public string MouldCombination { get; set; }
		public string Extru_Pin { get; set; }
		public string Extru_Die { get; set; }
		public string BlowPin { get; set; }
		public string CuttingRing { get; set; }
		public string Collar { get; set; }
		public bool? HotKnife { get; set; }
		public string StripperPlate { get; set; }
		public string Insert { get; set; }
		public bool? Punch { get; set; }
		public bool? ParisonControl { get; set; }
		public bool? RotaryKnife { get; set; }
		public bool? PinchingGear { get; set; }
		public string AccessoriesNotes1 { get; set; }
		public string AccessoriesNotes2 { get; set; }
		public bool? LeakDetector { get; set; }
		public string ChargePressure { get; set; }
		public string TestTime { get; set; }
		public string PassPressure { get; set; }
		public string PressureTest { get; set; }
		public bool? ProductionSample { get; set; }
		public bool? ConformanceCertificate { get; set; }
		public string SetupNote1 { get; set; }
		public string SetupNote2 { get; set; }
		public string SetupNote3 { get; set; }
		public DateTime? CurrentAsOf { get; set; } //= DateTime.MinValue;
		public int? CtnID { get; set; }
		public DateTime? last_updated_on { get; set; } //= DateTime.MinValue;
		public string last_updated_by { get; set; }

		public BMSetupData()
		{

		}
		public BMSetupData(int SetupID_, int? MachineID_, int? PmID_, int? MaterialID_, int? GradeID_, string CUSTNMBR_, int? MaterialIDA_, int? MaterialAPC_, int? MaterialIDB_,
			int? MaterialBPC_, int? MasterBatchIDA_, int? MasterBatchIDB_, int? AdditiveID_,
			string MaterialNote_, Single? WeightLower_, Single? WeightHigher_, Single? TotalShot_, string PackingStyle_, bool? Lined_, string Wrapping_, 
			string PalletType_, string PackingNotes_,  Single? MasterBatchPCA_, Single? MasterBatchPCB_, string MasterBatchNotes_, int? BlowingTime_, 
			int? AdditivePC_, int? AdditiveBID_, int? AdditiveBPC_, Single? CycleTimeLower_, Single? CycleTimeUpper_, string ZoneTemperature_, string MouldCombination_, string Extru_Pin_, 
			string Extru_Die_, string BlowPin_, string CuttingRing_, string Collar_, bool? HotKnife_, string StripperPlate_, string Insert_, bool? Punch_, bool? ParisonControl_, 
			bool? RotaryKnife_, bool? PinchingGear_, string AccessoriesNotes1_, string AccessoriesNotes2_, bool? LeakDetector_, string ChargePressure_, 
			string TestTime_, string PassPressure_, string PressureTest_, bool? ProductionSample_, bool? ConformanceCertificate_, string SetupNote1_, 
			string SetupNote2_, string SetupNote3_, DateTime? CurrentAsOf_, int? CtnID_, DateTime? last_updated_on_, string last_updated_by_)
		{
			this.SetupID = SetupID_;
			this.MachineID = MachineID_;
			this.PmID = PmID_;
			this.MaterialID = MaterialID_;
			this.GradeID = GradeID_;
			this.CUSTNMBR = CUSTNMBR_;
			this.MaterialIDA = MaterialIDA_;
			this.MaterialAPC = MaterialAPC_;
			this.MaterialIDB = MaterialIDB_;
			this.MaterialBPC = MaterialBPC_;
			this.MasterBatchIDA = MasterBatchIDA_;
			this.MasterBatchIDB = MasterBatchIDB_;
			this.AdditiveID = AdditiveID_;
			this.MaterialNote = MaterialNote_;
			this.WeightLower = WeightLower_;
			this.WeightHigher = WeightHigher_;
			this.TotalShot = TotalShot_;
			this.PackingStyle = PackingStyle_;
			this.Lined = Lined_;
			this.Wrapping = Wrapping_;
			this.PalletType = PalletType_;
			this.PackingNotes = PackingNotes_;
			this.MasterBatchPCA = MasterBatchPCA_;
			this.MasterBatchPCB = MasterBatchPCB_;
			this.MasterBatchNotes = MasterBatchNotes_;
			this.BlowingTime = BlowingTime_;
			this.AdditivePC = AdditivePC_;
			this.AdditiveBID = AdditiveBID_;
			this.AdditiveBPC = AdditiveBPC_;
			this.CycleTimeLower = CycleTimeLower_;
			this.CycleTimeUpper = CycleTimeUpper_;
			this.ZoneTemperature = ZoneTemperature_;
			this.MouldCombination = MouldCombination_;
			this.Extru_Pin = Extru_Pin_;
			this.Extru_Die = Extru_Die_;
			this.BlowPin = BlowPin_;
			this.CuttingRing = CuttingRing_;
			this.Collar = Collar_;
			this.HotKnife = HotKnife_;
			this.StripperPlate = StripperPlate_;
			this.Insert = Insert_;
			this.Punch = Punch_;
			this.ParisonControl = ParisonControl_;
			this.RotaryKnife = RotaryKnife_;
			this.PinchingGear = PinchingGear_;
			this.AccessoriesNotes1 = AccessoriesNotes1_;
			this.AccessoriesNotes2 = AccessoriesNotes2_;
			this.LeakDetector = LeakDetector_;
			this.ChargePressure = ChargePressure_;
			this.TestTime = TestTime_;
			this.PassPressure = PassPressure_;
			this.PressureTest = PressureTest_;
			this.ProductionSample = ProductionSample_;
			this.ConformanceCertificate = ConformanceCertificate_;
			this.SetupNote1 = SetupNote1_;
			this.SetupNote2 = SetupNote2_;
			this.SetupNote3 = SetupNote3_;
			this.CurrentAsOf = CurrentAsOf_;
			this.CtnID = CtnID_;
			this.last_updated_on = last_updated_on_;
			this.last_updated_by = last_updated_by_;
		}
	}
}
