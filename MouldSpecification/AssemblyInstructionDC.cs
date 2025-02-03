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
        /// <summary>
        /// Gets or sets the unique identifier for the assembly instruction.
        /// </summary>
        public int AssemblyInstructionID { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the item associated with the assembly instruction.
        /// </summary>
        public int ItemID { get; set; }

        /// <summary>
        /// Gets or sets the instruction number for the assembly instruction.
        /// </summary>
        public int InstructionNo { get; set; }

        /// <summary>
        /// Gets or sets the textual description of the assembly instruction.
        /// </summary>
        public string AssemblyInstruction { get; set; }

        /// <summary>
        /// Gets or sets the file path to the image associated with the assembly instruction.
        /// </summary>
        public string AssemblyImageFilePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInstructionDC"/> class with the specified values.
        /// </summary>
        /// <param name="AssemblyInstructionID_"> The unique identifier for the assembly instruction. </param>
        /// <param name="ItemID_"> The identifier for the item associated with the instruction. </param>
        /// <param name="InstructionNo_"> The instruction number. </param>
        /// <param name="AssemblyInstruction_"> The textual description of the instruction. </param>
        /// <param name="assemblyImageFilePath"> The file path to the image associated with the instruction. </param>
        public AssemblyInstructionDC(int AssemblyInstructionID_, int ItemID_, int InstructionNo_, string AssemblyInstruction_, string assemblyImageFilePath)
        {
            // Assign the provided values to the corresponding properties
            this.AssemblyInstructionID = AssemblyInstructionID_;
            this.ItemID = ItemID_;
            this.InstructionNo = InstructionNo_;
            this.AssemblyInstruction = AssemblyInstruction_;
            this.AssemblyImageFilePath = assemblyImageFilePath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInstructionDC"/> class with default values.
        /// </summary>
        public AssemblyInstructionDC()
        { 
            //Empty constructor 
        }

    }
}
