using System;

namespace MouldSpecification
{
    public class PackagingDC
    {
        public int PackingID { get; set; }
        public int ItemID { get; set; }
        public int? CtnID { get; set; }
        public int? PalletID { get; set; }
        public bool PackedInCtn { get; set; }
        public int CtnQty { get; set; }
        public bool Liner { get; set; }
        public bool InnerBag { get; set; }
        public int BagQty { get; set; }
        public string PackingStyle { get; set; }
        public bool PackedOnPallet { get; set; }
        public int PalQty { get; set; }
        public int CtnsPerPallet { get; set; }
        public bool PalletCover { get; set; }
        public string Wrapping { get; set; }
        public string LabelInnerBag { get; set; }
        public string BarcodeLabel { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public PackagingDC(int PackingID_, int ItemID_, int? CtnID_, int PalletID_, bool PackedInCtn_, int CtnQty_, bool Liner_, bool InnerBag_, int BagQty_, string PackingStyle_, bool PackedOnPallet_, int PalQty_, int CtnsPerPallet_, bool PalletCover_, string Wrapping_, string LabelInnerBag_, string BarcodeLabel_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.PackingID = PackingID_;
            this.ItemID = ItemID_;
            this.CtnID = CtnID_;
            this.PalletID = PalletID_;
            this.PackedInCtn = PackedInCtn_;
            this.CtnQty = CtnQty_;
            this.Liner = Liner_;
            this.InnerBag = InnerBag_;
            this.BagQty = BagQty_;
            this.PackingStyle = PackingStyle_;
            this.PackedOnPallet = PackedOnPallet_;
            this.PalQty = PalQty_;
            this.CtnsPerPallet = CtnsPerPallet_;
            this.PalletCover = PalletCover_;
            this.Wrapping = Wrapping_;
            this.LabelInnerBag = LabelInnerBag_;
            this.BarcodeLabel = BarcodeLabel_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }
    
        public PackagingDC()
        {
        }
    }
}
