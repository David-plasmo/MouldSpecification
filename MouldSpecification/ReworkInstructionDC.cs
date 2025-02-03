using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
    public class ReworkInstructionDC
    {
        public int ReworkInstructionID { get; set; }
        public int ItemID { get; set; }
        public int InstructionNo { get; set; }
        public string ReworkInstruction { get; set; }

        public ReworkInstructionDC(int ReworkInstructionID_, int ItemID_, int InstructionNo_, string ReworkInstruction_)
        {
            this.ReworkInstructionID = ReworkInstructionID_;
            this.ItemID = ItemID_;
            this.InstructionNo = InstructionNo_;
            this.ReworkInstruction = ReworkInstruction_;
        }

        public ReworkInstructionDC() { }

    }
}
