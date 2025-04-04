using System;

namespace MouldSpecification
{
    public class IMSpecificationDC
    {
        public int MouldID { get; set; }
        public int ItemID { get; set; }
        public string MouldNumber { get; set; }
        public string MouldLocation { get; set; }
        public string MouldOwner { get; set; }
        public Boolean FamilyMould { get; set; }
        public int NoOfCavities { get; set; }
        public int NoOfPart { get; set; }
        public string PartSummary { get; set; }
        public string Operation { get; set; }
        public string OtherFeatures { get; set; }
        public string FixedHalf { get; set; }
        public string FixedHalfTemp { get; set; }
        public string MovingHalf { get; set; }
        public string MovingHalfTemp { get; set; }
        public string PremouldReq { get; set; }
        public string PostMouldReq { get; set; }
        public Boolean AdditionalLabourReqd { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public IMSpecificationDC(int MouldID_, int ItemID_, string MouldNumber_, string MouldLocation_, string MouldOwner_, Boolean FamilyMould_, int NoOfCavities_, int NoOfPart_, string PartSummary_, string Operation_, string OtherFeatures_, string FixedHalf_, string FixedHalfTemp_, string MovingHalf_, string MovingHalfTemp_, string PremouldReq_, string PostMouldReq_, Boolean AdditionalLabourReqd_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.MouldID = MouldID_;
            this.ItemID = ItemID_;
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
            this.FixedHalfTemp = FixedHalfTemp_;
            this.MovingHalf = MovingHalf_;
            this.MovingHalfTemp = MovingHalfTemp_;
            this.PremouldReq = PremouldReq_;
            this.PostMouldReq = PostMouldReq_;
            this.AdditionalLabourReqd = AdditionalLabourReqd_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;

        }

        public IMSpecificationDC() { }

    }
}
