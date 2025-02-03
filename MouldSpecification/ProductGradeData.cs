using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{

	public class ProductGradeData
	{
		public int GradeID { get; set; }
		public string ShortDesc { get; set; }
		public string Description { get; set; }
		public string ImagePath { get; set; }

		public ProductGradeData()
		{
		}

		public ProductGradeData(int GradeID_, string ShortDesc_, string Description_, string ImagePath_)
		{
			this.GradeID = GradeID_;
			this.ShortDesc = ShortDesc_;
			this.Description = Description_;
			this.ImagePath = ImagePath_;
		}
	}
}
