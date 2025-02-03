using System;

namespace MouldSpecification
{
    public class FixedCostDC
    {
        public int FixedCostID { get; set; }
        public string FixedCostDesc { get; set; }
        public decimal FixedCost { get; set; }
        public string Comment { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public FixedCostDC(int FixedCostID_, string FixedCostDesc_, decimal FixedCost_, string Comment_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.FixedCostID = FixedCostID_;
            this.FixedCostDesc = FixedCostDesc_;
            this.FixedCost = FixedCost_;
            this.Comment = Comment_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }

        public FixedCostDC() { }

    }
}
