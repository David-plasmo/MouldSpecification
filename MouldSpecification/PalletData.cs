using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
	public class PalletData
	{
	    public int PalletID { get; set; }
		public string Pallet { get; set; }

		public PalletData()
		{

		}
		public PalletData(int PalletID_, string Pallet_)
		{
			this.PalletID = PalletID_;
			this.Pallet = Pallet_;
		}
	}
}
