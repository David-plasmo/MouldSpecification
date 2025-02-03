using System;

namespace MouldSpecification
{
    public class MaterialGradeDC
    {
        public int MaterialGradeID { get; set; }
        public int MaterialID { get; set; }
        public string MaterialGrade { get; set; }
        public decimal CostPerKg { get; set; }
        public string Supplier { get; set; }
        public string Comment { get; set; }
        public string AdditionalNotes { get; set; }
        public string MachineType { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public MaterialGradeDC(int MaterialGradeID_, int MaterialID_, string MaterialGrade_, decimal CostPerKg_, string Supplier_, string Comment_, string AdditionalNotes_, 
            string MachineType_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.MaterialGradeID = MaterialGradeID_;
            this.MaterialID = MaterialID_;
            this.MaterialGrade = MaterialGrade_;
            this.CostPerKg = CostPerKg_;
            this.Supplier = Supplier_;
            this.Comment = Comment_;
            this.AdditionalNotes = AdditionalNotes_;
            this.MachineType = MachineType_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }

        public MaterialGradeDC() { }
    }
}
