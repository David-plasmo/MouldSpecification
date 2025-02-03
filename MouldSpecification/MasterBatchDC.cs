using System;

namespace MouldSpecification
{
    public class MasterBatchDC
    {
        public int MBID { get; set; }
        public string MBCode { get; set; }
        public string MBColour { get; set; }
        public decimal CostPerKg { get; set; }
        public string Supplier { get; set; }
        public string Comment { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public MasterBatchDC()
        {

        }
        public MasterBatchDC(int MBID_, string MBCode_, string MBColour_, decimal CostPerKg_,
            string Supplier_, string Comment_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.MBID = MBID_;
            this.MBCode = MBCode_;
            this.MBColour = MBColour_;
            this.CostPerKg = CostPerKg_;
            this.Supplier = Supplier_;
            this.Comment = Comment_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }
    }
}
