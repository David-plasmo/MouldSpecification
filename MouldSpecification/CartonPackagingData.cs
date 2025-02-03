using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
	public class CartonPackagingData
	{
		public int CtnID { get; set; }
		public int GPCartonID { get; set; }
		public string CartonType { get; set; }
		public decimal CartonCost { get; set; }
		public string LinerType { get; set; }
		public decimal LinerCost { get; set; }
		public string InnerBag { get; set; }
		public decimal InnerBagCost { get; set; }
		//public float WidthMM { get; set; }
		//public float HeightMM { get; set; }
		//public float DepthMM { get; set; }
		//public int PalletQty { get; set; }
		public string Comment { get; set; }
		public string last_updated_by { get; set; }
		public DateTime last_updated_on { get; set; }

		public CartonPackagingData()
		{

		}
		public CartonPackagingData(
			int CtnID_, 
			int GPCartonID_,
			string CartonType_, decimal CartonCost_, 
			string LinerType_, decimal LinerCost_, 
			string InnerBag_, decimal InnerBagCost_, 
			//float WidthMM_, float HeightMM_, float DepthMM_,
			//int PalletQty_,
			string Comment_,
			string last_updated_by_, DateTime last_updated_on_)
		{
			this.CtnID = CtnID_;
			this.GPCartonID = GPCartonID_;
			this.CartonType = CartonType_;
			this.CartonCost = CartonCost_;
			this.LinerType = LinerType_;
			this.LinerCost = LinerCost_;
			this.InnerBag = InnerBag_;
			this.InnerBagCost = InnerBagCost_;
			//this.WidthMM = WidthMM_;
			//this.HeightMM = HeightMM_;
			//this.DepthMM = DepthMM_;
			//this.PalletQty = PalletQty_;
			this.Comment = Comment_;
			this.last_updated_by = last_updated_by_;
			this.last_updated_on = last_updated_on_;			
		}
	}
}
