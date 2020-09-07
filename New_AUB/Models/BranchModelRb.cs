using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_AUB.Models
{
    class BranchModelRb
    {
        private string returnBlankIfNull(string _input)
        {
            if (_input == null)
                return "";
            else
                return _input;
        }

        public string BRSTN { get; set; }
        //public string BranchName { get; set; }
        private string _address1;
        public string Address1
        {
            get
            {
                return (returnBlankIfNull(_address1));
            }
            set { _address1 = value; }
        }
        private string _address2;
        public string Address2
        {
            get
            {
                return (returnBlankIfNull(_address2));
            }
            set { _address2 = value; }
        }
        private string _address3;
        public string Address3
        {
            get
            {
                return (returnBlankIfNull(_address3));
            }
            set { _address3 = value; }
        }
        private string _address4;
        public string Address4
        {
            get
            {
                return (returnBlankIfNull(_address4));
            }
            set { _address4 = value; }
        }
        private string _address5;
        public string Address5
        {
            get
            {
                return (returnBlankIfNull(_address5));
            }
            set { _address5 = value; }
        }
        private string _address6;
        public string Address6
        {
            get
            {
                return (returnBlankIfNull(_address6));
            }
            set { _address6 = value; }
        }
        private string _accountNo;
        public string AccountNo
        {
            get
            {
                return (returnBlankIfNull(_accountNo));
            }
            set { _accountNo = value; }
        }
        public Int64 LastNo { get; set; }
        public  DateTime ModifiedDate { get; set; }

    }
}
