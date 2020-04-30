using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static string ConvertToBlockText(List<OrderModel> _check, string _ChkType, string _batchNumber, DateTime _deliveryDate, string _preparedBy, string _fileName)

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
            GenerateSpace(27) + "ISLA - SUMMARY OF BLOCK - " + _ChkType.ToUpper() + "\n" +
            GenerateSpace(30) + "(There should be an A prefix " + "\n" +
            GenerateSpace(28) + "printed before the CHECK NUMEBRS)" + "\n\n" +
            GenerateSpace(21) + "A L L  M A N U A L  E N C O D E D ! ! !" + "\n\n" +
            GenerateSpace(5) + "Starting Batch " + _batchNumber + ", New MICR Alignment of NCDSS is 15-54 ! ! !\n\n" +
            GenerateSpace(14) + "Hyphen: 5-5-1" + GenerateSpace(5) + "Additional 0 (zero) are in 6th Digit" + "\n\n\n" +
            GenerateSpace(8) + "BLOCK RT_NO" + GenerateSpace(5) + "M ACCT_NO" + GenerateSpace(9) + "START_NO." + GenerateSpace(2) + "END_NO.\n\n";
            int checkTypeCount = 0;
            foreach (var check in sort)
            {


                if (_ChkType == "PERSONAL")
                {
                    checkTypeCount = check.Qty;
                    while (check.StartSeries.Length < 7)
                        check.StartSeries = "0" + check.StartSeries;

                    while (check.EndSeries.Length < 7)
                        check.EndSeries = "0" + check.EndSeries;
                }
                else
                {

                    while (check.StartSeries.Length < 10)
                        check.StartSeries = "0" + check.StartSeries;

                    while (check.EndSeries.Length < 10)
                        check.EndSeries = "0" + check.EndSeries;
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
                    GenerateSpace(4) + check.StartSeries + GenerateSpace(4) + check.EndSeries + "\n";
                }
                else
                {
                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartSeries + GenerateSpace(4) + check.EndSeries + "\n";

                    lineCount += 1;

                    blockContent++;
                }
            }
            //if (lineCount >=61 )
            //{
            if (noFooter) //ADD FOOTER
            {
                output += "\n " + _batchNumber + GenerateSpace(46) + "DLVR: " + _deliveryDate.ToString("MM-dd(ddd)") + "\n\n" +
                    " A = " + checkTypeCount + GenerateSpace(20) + _fileName + ".txt\n" +
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

    }
}
