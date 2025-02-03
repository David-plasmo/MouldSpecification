using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
   	public class AdHocPriceQtyData
	{
		public int AHPID { get; set; }
		public int? PmID { get; set; }
		public decimal Price { get; set; }
		public DateTime? DateChanged { get; set; }
		public DateTime? last_updated_on { get; set; }
		public string last_updated_by { get; set; }
		public int? PricingQuantity { get; set; }
		public int? CustID { get; set; }
		
		public AdHocPriceQtyData()
        {

        }
		public AdHocPriceQtyData(int AHPID_, int? PmID_, decimal Price_, DateTime? DateChanged_, DateTime? last_updated_on_, string last_updated_by_, int? PricingQuantity_, int? CustID_)
		{
			this.AHPID = AHPID_;
			this.PmID = PmID_;
			this.Price = Price_;
			this.DateChanged = DateChanged_;
			this.last_updated_on = last_updated_on_;
			this.last_updated_by = last_updated_by_;
			this.PricingQuantity = PricingQuantity_;
			this.CustID = CustID_;
		}
	}
}
