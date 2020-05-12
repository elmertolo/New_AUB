using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using New_AUB.Models;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;

namespace New_AUB.Services
{
    class ProcessServices
    {
        DbConServices con = new DbConServices();
        static List<BranchModel> branch = new List<BranchModel>();
        StreamWriter file;
        string outputFolder = Application.StartupPath + "\\Output";

        public static string CheckInBranches(string _BRSTN, string _fileName)
        {
            try
            {
                var ifExist = branch.FirstOrDefault(r => r.BRSTN == _BRSTN);
               
                if (ifExist == null )
                {
                    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\ErrorMessage.txt");
                    sw.WriteLine("BRSTN - " + _BRSTN + " does not exist in Branches Database under file name <" + _fileName + ">.");
                    sw.Close();
                    return "Errors Found!";
                }
               
                return "";
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }
        public List<OrderModel> ProcessCheck(List<OrderModel> _checks)
        {
            var listofChecks = _checks.Select(a => a.BRSTN).ToList();

            return _checks;
        }
        public void DoBlockProcess(List<OrderModel> _checks, frmMain _mainForm)
        {
           
            var listofcheck = _checks.Select(r => r.ChkType).ToList();
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "A")
                {

                    string packkingListPath = outputFolder + "\\Regular Checks\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {

                        string output = OutputServices.ConvertToBlockText(checks, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);

                        file.WriteLine(output);
                    }

                }
            }
        }
        public void PackingText(List<OrderModel> _checksModel, frmMain _mainForm)
        {

            StreamWriter file;
            DbConServices db = new DbConServices();
          //  db.GetAllData(_checksModel, _mainForm._batchfile);
            var listofcheck = _checksModel.Select(e => e.ChkType).ToList();

            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "A")
                {

                    string packkingListPath = outputFolder + "\\Regular Checks\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingList(checks, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "B")
                {

                    string packkingListPath = outputFolder + "\\Regular Checks\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingList(checks, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
        }
        public void SaveToPackingDBF(List<OrderModel> _checks, string _batchNumber, frmMain _mainForm)
        {
            string dbConnection;
            string tempCheckType = "";
            int blockNo = 0, blockCounter = 0;
            DbConServices db = new DbConServices();
         //   db.GetAllData(_checks, _mainForm._batchfile);

            var listofchecks = _checks.Select(e => e.ChkType).Distinct().ToList();

            foreach (string checktype in listofchecks)
            {

                if (checktype == "A" || checktype == "B")
                {
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\Regular Checks\\Packing.dbf" + "; Mode=ReadWrite;";

                    //Check if packing file exists
                    //if (!File.Exists(_filepath))
                    //{
                    OleDbConnection oConnect = new OleDbConnection(dbConnection);
                    OleDbCommand oCommand;
                    oConnect.Open();
                    oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                    oCommand.ExecuteNonQuery();
                    foreach (var check in _checks)
                    {
                        if (tempCheckType != check.ChkType)
                            blockNo = 1;

                        tempCheckType = check.ChkType;

                        if (blockCounter < 4)
                            blockCounter++;
                        else
                        {
                            blockCounter = 1;
                            blockNo++;
                        }

                        string sql = "INSERT INTO PACKING (BATCHNO,BLOCK, RT_NO,BRANCH, ACCT_NO, ACCT_NO_P, CHKTYPE, ACCT_NAME1,ACCT_NAME2," +
                         "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('"+_batchNumber+"',"+ blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                         "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'","''") + "',1,'" +
                        check.StartingSerial + "','" + check.EndingSerial + "','R','"+check.Company+"')";

                        oCommand = new OleDbCommand(sql, oConnect);

                        oCommand.ExecuteNonQuery();
                    }
                    oConnect.Close();
                }
            }
        }
        public void PrinterFile(List<OrderModel> _checkModel, frmMain _mainForm)
        {

           // DbConServices db = new DbConServices();
           // db.GetAllData(_checkModel, _mainForm._batchfile);
            StreamWriter file;

            var listofchecks = _checkModel.Select(e => e.ChkType).Distinct().ToList();

            foreach (string checktype in listofchecks)
            {
                if (checktype == "A")
                {
                    string printerFilePathA = Application.StartupPath + "\\Output\\Regular Checks\\AUB" + /*_mainForm.batchfile.Substring(0, 4)*/  "P.txt";
                    var check = _checkModel.Where(e => e.ChkType == checktype).ToList();
                    if (File.Exists(printerFilePathA))
                        File.Delete(printerFilePathA);

                    file = File.CreateText(printerFilePathA);
                    file.Close();

                    //for (int a = 0; a < check.Count; a++)
                    //{


                        using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                        {
                            string output = OutputServices.ConvertToPrinterFile(check);

                            file.WriteLine(output);
                        }
                    //}
                  //  ZipFileServices.CopyPrinterFile(checktype, _mainForm);
                   // ZipFileServices.CopyPackingDBF(checktype, _mainForm);
                }

            }
            foreach (string checktype in listofchecks)
            {
                if (checktype == "B")
                {
                    string printerFilePath = Application.StartupPath + "\\Output\\Regular Checks\\AUB" /*+ _mainForm.batchfile.Substring(0, 4)*/ + "C.txt";
                    var check = _checkModel.Where(e => e.ChkType == checktype).ToList();
                    if (File.Exists(printerFilePath))
                        File.Delete(printerFilePath);

                    file = File.CreateText(printerFilePath);
                    file.Close();
                    //for (int a = 0; a < check.Count; a++)
                    //{


                        using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                        {
                        string output = OutputServices.ConvertToPrinterFile(check);

                            file.WriteLine(output);
                        }
                    //}
                   // ZipFileServices.CopyPrinterFile(checktype, _mainForm);
                    //ZipFileServices.CopyPackingDBF(checktype, _mainForm);
                }
            }
        }

    }
}
