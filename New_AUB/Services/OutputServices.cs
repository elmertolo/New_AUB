using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using New_AUB.Models;


namespace New_AUB.Services
{
    class OutputServices
    {
        private static string GenerateSpace(int _noOfSpaces)
        {
            string output = "";

            for (int x = 0; x < _noOfSpaces; x++)
            {
                output += " ";
            }

            return output;

        }//END OF FUNCTION

        private static string Seperator()
        {
            return "";
        }

        public static string ConvertToBlockText(List<OrderModel> _check, string _prodType,string _ChkType, string _batchNumber, DateTime _deliveryDate, string _preparedBy)

        {

            int page = 1, lineCount = 14, blockCounter = 1, blockContent = 1;
            string date = DateTime.Now.ToString("MMM. dd, yyyy");
            bool noFooter = true;
            string countText = "";
            string output = "";

            //Sort Check List
            var sort = (from c in _check
                        orderby c.BRSTN
                        ascending
                        select c).ToList();

            output += "\n" + GenerateSpace(8) + "Page No. " + page.ToString() + "\n" +
            GenerateSpace(8) + date +
            "\n";
            if ((_prodType == "Regular Checks" && _ChkType == "PERSONAL")|| (_prodType == "Starter Checks" && _ChkType =="PERSONAL"))
            {
                output += GenerateSpace(27) + "SUMMARY OF BLOCK - " + _ChkType.ToUpper() + "\n" +
                  GenerateSpace(30) + "AUB " + _prodType + "\n" +
                  GenerateSpace(21) + "U S E   C A P T I V E   B A S E S T O C K ! ! !" + "\n\n" +
                  GenerateSpace(28) + "Base Stock: 8 Outs (Cutsheet) --> Melinda\n\n" +
                  GenerateSpace(5) + "Starting Batch " + _batchNumber + ", New MICR Alignment of NCDSS is 15-54 ! ! !\n\n" +
                  GenerateSpace(14) + "Hyphen: 5-5-1" + GenerateSpace(5) + "Additional 0 (zero) are in 6th Digit" + "\n\n\n";
            }
            else if ((_prodType == "Regular Checks" && _ChkType == "COMMERCIAL") || (_prodType == "Starter Checks" && _ChkType == "COMMERCIAL"))
            {
                output += GenerateSpace(27) + "SUMMARY OF BLOCK - " + _ChkType.ToUpper() + "\n" +
                  GenerateSpace(30) + "AUB " + _prodType + "\n" +
                  GenerateSpace(21) + "U S E   C A P T I V E   B A S E S T O C K ! ! !" + "\n\n" +
                  GenerateSpace(28) + "Base Stock: 8 Outs (Cutsheet) --> Melinda\n\n" +
                  GenerateSpace(5) + "Starting Batch " + _batchNumber + ", New MICR Alignment of NCDSS is 15-54 ! ! !\n\n" +
                  GenerateSpace(14) + "Hyphen: 5-5-1" + GenerateSpace(5) + "Additional 0 (zero) are in 6th Digit" + "\n\n\n";
            }
            else if(_prodType == "Continues Check w/ Voucher")
            {
                output += GenerateSpace(20) + "AUB " + _ChkType + "\n" +

                 GenerateSpace(23) + "Base Stock: 1 Out (Cutsheet) --> Melinda\n\n" +
                 GenerateSpace(3) + "Acct No. With hypen is on Account Name field if no Blank name or no RB Acct No" +
                 GenerateSpace(5) + "Starting Batch " + _batchNumber + ", New MICR Alignment of NCDSS is 15-54 ! ! !\n\n";
 
            }
            else if (_prodType == "MANAGER'S CHECK")
            {
                output += GenerateSpace(27) + "SUMMARY OF BLOCK - " + _ChkType.ToUpper() + "\n" +
                  GenerateSpace(30) + "AUB " + Encode.outputfolder + "\n" +

                  GenerateSpace(28) + "50 Pcs. / Book" + "\n" +
                  GenerateSpace(23) + "A L L  M A N U A L  E N C O D E D\n" +
                  GenerateSpace(8) + "Pls. DISREGARD the Series given on the Hard-copy by AUB\n" +
                  GenerateSpace(17) + "since Series are only maintained by CPC\n" +
                  GenerateSpace(8) + "Pls. Disregard Branch Name on Hard Copy given by AUB\n" +
                  GenerateSpace(9) + "Branch Name depends on the BRSTN on Regular Checks\n\n\n" +
                  GenerateSpace(3) + "Acct No. With hyphen is on Account Name field if no Blank name or no RB Acct No\n\n" +
                  GenerateSpace(5) + "Starting Batch " + _batchNumber + ", New MICR Alignment of NCDSS is 15-54 ! ! !\n\n" +
                  GenerateSpace(14) + "Hyphen: 5-5-1" + GenerateSpace(5) + "Additional 0 (zero) are in 6th Digit" + "\n\n\n";
               
            }
            output += GenerateSpace(8) + "BLOCK RT_NO" + GenerateSpace(5) + "M ACCT_NO" + GenerateSpace(9) + "START_NO." + GenerateSpace(2) + "END_NO.\n\n";
            Int64 checkTypeCount = 0;
            foreach (var check in sort)
            {


                if (_ChkType == "PERSONAL")
                {
                    checkTypeCount = check.Quantity;
                    while (check.StartingSerial.Length < 7)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 7)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                else
                {

                    while (check.StartingSerial.Length < 10)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 10)
                        check.EndingSerial = "0" + check.EndingSerial;
                }


                if (blockContent == 1)
                {
                    output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                    lineCount += 2;
                }

                if (blockContent == 5)
                {
                    blockContent = 2;

                    blockCounter++;

                    output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                }
                else
                {
                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                    lineCount += 1;

                    blockContent++;
                }
            }
            //if (lineCount >=61 )
            //{
            if (noFooter) //ADD FOOTER
            {
                output += "\n " + _batchNumber + GenerateSpace(46) + "DLVR: " + _deliveryDate.ToString("MM-dd(ddd)") + "\n\n" +
                    " A = " + checkTypeCount + GenerateSpace(20) + _check[0].FileName + ".txt\n" +
                    countText +
                    GenerateSpace(4) + "Prepared By" + GenerateSpace(3) + ": " + _preparedBy + "\t\t\t\t RECHECKED BY:\n" +
                    GenerateSpace(4) + "Updated By" + GenerateSpace(4) + ": " + _preparedBy + "\n" +
                    GenerateSpace(4) + "Time Start" + GenerateSpace(4) + ": " + DateTime.Now.ToShortTimeString() + "\n" +
                    GenerateSpace(4) + "Time Finished :\n" +
                    GenerateSpace(4) + "File rcvd" + GenerateSpace(5) + ":\n";

                noFooter = false;
            }

