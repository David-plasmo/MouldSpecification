using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
    public class PackingImageDC
    {
        public int PackingImageID { get; set; }
        public int ItemID { get; set; }
        public string PackingImageFilepath1 { get; set; }
        public string PackingImageFilepath2 { get; set; }
        public string PackingImageFilepath3 { get; set; }

        public PackingImageDC(int PackingImageID_, int ItemID_, string PackingImageFilepath1_, string PackingImageFilepath2_, string PackingImageFilepath3_)
        {
            this.PackingImageID = PackingImageID_;
            this.ItemID = ItemID_;
            this.PackingImageFilepath1 = PackingImageFilepath1_;
            this.PackingImageFilepath2 = PackingImageFilepath2_;
            this.PackingImageFilepath3 = PackingImageFilepath3_;
        }

        public PackingImageDC() { }

    }
}
