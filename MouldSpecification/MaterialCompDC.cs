using System;

namespace MouldSpecification
{
    public class MaterialCompDC
    {
        public int MaterialCompID { get; set; }
        public int? MaterialGradeID { get; set; }
        //public int? MaterialID { get; set; }
        public int ItemID { get; set; }
        public int Polymer123 { get; set; }
        public Single PolymerPercent { get; set; }
        public Single RegrindMaxPC { get; set; }
        public bool IsActive { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public MaterialCompDC(int MaterialCompID_, int? MaterialGradeID_, /*int? MaterialID_,*/ int ItemID_, int Polymer123_, Single PolymerPercent_,
            Single RegrindMaxPC_, bool IsActive_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.MaterialCompID = MaterialCompID_;
            this.MaterialGradeID = MaterialGradeID_;
            //this.MaterialID = MaterialID_;
            this.ItemID = ItemID_;
            this.Polymer123 = Polymer123_;
            this.PolymerPercent = PolymerPercent_;
            this.RegrindMaxPC = RegrindMaxPC_;
            this.IsActive = IsActive_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }

        public MaterialCompDC() { }

    }
}