            // output += Seperator();

            lineCount = 1;
            //}

            return output;

        }

        public static string ConvertToPackingList(List<OrderModel> _checks, string _checkType, frmMain _mainForm)
        {
            var listofbrstn = _checks.Select(e => e.BRSTN).Distinct().ToList();
            int page = 1;
            string date = DateTime.Now.ToShortDateString();
            string output = "";
            int i = 0;

            foreach (string brstn in listofbrstn)
            {

                output += "\n Page No. " + page.ToString() + "\n " +
                                  date + "\n" +
                                  GenerateSpace(29) + "CAPTIVE PRINTING CORPORATION\n" +
                                  GenerateSpace(28) + "AUB - " + _checkType + " Checks Summary\n\n" +
                                  GenerateSpace(2) + "ACCT_NO" + GenerateSpace(9) + "ACCOUNT NAME" + GenerateSpace(21) + "QTY CT START #" + GenerateSpace(4) + "END #\n\n\n";

                var listofchecks = _checks.Where(e => e.BRSTN == brstn).ToList();
                output += " ** ORDERS OF BRSTN " + _checks[i].BRSTN + " " + _checks[i].BranchName+ "\n\n" +
                              " * BATCH #: " + _mainForm.batchfile + "\n\n";



                foreach (var check in listofchecks)
                {

                    if (_checkType == "PERSONAL")
                    {
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {
                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }//END OF ADDING ZERO IN SERIES NUMBER

                    output += GenerateSpace(2) + check.AccountNo + GenerateSpace(4);

                    if (check.AccountName.Length < 50)
                        output += check.AccountName + GenerateSpace(50 - check.AccountName.Length);
                    else if (check.AccountName.Length > 50)
                        output += check.AccountName2.Substring(0, 50);

                    output += "  1 " + check.ChkType + GenerateSpace(2) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + " \n";
                    if (check.AccountName2 != "")
                        output += GenerateSpace(18) + check.AccountName2 + "\n";
                }

                output += "\n";
                output += "  * * * Sub Total * * * " + listofchecks.Count + "\n";

                page++;
                i++;

            }
            output += "  * * * Grand Total * * * " + _checks.Count + "\n";
            return output;

        }// end of function


        public static string ConvertToPrinterFile(List<OrderModel> _checkModels)
        {

            //var listofcheck = _checkModel.Select(e => e.BRSTN).OrderBy(e => e).ToList();

            string output = "";
            var sort = (from c in _checkModels
                        orderby c.BRSTN, c.AccountNo
                        ascending
                        select c).ToList();


            foreach (var check in sort)
            {
                Int64 Series = 0;
                if (check.ChkType == "B")
                {
                    Series = Int64.Parse(check.StartingSerial) - 1 + 100;
                }
                else
                {
                    Series = Int64.Parse(check.StartingSerial) - 1 + 50;
                }
                Int64 endSeries = Series - 1;

                string outputStartSeries = check.StartingSerial.ToString();

                string outputEndSeries = endSeries.ToString();

                //   string brstnFormat = "";

                string txtSeries = Series.ToString();

                if (check.ChkType == "A")
                {
                    while (check.StartingSerial.Length < 7)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 7)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                else
                {
                    while (check.StartingSerial.Length < 10)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 10)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                output += "10\n" + //1 (FIXED)
                        // "10\n" + //2 (FIXED)
                         check.BRSTN + "\n" + //3  (BRSTN)
                         //check.BRSTN + "\n" + //4  (BRSTN)
                         check.AccountNo + "\n" + //5 (ACCT NUMBER)
                         //check.AccountNo + "\n" + //6 (ACCT NUMBER)
                         Series.ToString() + "\n" + //7 (Start Series + PCS per Book)
                         //Series.ToString() + "\n" + //8 (Start Series + PCS per Book)
                         check.ChkType + "\n" + //9 (FIXED)
                         //check.ChkType + "\n" + //10 (FIXED)
                         "\n" + //11 (BLANK)
                         //"\n" + //12 (BLANK)
                  //       check.BRSTN.Substring(0, 5) + "\n"; //13 BRSTN FORMATTED
                         check.BRSTN.Substring(0, 5) + "\n" +//14 BRSTN FORMATTED
                         " " + check.BRSTN.Substring(5, 4) + "\n" + //15 BRSTN FORMATTED
                         //" " + check.BRSTN.Substring(5, 4) + "\n" + //16 BRSTN FORMATTED
                         check.AccountNo.Substring(0, 5) + "-" + check.AccountNo.Substring(5, 5) + "-" + check.AccountNo.Substring(10, 1) + "\n" + //17 (ACCT NUMBER)
                //check.AccountNo.Substring(0, 5) + "-" + check.AccountNo.Substring(5, 5) + "-" + check.AccountNo.Substring(10, 1) + "\n" + //18 (ACCT NUMBER)
                         check.AccountName + "\n" + //19 (NAME 1)
                //check.Name1 + "\n" + //20 (NAME 1)
                         "SN\n" + //21 (FIXED)
                //"SN\n" + //22 (FIXED)
                         "\n" + //23 (BLANK) 
                //"\n" + //24 (BLANK) 
                         check.AccountName2 + "\n" + //25 (NAME 2)
                //check.Name2 + "\n" + //26 (NAME 2)
                         "\n" + //27 (FIXED)
                //"\n" + //28 (FIXED)
                         "\n" + //29 (BLANK)
                //"\n" +//30 (BLANK)
                         "\n" + //31 (BLANK)
                //"\n" +//32(BLANK)
                         check.BranchName + "\n" + //33 (ADDRESS 1)
                //check.Address1 + "\n" + //34 (ADDRESS 1)
                         check.Address2 + "\n" + //35 (ADDRESS 2)
                //check.Address2 + "\n" + //36 (ADDRESS 2)
                         check.Address3 + "\n" + //37 (ADDRESS 3)
               // check.Address3 + "\n" + //38 (ADDRESS 3)
                         check.Address4 + "\n" + //39 (ADDRESS 4)
                //check.Address4 + "\n" + //40 (ADDRESS 4)
                         check.Address5 + "\n" + //41 (ADDRESS 5)
               //check.Address5 + "\n" + //42 (ADDRESS 5)
                         "\n" +//43 (BLANK)
                //"\n" +//44 (BLANK)
                         "ISLA BANK\n" +//45 (FIXED)
                //"ISLA BANK\n" +//46 (FIXED)
                         "\n" + //47 (BLANK)//
               // "\n" + //48 (BLANK)
                         "\n" + //49 (BLANK)
                //"\n" + //50 (BLANK)
                         "\n" + //51 (BLANK)
                // "\n" + //52 (BLANK)
                         "\n" + //53 (BLANK)
               // "\n" + //54 (BLANK)
                         "\n" + //55 (BLANK)
                //"\n" + //56 (BLANK)
                         "\n" + //57 (BLANK)
               // "\n" + //58 (BLANK)
                         "\n" + //59 (BLANK)
               // "\n" + //60 (BLANK)
                         "\n" + //61 (BLANK)
                //"\n" + //62 (BLANK)
                check.StartingSerial + "\n" + //63 (STARTING SERIES)
                //check.StartSeries + "\n" + //64 (STARTING SERIES)
                check.EndingSerial + "\n";  //65 (ENDING SERIES)
                //check.EndSeries + "\n"; //66 (ENDING SERIES)     
                //if(sort.Count % 4 == 0)
                //              output +=     "\\" + "\n";
            }

            return output;
        }
        public static string ConvertToBlockTextRB(List<OrderModelRb> _check, string _ChkType, string _batchNumber, DateTime _deliveryDate, string _preparedBy)

        {
            int page = 1, lineCount = 14, blockCounter = 1, blockContent = 1;
            string date = DateTime.Now.ToString("MMM. dd, yyyy");
            bool noFooter = true;
            string countText = "";
            string output = "";


            //Sort Check List
            var sort = (from c in _check
                        orderby c.BRSTN
                        ascending
                        select c).ToList();

            output += "\n" + GenerateSpace(8) + "Page No. " + page.ToString() + "\n" +
            GenerateSpace(8) + date +
            "\n" +
            GenerateSpace(27) + "SUMMARY OF BLOCK - " + _ChkType.ToUpper() + "\n" +
            GenerateSpace(30) + "AUB Regular Checks" + "\n" +
            GenerateSpace(21) + "U S E   C A P T I V E   B A S E S T O C K ! ! !" + "\n\n" +
            GenerateSpace(28) + "Base Stock: 8 Outs (Cutsheet) --> MELiNDA\n\n" +
            GenerateSpace(5) + "Starting Batch " + _batchNumber + ", New MICR Alignment of NCDSS is 15-54 ! ! !\n\n" +
            GenerateSpace(14) + "Hyphen: 5-5-1" + GenerateSpace(5) + "Additional 0 (zero) are in 6th Digit" + "\n\n\n" +
            GenerateSpace(8) + "BLOCK RT_NO" + GenerateSpace(5) + "M ACCT_NO" + GenerateSpace(9) + "START_NO." + GenerateSpace(2) + "END_NO.\n\n";
            //Int64 checkTypeCount = 0;
       //     Int64 SN = 0;
            DbConServices con = new DbConServices();
            
            foreach (var check in sort)
            {
              
                if (check.BankName == "Aspac_Rural")
                {
                   
                    if (_ChkType == "PERSONAL")
                    {
                      //  checkTypeCount = check.Quantity;
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;


                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {

                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }


                    if (blockContent == 1)
                    {
                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                        lineCount += 2;
                    }

                    if (blockContent == 5)
                    {
                        blockContent = 2;

                        blockCounter++;

                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                    }
                    else
                    {
                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                        lineCount += 1;

                        blockContent++;
                    }
                }
            }
            foreach (var check in sort)
            {
        
                if (check.BankName == "Imus_Rural_Bank")
                {
                    if (_ChkType == "PERSONAL")
                    {
                      //  checkTypeCount = check.Quantity;
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;


                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {

                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }


                    if (blockContent == 1)
                    {
                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                        lineCount += 2;
                    }

                    if (blockContent == 5)
                    {
                        blockContent = 2;

                        blockCounter++;

                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                    }
                    else
                    {
                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                        lineCount += 1;

                        blockContent++;
                    }
                }
            }
            foreach (var check in sort)
            {

                if (check.BankName == "Masuwerte")
                {
                    if (_ChkType == "PERSONAL")
                    {
                        //  checkTypeCount = check.Quantity;
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;


                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {

                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }


                    if (blockContent == 1)
                    {
                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                        lineCount += 2;
                    }

                    if (blockContent == 5)
                    {
                        blockContent = 2;

                        blockCounter++;

                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                    }
                    else
                    {
                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                        lineCount += 1;

                        blockContent++;
                    }
                }
            }
            foreach (var check in sort)
            {

                if (check.BankName == "Rural_Bank_of_Angeles")
                {
                    if (_ChkType == "PERSONAL")
                    {
                        //  checkTypeCount = check.Quantity;
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;


                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {

                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }


                    if (blockContent == 1)
                    {
                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                        lineCount += 2;
                    }

                    if (blockContent == 5)
                    {
                        blockContent = 2;

                        blockCounter++;

                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                    }
                    else
                    {
                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                        lineCount += 1;

                        blockContent++;
                    }
                }
            }
            foreach (var check in sort)
            {

                if (check.BankName == "Banko_Mabuhay")
                {
                    if (_ChkType == "PERSONAL")
                    {
                        //  checkTypeCount = check.Quantity;
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;


                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {

                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }


                    if (blockContent == 1)
                    {
                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                        lineCount += 2;
                    }

                    if (blockContent == 5)
                    {
                        blockContent = 2;

                        blockCounter++;

                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                    }
                    else
                    {
                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                        lineCount += 1;

                        blockContent++;
                    }
                }
            }
            foreach (var check in sort)
            {

                if (check.BankName == "Rural_Bank_of_Cardona")
                {
                    if (_ChkType == "PERSONAL")
                    {
                        //  checkTypeCount = check.Quantity;
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;


                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {

                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }


                    if (blockContent == 1)
                    {
                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                        lineCount += 2;
                    }

                    if (blockContent == 5)
                    {
                        blockContent = 2;

                        blockCounter++;

                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                    }
                    else
                    {
                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                        lineCount += 1;

                        blockContent++;
                    }
                }
            }
            foreach (var check in sort)
            {

                if (check.BankName == "Rural_Bank_of_Dulag")
                {
                    if (_ChkType == "PERSONAL")
                    {
                        //  checkTypeCount = check.Quantity;
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;


                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {

                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }


                    if (blockContent == 1)
                    {
                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                        lineCount += 2;
                    }

                    if (blockContent == 5)
                    {
                        blockContent = 2;

                        blockCounter++;

                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                    }
                    else
                    {
                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                        lineCount += 1;

                        blockContent++;
                    }
                }
            }
            foreach (var check in sort)
            {

                if (check.BankName == "Rural_Bank_of_Kawit")
                {
                    if (_ChkType == "PERSONAL")
                    {
                        //  checkTypeCount = check.Quantity;
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;


                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {

                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }


                    if (blockContent == 1)
                    {
                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                        lineCount += 2;
                    }

                    if (blockContent == 5)
                    {
                        blockContent = 2;

                        blockCounter++;

                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                    }
                    else
                    {
                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                        lineCount += 1;

                        blockContent++;
                    }
                }
            }
            foreach (var check in sort)
            {

                if (check.BankName == "Rural_Bank_of_Mexico")
                {
                    if (_ChkType == "PERSONAL")
                    {
                        //  checkTypeCount = check.Quantity;
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;


                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {

                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }


                    if (blockContent == 1)
                    {
                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                        lineCount += 2;
                    }

                    if (blockContent == 5)
                    {
                        blockContent = 2;

                        blockCounter++;

                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                    }
                    else
                    {
                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                        lineCount += 1;

                        blockContent++;
                    }
                }
            }
            foreach (var check in sort)
            {

                if (check.BankName == "Rural_Bank_of_Porac")
                {
                    if (_ChkType == "PERSONAL")
                    {
                        //  checkTypeCount = check.Quantity;
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;


                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {

                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }


                    if (blockContent == 1)
                    {
                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                        lineCount += 2;
                    }

                    if (blockContent == 5)
                    {
                        blockContent = 2;

                        blockCounter++;

                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                    }
                    else
                    {
                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                        lineCount += 1;

                        blockContent++;
                    }
                }
            }
            foreach (var check in sort)
            {

                if (check.BankName == "Rural_Bank_of_Salinas")
                {
                    if (_ChkType == "PERSONAL")
                    {
                        //  checkTypeCount = check.Quantity;
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;


                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {

                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }


                    if (blockContent == 1)
                    {
                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                        lineCount += 2;
                    }

                    if (blockContent == 5)
                    {
                        blockContent = 2;

                        blockCounter++;

                        output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                    }
                    else
                    {
                        output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                        GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                        lineCount += 1;

                        blockContent++;
                    }
                }
            }
            //if (lineCount >=61 )
            //{
            if (noFooter) //ADD FOOTER
            {
                output += "\n " + _batchNumber + GenerateSpace(46) + "DLVR: " + _deliveryDate.ToString("MM-dd(ddd)") + "\n\n" +
                    " A = " +GenerateSpace(20) + frmMain._fileName + ".txt\n" +
                    countText +
                    GenerateSpace(4) + "Prepared By" + GenerateSpace(3) + ": " + _preparedBy + "\t\t\t\t RECHECKED BY:\n" +
                    GenerateSpace(4) + "Updated By" + GenerateSpace(4) + ": " + _preparedBy + "\n" +
                    GenerateSpace(4) + "Time Start" + GenerateSpace(4) + ": " + DateTime.Now.ToShortTimeString() + "\n" +
                    GenerateSpace(4) + "Time Finished :\n" +
                    GenerateSpace(4) + "File rcvd" + GenerateSpace(5) + ":\n";

                noFooter = false;
            }

            // output += Seperator();

            lineCount = 1;
            //}

            return output;

        }

        public static string ConvertToPackingListRb(List<OrderModelRb> _checks, string _checkType, frmMain _mainForm)
        {
            var listofbrstn = _checks.Select(e => e.BRSTN).Distinct().ToList();
            int page = 1;
            string date = DateTime.Now.ToShortDateString();
            string output = "";
            int i = 0;

            foreach (string brstn in listofbrstn)
            {

                output += "\n Page No. " + page.ToString() + "\n " +
                                  date + "\n" +
                                  GenerateSpace(29) + "CAPTIVE PRINTING CORPORATION\n" +
                                  GenerateSpace(28) + "AUB - " + _checkType + " Checks Summary\n\n" +
                                  GenerateSpace(2) + "ACCT_NO" + GenerateSpace(9) + "ACCOUNT NAME" + GenerateSpace(21) + "QTY CT START #" + GenerateSpace(4) + "END #\n\n\n";

                var listofchecks = _checks.Where(e => e.BRSTN == brstn).ToList();
                output += " ** ORDERS OF BRSTN " + _checks[i].BRSTN + " " + _checks[i].BranchName + "\n\n" +
                              " * BATCH #: " + _mainForm.batchfile + "\n\n";



                foreach (var check in listofchecks)
                {
                   
                        if (_checkType == "PERSONAL")
                        {
                            while (check.StartingSerial.Length < 7)
                                check.StartingSerial = "0" + check.StartingSerial;

                            while (check.EndingSerial.Length < 7)
                                check.EndingSerial = "0" + check.EndingSerial;
                        }
                        else
                        {
                            while (check.StartingSerial.Length < 10)
                                check.StartingSerial = "0" + check.StartingSerial;

                            while (check.EndingSerial.Length < 10)
                                check.EndingSerial = "0" + check.EndingSerial;
                        }//END OF ADDING ZERO IN SERIES NUMBER

                        output += GenerateSpace(2) + check.AccountNo + GenerateSpace(4);

                        if (check.AccountName.Length < 50)
                            output += check.AccountName + GenerateSpace(50 - check.AccountName.Length);
                        else if (check.AccountName.Length > 50)
                             output += check.AccountName2.Substring(0, 50);

                            output += "  1 " + check.ChkType + GenerateSpace(2) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + " \n";
                        if (check.AccountName2 != "")
                            output += GenerateSpace(18) + check.AccountName2 + "\n";
                    
                }

                output += "\n";
                output += "  * * * Sub Total * * * " + listofchecks.Count + "\n\n";

                page++;
                i++;

            }
            output += "  * * * Grand Total * * * " + _checks.Count + "\n";
            return output;

        }// end of function


        public static string ConvertToPrinterFileRB(List<OrderModelRb> _checkModels)
        {

            //var listofcheck = _checkModel.Select(e => e.BRSTN).OrderBy(e => e).ToList();

            string output = "";
            var sort = (from c in _checkModels
                        orderby c.BRSTN, c.AccountNo
                        ascending
                        select c).ToList();


            foreach (var check in sort)
            {
                Int64 Series = 0;
                if (check.ChkType == "B")
                {
                    Series = Int64.Parse(check.StartingSerial) - 1 + 100;
                }
                else
                {
                    Series = Int64.Parse(check.StartingSerial) - 1 + 50;
                }
                Int64 endSeries = Series - 1;

                string outputStartSeries = check.StartingSerial.ToString();

                string outputEndSeries = endSeries.ToString();

                //   string brstnFormat = "";

                string txtSeries = Series.ToString();

                if (check.ChkType == "A")
                {
                    while (check.StartingSerial.Length < 7)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 7)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                else
                {
                    while (check.StartingSerial.Length < 10)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 10)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                output += "10\n" + //1 (FIXED)
                         check.BRSTN + "\n" + //3  (BRSTN)                                             
                         check.AccountNo + "\n" + //5 (ACCT NUMBER)                                              
                         Series.ToString() + "\n" + //7 (Start Series + PCS per Book)                                                    
                         check.ChkType + "\n" + //9 (FIXED)                                              
                         "\n" + //11 (BLANK)                           
                         check.BRSTN.Substring(0, 5) + "\n" +//14 BRSTN FORMATTED
                         " " + check.BRSTN.Substring(5, 4) + "\n" + //15 BRSTN FORMATTED                                                                    
                         check.AccountNo.Substring(0, 5) + "-" + check.AccountNo.Substring(5, 5) + "-" + check.AccountNo.Substring(10, 1) + "\n" + //17 (ACCT NUMBER)                
                         check.AccountName + "\n" + //19 (NAME 1)           
                         "SN\n" + //21 (FIXED)           
                         "\n" + //23 (BLANK)                
                         check.AccountName2 + "\n" + //25 (NAME 2)                
                         "\n" + //27 (FIXED)          
                         "\n" + //29 (BLANK)          
                         "\n" + //31 (BLANK)           
                         check.BranchName + "\n" + //33 (ADDRESS 1)                
                         check.Address2 + "\n" + //35 (ADDRESS 2)  
                         check.Address3 + "\n" + //37 (ADDRESS 3)                            
                         check.Address4 + "\n" + //39 (ADDRESS 4)
                         check.Address5 + "\n" + //41 (ADDRESS 5)
                         check.Address6 + "\n" +                  
                         "ASIA UNITED BANK\n" +//45 (FIXED)
                         "\n" + //47 (BLANK)//   
                         "\n" + //49 (BLANK)  
                         "\n" + //51 (BLANK)
                         "\n" + //53 (BLANK)                         
                         "\n" + //55 (BLANK)               
                         "\n" + //57 (BLANK)                  
                         "\n" + //59 (BLANK)                                        
                check.StartingSerial + "\n" + //63 (STARTING SERIES)               
                check.EndingSerial + "\n";  //65 (ENDING SERIES)

            }

            return output;
        }//end of function
            public static string  OrderFile(List<OrderModel> _order)
            {
            string output = "";
            foreach (var order in _order)
                {
                output = order.ChkType + order.BRSTN + order.AccountNo + order.AccountName + order.BranchName + order.AccountName2 + order.PcsPerbook;

                
                }
               return output;
            }


        public static string ConvertToBlockTextM(List<ManualOrderModel> _check, string _ChkType, string _batchNumber, DateTime _deliveryDate, string _preparedBy)

        {

            int page = 1, lineCount = 14, blockCounter = 1, blockContent = 1;
            string date = DateTime.Now.ToString("MMM. dd, yyyy");
            bool noFooter = true;
            string countText = "";
            string output = "";

            //Sort Check List
            var sort = (from c in _check
                        orderby c.BRSTN
                        ascending
                        select c).ToList();

            output += "\n" + GenerateSpace(8) + "Page No. " + page.ToString() + "\n" +
            GenerateSpace(8) + date +
            "\n";
            if (_ChkType == "PERSONAL")
            {
                output += GenerateSpace(27) + "SUMMARY OF BLOCK - " + _ChkType.ToUpper() + "\n" +
                  GenerateSpace(30) + "AUB " + frmMain.outputFolder + "\n" +
                  GenerateSpace(21) + "U S E   C A P T I V E   B A S E S T O C K ! ! !" + "\n\n" +
                  GenerateSpace(28) + "Base Stock: 8 Outs (Cutsheet) --> Melinda\n\n" +
                  GenerateSpace(5) + "Starting Batch " + _batchNumber + ", New MICR Alignment of NCDSS is 15-54 ! ! !\n\n" +
                  GenerateSpace(14) + "Hyphen: 5-5-1" + GenerateSpace(5) + "Additional 0 (zero) are in 6th Digit" + "\n\n\n";
            }
            else if (_ChkType == "MANAGER'S CHECK")
            {
                output += GenerateSpace(32) + "Asia United Bank" +
                  GenerateSpace(30) + "Manager's Check\n" +
                  GenerateSpace(28) + "50 Pcs. / Book" + "\n" +
                  GenerateSpace(23) + "A L L  M A N U A L  E N C O D E D\n" +
                  GenerateSpace(8) + "Pls. DISREGARD the Series given on the Hard-copy by AUB\n" +
                  GenerateSpace(17) + "since Series are only maintained by CPC\n" +
                  GenerateSpace(8) + "Pls. Disregard Branch Name on Hard Copy given by AUB\n" +
                  GenerateSpace(9) + "Branch Name depends on the BRSTN on Regular Checks\n\n\n" +
                  GenerateSpace(3) + "Acct No. With hyphen is on Account Name field if no Blank name or no RB Acct No\n\n" +
                  GenerateSpace(5) + "Starting Batch " + _batchNumber + ", New MICR Alignment of NCDSS is 15-54 ! ! !\n\n" +
                  GenerateSpace(14) + "Hyphen: 5-5-1" + GenerateSpace(5) + "Additional 0 (zero) are in 6th Digit" + "\n\n\n";

            }
            output += GenerateSpace(8) + "BLOCK RT_NO" + GenerateSpace(5) + "M ACCT_NO" + GenerateSpace(9) + "START_NO." + GenerateSpace(2) + "END_NO.\n\n";
            Int64 checkTypeCount = 0;
            foreach (var check in sort)
            {


                if (_ChkType == "PERSONAL")
                {
                    checkTypeCount = check.Quantity;
                    while (check.StartingSerial.Length < 7)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 7)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                else
                {

                    while (check.StartingSerial.Length < 10)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 10)
                        check.EndingSerial = "0" + check.EndingSerial;
                }


                if (blockContent == 1)
                {
                    output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                    lineCount += 2;
                }

                if (blockContent == 5)
                {
                    blockContent = 2;

                    blockCounter++;

                    output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                }
                else
                {
                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                    lineCount += 1;

                    blockContent++;
                }
            }
            //if (lineCount >=61 )
            //{
            if (noFooter) //ADD FOOTER
            {
                output += "\n " + _batchNumber + GenerateSpace(46) + "DLVR: " + _deliveryDate.ToString("MM-dd(ddd)") + "\n\n" +
                    " A = " + _check.Count + GenerateSpace(20)  + ".txt\n" +
                    countText +
                    GenerateSpace(4) + "Prepared By" + GenerateSpace(3) + ": " + _preparedBy + "\t\t\t\t RECHECKED BY:\n" +
                    GenerateSpace(4) + "Updated By" + GenerateSpace(4) + ": " + _preparedBy + "\n" +
                    GenerateSpace(4) + "Time Start" + GenerateSpace(4) + ": " + DateTime.Now.ToShortTimeString() + "\n" +
                    GenerateSpace(4) + "Time Finished :\n" +
                    GenerateSpace(4) + "File rcvd" + GenerateSpace(5) + ":\n";

                noFooter = false;
            }

            // output += Seperator();

            lineCount = 1;
            //}

            return output;

        }

        public static string ConvertToPackingListM(List<ManualOrderModel> _checks, string _checkType, Encode _mainForm)
        {
            var listofbrstn = _checks.Select(e => e.BRSTN).Distinct().ToList();
            int page = 1;
            string date = DateTime.Now.ToShortDateString();
            string output = "";
            int i = 0;

            foreach (string brstn in listofbrstn)
            {

                output += "\n Page No. " + page.ToString() + "\n " +
                                  date + "\n" +
                                  GenerateSpace(29) + "CAPTIVE PRINTING CORPORATION\n" +
                                  GenerateSpace(28) + "AUB - " + _checkType + " Checks Summary\n\n" +
                                  GenerateSpace(2) + "ACCT_NO" + GenerateSpace(9) + "ACCOUNT NAME" + GenerateSpace(21) + "QTY CT START #" + GenerateSpace(4) + "END #\n\n\n";

                var listofchecks = _checks.Where(e => e.BRSTN == brstn).ToList();
                output += " ** ORDERS OF BRSTN " + _checks[i].BRSTN + " " + _checks[i].BranchName + "\n\n" +
                              " * BATCH #: " + _mainForm.batchfile + "\n\n";



                foreach (var check in listofchecks)
                {

                    if (_checkType == "PERSONAL")
                    {
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {
                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }//END OF ADDING ZERO IN SERIES NUMBER

                    output += GenerateSpace(2) + check.AccountNo + GenerateSpace(4);

                    if (check.AccountName.Length < 50)
                        output += check.AccountName + GenerateSpace(50 - check.AccountName.Length);
                    else if (check.AccountName.Length > 50)
                        output += check.AccountName2.Substring(0, 50);

                    output += "  1 " + check.ChkType + GenerateSpace(2) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + " \n";
                    if (check.AccountName2 != "")
                        output += GenerateSpace(18) + check.AccountName2 + "\n";
                }

                output += "\n";
                output += "  * * * Sub Total * * * " + listofchecks.Count + "\n";

                page++;
                i++;

            }
            output += "  * * * Grand Total * * * " + _checks.Count + "\n";
            return output;

        }// end of function


        public static string ConvertToPrinterFileM(List<ManualOrderModel> _checkModels)
        {

            //var listofcheck = _checkModel.Select(e => e.BRSTN).OrderBy(e => e).ToList();

            string output = "";
            var sort = (from c in _checkModels
                        orderby c.BRSTN, c.AccountNo
                        ascending
                        select c).ToList();


            foreach (var check in sort)
            {
                Int64 Series = 0;
                if (check.ChkType == "B")
                {
                    Series = Int64.Parse(check.StartingSerial) - 1 + 100;
                }
                else
                {
                    Series = Int64.Parse(check.StartingSerial) - 1 + 50;
                }
                Int64 endSeries = Series - 1;

                string outputStartSeries = check.StartingSerial.ToString();

                string outputEndSeries = endSeries.ToString();

                //   string brstnFormat = "";

                string txtSeries = Series.ToString();

                if (check.ChkType == "A")
                {
                    while (check.StartingSerial.Length < 7)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 7)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                else
                {
                    while (check.StartingSerial.Length < 10)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 10)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                output += "10\n" + //1 (FIXED)
                                   // "10\n" + //2 (FIXED)
                         check.BRSTN + "\n" + //3  (BRSTN)
                                              //check.BRSTN + "\n" + //4  (BRSTN)
                         check.AccountNo + "\n" + //5 (ACCT NUMBER)
                                                  //check.AccountNo + "\n" + //6 (ACCT NUMBER)
                         Series.ToString() + "\n" + //7 (Start Series + PCS per Book)
                                                    //Series.ToString() + "\n" + //8 (Start Series + PCS per Book)
                         check.ChkType + "\n" + //9 (FIXED)
                                                //check.ChkType + "\n" + //10 (FIXED)
                         "\n" + //11 (BLANK)
                                //"\n" + //12 (BLANK)
                                //       check.BRSTN.Substring(0, 5) + "\n"; //13 BRSTN FORMATTED
                         check.BRSTN.Substring(0, 5) + "\n" +//14 BRSTN FORMATTED
                         " " + check.BRSTN.Substring(5, 4) + "\n" + //15 BRSTN FORMATTED
                                                                    //" " + check.BRSTN.Substring(5, 4) + "\n" + //16 BRSTN FORMATTED
                         check.AccountNo.Substring(0, 5) + "-" + check.AccountNo.Substring(5, 5) + "-" + check.AccountNo.Substring(10, 1) + "\n" + //17 (ACCT NUMBER)
                //check.AccountNo.Substring(0, 5) + "-" + check.AccountNo.Substring(5, 5) + "-" + check.AccountNo.Substring(10, 1) + "\n" + //18 (ACCT NUMBER)
                         check.AccountName + "\n" + //19 (NAME 1)
                //check.Name1 + "\n" + //20 (NAME 1)
                         "SN\n" + //21 (FIXED)
                //"SN\n" + //22 (FIXED)
                         "\n" + //23 (BLANK) 
                //"\n" + //24 (BLANK) 
                         check.AccountName2 + "\n" + //25 (NAME 2)
                //check.Name2 + "\n" + //26 (NAME 2)
                         "\n" + //27 (FIXED)
                //"\n" + //28 (FIXED)
                         "\n" + //29 (BLANK)
                //"\n" +//30 (BLANK)
                         "\n" + //31 (BLANK)
                //"\n" +//32(BLANK)
                         check.BranchName + "\n" + //33 (ADDRESS 1)
                //check.Address1 + "\n" + //34 (ADDRESS 1)
                         check.Address2 + "\n" + //35 (ADDRESS 2)
                //check.Address2 + "\n" + //36 (ADDRESS 2)
                         check.Address3 + "\n" + //37 (ADDRESS 3)
                                                 // check.Address3 + "\n" + //38 (ADDRESS 3)
                         check.Address4 + "\n" + //39 (ADDRESS 4)
                //check.Address4 + "\n" + //40 (ADDRESS 4)
                         check.Address5 + "\n" + //41 (ADDRESS 5)
                                                 //check.Address5 + "\n" + //42 (ADDRESS 5)
                         "\n" +//43 (BLANK)
                //"\n" +//44 (BLANK)
                         "ISLA BANK\n" +//45 (FIXED)
                //"ISLA BANK\n" +//46 (FIXED)
                         "\n" + //47 (BLANK)//
                                // "\n" + //48 (BLANK)
                         "\n" + //49 (BLANK)
                //"\n" + //50 (BLANK)
                         "\n" + //51 (BLANK)
                // "\n" + //52 (BLANK)
                         "\n" + //53 (BLANK)
                                // "\n" + //54 (BLANK)
                         "\n" + //55 (BLANK)
                //"\n" + //56 (BLANK)
                         "\n" + //57 (BLANK)
                                // "\n" + //58 (BLANK)
                         "\n" + //59 (BLANK)
                                // "\n" + //60 (BLANK)
                         "\n" + //61 (BLANK)
                //"\n" + //62 (BLANK)
                check.StartingSerial + "\n" + //63 (STARTING SERIES)
                //check.StartSeries + "\n" + //64 (STARTING SERIES)
                check.EndingSerial + "\n";  //65 (ENDING SERIES)
                //check.EndSeries + "\n"; //66 (ENDING SERIES)     
                //if(sort.Count % 4 == 0)
                //              output +=     "\\" + "\n";
            }

            return output;
        }


        public static string DoBlockHeader(string _chkTpye, string _batch ,string _output)
        {
            if (_chkTpye == "PERSONAL")
            {
                _output += GenerateSpace(27) + "SUMMARY OF BLOCK - " + _chkTpye.ToUpper() + "\n" +
                  GenerateSpace(30) + "AUB " +frmMain.outputFolder  + "\n" +
                  GenerateSpace(21) + "U S E   C A P T I V E   B A S E S T O C K ! ! !" + "\n\n" +
                  GenerateSpace(28) + "Base Stock: 8 Outs (Cutsheet) --> Melinda\n\n" +
                  GenerateSpace(5) + "Starting Batch " + _batch + ", New MICR Alignment of NCDSS is 15-54 ! ! !\n\n" +
                  GenerateSpace(14) + "Hyphen: 5-5-1" + GenerateSpace(5) + "Additional 0 (zero) are in 6th Digit" + "\n\n\n";
            }
           else  if (_chkTpye == "MANAGER'S CHECK")
            {
                _output += GenerateSpace(27) + "SUMMARY OF BLOCK - " + _chkTpye.ToUpper() + "\n" +
                  GenerateSpace(30) + "AUB "+ Encode.outputfolder + "\n" +

                  GenerateSpace(28) + "50 Pcs. / Book" + "\n" +
                  GenerateSpace(23) + "A L L  M A N U A L  E N C O D E D\n" +
                  GenerateSpace(8) + "Pls. DISREGARD the Series given on the Hard-copy by AUB\n" +
                  GenerateSpace(17) + "since Series are only maintained by CPC\n" +
                  GenerateSpace(8) + "Pls. Disregard Branch Name on Hard Copy given by AUB\n" +
                  GenerateSpace(9) + "Branch Name depends on the BRSTN on Regular Checks\n\n\n" +
                  GenerateSpace(3) + "Acct No. With hyphen is on Account Name field if no Blank name or no RB Acct No\n\n" +
                  GenerateSpace(5) + "Starting Batch " + _batch + ", New MICR Alignment of NCDSS is 15-54 ! ! !\n\n" +
                  GenerateSpace(14) + "Hyphen: 5-5-1" + GenerateSpace(5) + "Additional 0 (zero) are in 6th Digit" + "\n\n\n";
                return _output;
            }
            return _output;
        }
    }
   
}
