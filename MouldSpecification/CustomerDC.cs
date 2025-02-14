using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouldSpecification
{
    public class CustomerDC
    {
        public int CustomerID { get; set; }
        public string CompDB { get; set; }
        public string CUSTNMBR { get; set; }
        public string CUSTNAME { get; set; }
        public string CUSTCLAS { get; set; }
        public string CNTCPRSN { get; set; }
        public string ADRSCODE { get; set; }
        public string SHIPMTHD { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string COUNTRY { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string ZIP { get; set; }
        public string PHONE1 { get; set; }
        public string PHONE2 { get; set; }
        public string PHONE3 { get; set; }
        public string FAX { get; set; }
        public string PYMTRMID { get; set; }
        public string LOCNCODE { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public CustomerDC(int CustomerID_, string CompDB_, string CUSTNMBR_, string CUSTNAME_, string CUSTCLAS_, 
            string CNTCPRSN_, string ADRSCODE_, string SHIPMTHD_, string ADDRESS1_, string ADDRESS2_, 
            string ADDRESS3_, string COUNTRY_, string CITY_, string STATE_, string ZIP_, string PHONE1_, 
            string PHONE2_, string PHONE3_, string FAX_, string PYMTRMID_, string LOCNCODE_, 
            string last_updated_by_, DateTime last_updated_on_)
        {
            this.CustomerID = CustomerID_;
            this.CompDB = CompDB_;
            this.CUSTNMBR = CUSTNMBR_;
            this.CUSTNAME = CUSTNAME_;
            this.CUSTCLAS = CUSTCLAS_;
            this.CNTCPRSN = CNTCPRSN_;
            this.ADRSCODE = ADRSCODE_;
            this.SHIPMTHD = SHIPMTHD_;
            this.ADDRESS1 = ADDRESS1_;
            this.ADDRESS2 = ADDRESS2_;
            this.ADDRESS3 = ADDRESS3_;
            this.COUNTRY = COUNTRY_;
            this.CITY = CITY_;
            this.STATE = STATE_;
            this.ZIP = ZIP_;
            this.PHONE1 = PHONE1_;
            this.PHONE2 = PHONE2_;
            this.PHONE3 = PHONE3_;
            this.FAX = FAX_;
            this.PYMTRMID = PYMTRMID_;
            this.LOCNCODE = LOCNCODE_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;

        }

        public CustomerDC() { }

    }
}
