using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService;

namespace MouldSpecification
{
	public class MaterialGradeData
	{
		public int MaterialGradeID { get; set; }
		public int MaterialID { get; set; }		
		public string MaterialGrade { get; set; }
		public decimal CostPerKg { get; set; }
		public string Supplier { get; set; }
		public string Comment { get; set; }
		public string last_updated_by { get; set; }
		public DateTime last_updated_on { get; set; }

		public MaterialGradeData()
		{

		}

		public MaterialGradeData(int MaterialGradeID_, int MaterialID_, string MaterialGrade_, decimal CostPerKg_, 
			string Supplier_, string Comment_, string last_updated_by_, DateTime last_updated_on_)
		{
			this.MaterialGradeID = MaterialGradeID_;
			this.MaterialID = MaterialID_;
			this.MaterialGrade = MaterialGrade_;
			this.CostPerKg = CostPerKg_;
			this.Supplier = Supplier_;
			this.Comment = Comment_;
			this.last_updated_by = last_updated_by_;
			this.last_updated_on = last_updated_on_;
		}
	}
}
