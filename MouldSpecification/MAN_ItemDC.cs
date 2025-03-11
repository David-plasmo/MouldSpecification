using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
    public class MAN_ItemDC
    {
        public int ItemID { get; set; }
        public string ITEMNMBR { get; set; }
        public string ITEMDESC { get; set; }
        public string AltCode { get; set; }
        public string ProductType { get; set; }
        public int GradeID { get; set; }
        public string ImageFile { get; set; }
        public decimal ComponentWeight { get; set; }
        public decimal SprueRunnerTotal { get; set; }
        public decimal TotalShotWeight { get; set; }
        public string CompDB { get; set; }
        public string ITMCLSCD { get; set; }
        public int CtnQty { get; set; }
        public int? CartonID { get; set; }
        public int? LabelTypeID { get; set; }
        public string BottleSize { get; set; }
        public string Style { get; set; }
        public string NeckSize { get; set; }
        public string Colour { get; set; }
        public Boolean DangerousGood { get; set; }
        public Boolean StockLine { get; set; }
        public DateTime last_updated_on { get; set; }
        public string last_updated_by { get; set; }

        public MAN_ItemDC(int ItemID_, string ITEMNMBR_, string ITEMDESC_, string AltCode_, string ProductType_, int GradeID_, string ImageFile_, decimal ComponentWeight_, decimal SprueRunnerTotal_, decimal TotalShotWeight_, string CompDB_, string ITMCLSCD_, int CtnQty_, int CartonID_, int LabelTypeID_, string BottleSize_, string Style_, string NeckSize_, string Colour_, Boolean DangerousGood_, Boolean StockLine_, DateTime last_updated_on_, string last_updated_by_)
        {
            this.ItemID = ItemID_;
            this.ITEMNMBR = ITEMNMBR_;
            this.ITEMDESC = ITEMDESC_;
            this.AltCode = AltCode_;
            this.ProductType = ProductType_;
            this.GradeID = GradeID_;
            this.ImageFile = ImageFile_;
            this.ComponentWeight = ComponentWeight_;
            this.SprueRunnerTotal = SprueRunnerTotal_;
            this.TotalShotWeight = TotalShotWeight_;
            this.CompDB = CompDB_;
            this.ITMCLSCD = ITMCLSCD_;
            this.CtnQty = CtnQty_;
            this.CartonID = CartonID_;
            this.LabelTypeID = LabelTypeID_;
            this.BottleSize = BottleSize_;
            this.Style = Style_;
            this.NeckSize = NeckSize_;
            this.Colour = Colour_;
            this.DangerousGood = DangerousGood_;
            this.StockLine = StockLine_;
            this.last_updated_on = last_updated_on_;
            this.last_updated_by = last_updated_by_;

        }

        public MAN_ItemDC() { }

    }
}
