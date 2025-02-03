using System;

namespace MouldSpecification
{
    public class CustomerPriceQtyDC
    {
        public int CostID { get; set; }
        public int ItemID { get; set; }
        public int CustomerID { get; set; }
        public int? PricingQty { get; set; }
        public decimal? CalculatedPrice { get; set; }
        public decimal? CurrentPrice { get; set; }
        public DateTime? DateChanged { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public CustomerPriceQtyDC(int CostID_, int ItemID_, int CustomerID_, int PricingQty_, decimal CalculatedPrice_, decimal CurrentPrice_, DateTime DateChanged_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.CostID = CostID_;
            this.ItemID = ItemID_;
            this.CustomerID = CustomerID_;
            this.PricingQty = PricingQty_;
            this.CalculatedPrice = CalculatedPrice_;
            this.CurrentPrice = CurrentPrice_;
            this.DateChanged = DateChanged_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }

        public CustomerPriceQtyDC() { }

    }
}
