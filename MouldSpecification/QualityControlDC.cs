using System;

namespace MouldSpecification
{
    public class QualityControlDC
    {
        public int QualityControlID { get; set; }
        public int ItemID { get; set; }
        public string FinishedPTQC { get; set; }
        public bool ProductSample { get; set; }
        public bool CertificateOfConformance { get; set; }
        public string Notes { get; set; }
        public string LabelIcon { get; set; }
        public string Costing { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public QualityControlDC(int QualityControlID_, int ItemID_, string FinishedPTQC_, bool ProductSample_, bool CertificateOfConformance_, string Notes_, string LabelIcon_, string Costing_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.QualityControlID = QualityControlID_;
            this.ItemID = ItemID_;
            this.FinishedPTQC = FinishedPTQC_;
            this.ProductSample = ProductSample_;
            this.CertificateOfConformance = CertificateOfConformance_;
            this.Notes = Notes_;
            this.LabelIcon = LabelIcon_;
            this.Costing = Costing_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }

        public QualityControlDC() { }
    }
}

