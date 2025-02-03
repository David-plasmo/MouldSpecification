namespace MouldSpecification
{
    public class CustomerProductDC
    {
        public int CustomerProductID { get; set; }
        public int CustomerID { get; set; }
        public int ItemID { get; set; }

        public CustomerProductDC(int CustomerProductID_, int CustomerID_, int ItemID_)
        {
            this.CustomerProductID = CustomerProductID_;
            this.CustomerID = CustomerID_;
            this.ItemID = ItemID_;

        }

        public CustomerProductDC() { }

    }
}
