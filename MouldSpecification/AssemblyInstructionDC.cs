using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
    /// <summary>
    /// Represents an assembly instruction in the system.
    /// </summary>
    public class AssemblyInstructionDC
    {
        public int AssemblyInstructionID { get; set; }
        public int ItemID { get; set; }
        public int InstructionNo { get; set; }
        public string AssemblyInstruction { get; set; }
        public string AssemblyImageFilePath { get; set; }

        public AssemblyInstructionDC(int AssemblyInstructionID_, int ItemID_, int InstructionNo_, string AssemblyInstruction_, string AssemblyImageFilePath_)
        {
            this.AssemblyInstructionID = AssemblyInstructionID_;
            this.ItemID = ItemID_;
            this.InstructionNo = InstructionNo_;
            this.AssemblyInstruction = AssemblyInstruction_;
            this.AssemblyImageFilePath = AssemblyImageFilePath_;

        }

        public AssemblyInstructionDC() { }

    }
}
