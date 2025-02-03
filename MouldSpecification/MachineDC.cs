using System;

namespace MouldSpecification
{
    public class MachineDC
    {
        public int MachineID { get; set; }
        public string Machine { get; set; }
        public string Capacity { get; set; }
        public string Type { get; set; }
        public decimal CostPerHour { get; set; }
        public string Comment { get; set; }
        public DateTime last_updated_on { get; set; }
        public string last_updated_by { get; set; }

        public MachineDC()
        {

        }

        public MachineDC(int MachineID_, string Machine_, string Capacity_, string Type_, decimal CostPerHour_,
            string Comment_, DateTime last_updated_on_, string last_updated_by_)
        {
            this.MachineID = MachineID_;
            this.Machine = Machine_;
            this.Capacity = Capacity_;
            this.Type = Type_;
            this.CostPerHour = CostPerHour_;
            this.Comment = Comment_;
            this.last_updated_on = last_updated_on_;
            this.last_updated_by = last_updated_by_;
        }
    }
}
