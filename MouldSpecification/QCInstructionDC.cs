using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
    public class QCInstructionDC
    {
        public int QCInstructionID { get; set; }
        public int ItemID { get; set; }
        public int InstructionNo { get; set; }
        public string QCInstruction { get; set; }
        public string QCImageFilepath { get; set; }

        public QCInstructionDC(int QCInstructionID_, int ItemID_, int InstructionNo_, string QCInstruction_, string QCImageFilepath_)
        {
            this.QCInstructionID = QCInstructionID_;
            this.ItemID = ItemID_;
            this.InstructionNo = InstructionNo_;
            this.QCInstruction = QCInstruction_;
            this.QCImageFilepath = QCImageFilepath_;
        }

        public QCInstructionDC() { }
    }   
}
