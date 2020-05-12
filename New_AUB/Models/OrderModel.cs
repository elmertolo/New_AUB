using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_AUB.Models
{
    class OrderModel
    {
        private string returnBlankIfNull(string _input)
        {
            if (_input == null)
                return "";
            else
                return _input;
        }

        public string BRSTN { get; set; }
        public string AccountNo { get; set; }
        private string _accountName2;
        public string AccountName
        {
            get
            {
                return (returnBlankIfNull(_accountName2));
            }
            set { _accountName2 = value; }
        }
        public string AccountName2 { get; set; }
        public Int64 Quantity { get; set; }
        public string ChkType { get; set; }
        public string PcsPerbook { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string StartingSerial { get; set; }
        public string EndingSerial { get; set; }
        public string BranchName { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string Address6 { get; set; }
        public string BranchCode { get; set; }
        public string BaeStock { get; set; }
        public string Company { get; set; }

    }


}
