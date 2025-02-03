using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
	public class IMSpecificationData
	{
		public int ImID { get; set; }
		public int PastelID { get; set; }
		public int PmID { get; set; }
		public string Image { get; set; }
		public int MachineAID { get; set; }
		public int MachineAProgram { get; set; }
		public int MachineBID { get; set; }
		public int MachineBProgram { get; set; }
		public int MachineCID { get; set; }
		public int MachineCProgram { get; set; }
		public int Polymer1TypeID { get; set; }
		public int Polymer1GradeID { get; set; }
		public int Polymer1PC { get; set; }
		public int Polymer2TypeID { get; set; }
		public int Polymer2GradeID { get; set; }
		public int Polymer2PC { get; set; }
		public string AdditionalNotes { get; set; }
		public int MBID { get; set; }
		public Single MBPC { get; set; }
		public int AdditiveACodeID { get; set; }
		public Single AdditiveAPC { get; set; }
		public int AdditiveBCodeID { get; set; }
		public Single AdditiveBPC { get; set; }
		public int RegrindMaxPC { get; set; }
		public string MouldNumber { get; set; }
		public string MouldLocation { get; set; }
		public string MouldOwner { get; set; }
		public string FamilyMould { get; set; }
		public int NoOfCavities { get; set; }
		public int NoOfPart { get; set; }
		public string PartSummary { get; set; }
		public string Operation { get; set; }
		public string OtherFeatures { get; set; }
		public string FixedHalf { get; set; }
		public string FixedHalfDegC { get; set; }
		public string MovingHalf { get; set; }
		public string MovingHalfDegC { get; set; }
		public Single ComponentWeightTotalFamily { get; set; }
		public Single SpruePlusRunner { get; set; }
		public Single TotalShotWeight { get; set; }
		public Single CycleTimeA { get; set; }
		public int NoPartsPerHrA { get; set; }
		public int CycleTimeB { get; set; }
		public int NoPartsPerHrB { get; set; }
		public int CycleTimeC { get; set; }
		public int NoPartsPerHrC { get; set; }
		public string PremouldReq { get; set; }
		public string PostMouldReq { get; set; }
		public string FinishedPTandQC { get; set; }
		public bool ProductSample { get; set; }
		public bool CertificateOfConformance { get; set; }
		public bool PackedInCtn { get; set; }
		public string CtnType { get; set; }
		public int CtnQty { get; set; }
		public bool Liner { get; set; }
		public bool InnerBag { get; set; }
		public int BagQty { get; set; }
		public string PackingStyle { get; set; }
		public bool PackedOnPallet { get; set; }
		public string PalletType { get; set; }
		public int PalQty { get; set; }
		public int CtnsPerPallet { get; set; }
		public bool PalletCover { get; set; }
		public string Wrapping { get; set; }
		public string LabelInnerBag { get; set; }
		public int BarcodeLabel { get; set; }
		public string PackerQCInstructions { get; set; }
		public string PackerQCInstruction2 { get; set; }
		public string PackerQCInstruction3 { get; set; }
		public string PackerQCInstruction4 { get; set; }
		public string PackerQCInstruction5 { get; set; }
		public string PackingImage1 { get; set; }
		public string PackingImage2 { get; set; }
		public string PackingImage3 { get; set; }
		public string ReworkInstructions1 { get; set; }
		public string ReworkInstructions2 { get; set; }
		public string AssemblyInstructions { get; set; }
		public string AssemblyImage1 { get; set; }
		public string AssemblyImage2 { get; set; }
		public string AssemblyImage3 { get; set; }
		public string AssemblyImage4 { get; set; }
		public string AssemblyImage5 { get; set; }
		public string AssemblyImage6 { get; set; }
		public string Notes { get; set; }
		public string QCInstruction1 { get; set; }
		public string QCImage1 { get; set; }
		public string QCInstruction2 { get; set; }
		public string QCImage2 { get; set; }
		public string QCInstruction3 { get; set; }
		public string QCImage3 { get; set; }
		public string QCInstruction4 { get; set; }
		public string QCImage4 { get; set; }
		public string Field1 { get; set; }
		public string Costing { get; set; }
		public string Field2 { get; set; }
		public DateTime last_updated_on { get; set; }
		public string last_updated_by { get; set; }
		public int CtnID { get; set; }
		public bool AdditionalLabour { get; set; }
		public int PalletDays { get; set; }
		public int PalletID { get; set; }
		public int MBBID { get; set; }
		public Single MBBPC { get; set; }
		public int GradeID { get; set; }
		public int MaterialID { get; set; }
		public int CustID { get; set; }

		public IMSpecificationData()
		{
		}

		public IMSpecificationData(int ImID_, int PastelID_, int PmID_, string Image_, int MachineAID_, int MachineAProgram_, int MachineBID_,
			int MachineBProgram_, int MachineCID_, int MachineCProgram_, int Polymer1TypeID_, int Polymer1GradeID_, int Polymer1PC_,
			int Polymer2TypeID_, int Polymer2GradeID_, int Polymer2PC_, string AdditionalNotes_,
			int MBID_, Single MBPC_, int AdditiveACodeID_, Single AdditiveAPC_, int AdditiveBCodeID_, Single AdditiveBPC_,
			int RegrindMaxPC_, string MouldNumber_, string MouldLocation_, string MouldOwner_, string FamilyMould_,
			int NoOfCavities_, int NoOfPart_, string PartSummary_, string Operation_, string OtherFeatures_, string FixedHalf_,
			string FixedHalfDegC_, string MovingHalf_, string MovingHalfDegC_, Single ComponentWeightTotalFamily_,
			Single SpruePlusRunner_, Single TotalShotWeight_, Single CycleTimeA_, int NoPartsPerHrA_, int CycleTimeB_,
			int NoPartsPerHrB_, int CycleTimeC_, int NoPartsPerHrC_, string PremouldReq_, string PostMouldReq_,
			string FinishedPTandQC_, bool ProductSample_, bool CertificateOfConformance_, bool PackedInCtn_,
			string CtnType_, int CtnQty_, bool Liner_, bool InnerBag_, int BagQty_, string PackingStyle_,
			bool PackedOnPallet_, string PalletType_, int PalQty_, int CtnsPerPallet_, bool PalletCover_,
			string Wrapping_, string LabelInnerBag_, int BarcodeLabel_, string PackerQCInstructions_,
			string PackerQCInstruction2_, string PackerQCInstruction3_, string PackerQCInstruction4_, string PackerQCInstruction5_,
			string PackingImage1_, string PackingImage2_, string PackingImage3_, string ReworkInstructions1_,
			string ReworkInstructions2_, string AssemblyInstructions_, string AssemblyImage1_, string AssemblyImage2_,
			string AssemblyImage3_, string AssemblyImage4_, string AssemblyImage5_, string AssemblyImage6_, string Notes_,
			string QCInstruction1_, string QCImage1_, string QCInstruction2_, string QCImage2_, string QCInstruction3_,
			string QCImage3_, string QCInstruction4_, string QCImage4_, string Field1_, string Costing_, string Field2_,
			DateTime last_updated_on_, string last_updated_by_, int CtnID_, bool AdditionalLabour_, int PalletDays_,
			int PalletID_, int MBBID_, Single MBBPC_, int GradeID_, int MaterialID_, int CustID_)
		{
			this.ImID = ImID_;
			this.PastelID = PastelID_;
			this.PmID = PmID_;
			this.Image = Image_;
			this.MachineAID = MachineAID_;
			this.MachineAProgram = MachineAProgram_;
			this.MachineBID = MachineBID_;
			this.MachineBProgram = MachineBProgram_;
			this.MachineCID = MachineCID_;
			this.MachineCProgram = MachineCProgram_;
			this.Polymer1TypeID = Polymer1TypeID_;
			this.Polymer1GradeID = Polymer1GradeID_;
			this.Polymer1PC = Polymer1PC_;
			this.Polymer2TypeID = Polymer2TypeID_;
			this.Polymer2GradeID = Polymer2GradeID_;
			this.Polymer2PC = Polymer2PC_;
			this.AdditionalNotes = AdditionalNotes_;
			this.MBID = MBID_;
			this.MBPC = MBPC_;
			this.AdditiveACodeID = AdditiveACodeID_;
			this.AdditiveAPC = AdditiveAPC_;
			this.AdditiveBCodeID = AdditiveBCodeID_;
			this.AdditiveBPC = AdditiveBPC_;
			this.RegrindMaxPC = RegrindMaxPC_;
			this.MouldNumber = MouldNumber_;
			this.MouldLocation = MouldLocation_;
			this.MouldOwner = MouldOwner_;
			this.FamilyMould = FamilyMould_;
			this.NoOfCavities = NoOfCavities_;
			this.NoOfPart = NoOfPart_;
			this.PartSummary = PartSummary_;
			this.Operation = Operation_;
			this.OtherFeatures = OtherFeatures_;
			this.FixedHalf = FixedHalf_;
			this.FixedHalfDegC = FixedHalfDegC_;
			this.MovingHalf = MovingHalf_;
			this.MovingHalfDegC = MovingHalfDegC_;
			this.ComponentWeightTotalFamily = ComponentWeightTotalFamily_;
			this.SpruePlusRunner = SpruePlusRunner_;
			this.TotalShotWeight = TotalShotWeight_;
			this.CycleTimeA = CycleTimeA_;
			this.NoPartsPerHrA = NoPartsPerHrA_;
			this.CycleTimeB = CycleTimeB_;
			this.NoPartsPerHrB = NoPartsPerHrB_;
			this.CycleTimeC = CycleTimeC_;
			this.NoPartsPerHrC = NoPartsPerHrC_;
			this.PremouldReq = PremouldReq_;
			this.PostMouldReq = PostMouldReq_;
			this.FinishedPTandQC = FinishedPTandQC_;
			this.ProductSample = ProductSample_;
			this.CertificateOfConformance = CertificateOfConformance_;
			this.PackedInCtn = PackedInCtn_;
			this.CtnType = CtnType_;
			this.CtnQty = CtnQty_;
			this.Liner = Liner_;
			this.InnerBag = InnerBag_;
			this.BagQty = BagQty_;
			this.PackingStyle = PackingStyle_;
			this.PackedOnPallet = PackedOnPallet_;
			this.PalletType = PalletType_;
			this.PalQty = PalQty_;
			this.CtnsPerPallet = CtnsPerPallet_;
			this.PalletCover = PalletCover_;
			this.Wrapping = Wrapping_;
			this.LabelInnerBag = LabelInnerBag_;
			this.BarcodeLabel = BarcodeLabel_;
			this.PackerQCInstructions = PackerQCInstructions_;
			this.PackerQCInstruction2 = PackerQCInstruction2_;
			this.PackerQCInstruction3 = PackerQCInstruction3_;
			this.PackerQCInstruction4 = PackerQCInstruction4_;
			this.PackerQCInstruction5 = PackerQCInstruction5_;
			this.PackingImage1 = PackingImage1_;
			this.PackingImage2 = PackingImage2_;
			this.PackingImage3 = PackingImage3_;
			this.ReworkInstructions1 = ReworkInstructions1_;
			this.ReworkInstructions2 = ReworkInstructions2_;
			this.AssemblyInstructions = AssemblyInstructions_;
			this.AssemblyImage1 = AssemblyImage1_;
			this.AssemblyImage2 = AssemblyImage2_;
			this.AssemblyImage3 = AssemblyImage3_;
			this.AssemblyImage4 = AssemblyImage4_;
			this.AssemblyImage5 = AssemblyImage5_;
			this.AssemblyImage6 = AssemblyImage6_;
			this.Notes = Notes_;
			this.QCInstruction1 = QCInstruction1_;
			this.QCImage1 = QCImage1_;
			this.QCInstruction2 = QCInstruction2_;
			this.QCImage2 = QCImage2_;
			this.QCInstruction3 = QCInstruction3_;
			this.QCImage3 = QCImage3_;
			this.QCInstruction4 = QCInstruction4_;
			this.QCImage4 = QCImage4_;
			this.Field1 = Field1_;
			this.Costing = Costing_;
			this.Field2 = Field2_;
			this.last_updated_on = last_updated_on_;
			this.last_updated_by = last_updated_by_;
			this.CtnID = CtnID_;
			this.AdditionalLabour = AdditionalLabour_;
			this.PalletDays = PalletDays_;
			this.PalletID = PalletID_;
			this.MBBID = MBBID_;
			this.MBBPC = MBBPC_;
			this.GradeID = GradeID_;
			this.MaterialID = MaterialID_;
			this.CustID = CustID_;
		}
	}
}

