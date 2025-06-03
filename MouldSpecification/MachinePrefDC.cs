using System;

namespace MouldSpecification
{
    public class MachinePrefDC
    {
        public int MachPrefID { get; set; }
        public int MachineID { get; set; }
        public int ProgramNo { get; set; }
        public int ItemID { get; set; }
        public string MachineABC { get; set; }
        public double CycleTime { get; set; }
        public int NoPartsPerHour { get; set; }
        public Boolean IsPreferred { get; set; }
        public int BMMachineNo { get; set; }
        public int CycleTimeFrom { get; set; }
        public int CycleTimeTo { get; set; }
        public int BlowingTime { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public MachinePrefDC(int MachPrefID_, int MachineID_, int ProgramNo_, int ItemID_, string MachineABC_, double CycleTime_, int NoPartsPerHour_, Boolean IsPreferred_, int BMMachineNo_, int CycleTimeFrom_, int CycleTimeTo_, int BlowingTime_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.MachPrefID = MachPrefID_;
            this.MachineID = MachineID_;
            this.ProgramNo = ProgramNo_;
            this.ItemID = ItemID_;
            this.MachineABC = MachineABC_;
            this.CycleTime = CycleTime_;
            this.NoPartsPerHour = NoPartsPerHour_;
            this.IsPreferred = IsPreferred_;
            this.BMMachineNo = BMMachineNo_;
            this.CycleTimeFrom = CycleTimeFrom_;
            this.CycleTimeTo = CycleTimeTo_;
            this.BlowingTime = BlowingTime_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;

        }

        public MachinePrefDC() { }

    }    
}
