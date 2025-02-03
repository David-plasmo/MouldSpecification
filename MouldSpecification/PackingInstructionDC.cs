using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
    public class PackingInstructionDC
    {
        public int PackingInstructionID { get; set; }
        public int ItemID { get; set; }
        public int InstructionNo { get; set; }
        public string PackingInstruction { get; set; }

        public PackingInstructionDC(int PackingInstructionID_, int ItemID_, int InstructionNo_, string PackingInstruction_)
        {
            this.PackingInstructionID = PackingInstructionID_;
            this.ItemID = ItemID_;
            this.InstructionNo = InstructionNo_;
            this.PackingInstruction = PackingInstruction_;
        }

        public PackingInstructionDC() { }

    }
}
