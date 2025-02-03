using System;

namespace MouldSpecification
{
    public class MasterBatchCompDC
    {
        public int MBCompID { get; set; }
        public int? MBID { get; set; }
        public int ItemID { get; set; }
        public int MB123 { get; set; }
        public Single MBPercent { get; set; }
        public bool IsPreferred { get; set; }
        public int? AdditiveID { get; set; }
        public Single AdditivePC { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public MasterBatchCompDC(int MBCompID_, int? MBID_, int ItemID_, int MB123_, Single MBPercent_, bool IsPreferred_, int? AdditiveID_, Single AdditivePC_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.MBCompID = MBCompID_;
            this.MBID = MBID_;
            this.ItemID = ItemID_;
            this.MB123 = MB123_;
            this.MBPercent = MBPercent_;
            this.IsPreferred = IsPreferred_;
            this.AdditiveID = AdditiveID_;
            this.AdditivePC = AdditivePC_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }


        public MasterBatchCompDC() { }

    }

}
