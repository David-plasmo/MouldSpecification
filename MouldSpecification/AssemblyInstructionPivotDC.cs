using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
    public class AssemblyInstructionPivotDC
    {
        public int ItemID1 { get; set; }
        public int InstructionNo1 { get; set; }
        public int AssemblyInstructionID1 { get; set; }
        public string AssemblyInstruction1 { get; set; }
        public string AssemblyImageFilePath1 { get; set; }
        public int ItemID2 { get; set; }
        public int AssemblyInstructionID2 { get; set; }
        public int InstructionNo2 { get; set; }
        public string AssemblyInstruction2 { get; set; }
        public string AssemblyImageFilePath2 { get; set; }

        public AssemblyInstructionPivotDC(int ItemID1_, int InstructionNo1_, int AssemblyInstructionID1_, string AssemblyInstruction1_, string AssemblyImageFilePath1_, int ItemID2_, int AssemblyInstructionID2_, int InstructionNo2_, string AssemblyInstruction2_, string AssemblyImageFilePath2_)
        {
            this.ItemID1 = ItemID1_;
            this.InstructionNo1 = InstructionNo1_;
            this.AssemblyInstructionID1 = AssemblyInstructionID1_;
            this.AssemblyInstruction1 = AssemblyInstruction1_;
            this.AssemblyImageFilePath1 = AssemblyImageFilePath1_;
            this.ItemID2 = ItemID2_;
            this.AssemblyInstructionID2 = AssemblyInstructionID2_;
            this.InstructionNo2 = InstructionNo2_;
            this.AssemblyInstruction2 = AssemblyInstruction2_;
            this.AssemblyImageFilePath2 = AssemblyImageFilePath2_;

        }

        public AssemblyInstructionPivotDC() { }

    }
}
