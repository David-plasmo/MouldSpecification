using System;

namespace MouldSpecification
{
    public class MaterialTypeDC
    {
        public int MaterialID { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public MaterialTypeDC(int MaterialID_, string ShortDesc_, string Description_, string Comment_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.MaterialID = MaterialID_;
            this.ShortDesc = ShortDesc_;
            this.Description = Description_;
            this.Comment = Comment_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }

        public MaterialTypeDC() { }

    }
}
