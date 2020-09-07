using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using New_AUB.Models;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.AccessControl;
//using Microsoft.Office.Interop.Access;

namespace New_AUB.Services
{
    class ProcessServices
    {
        DbConServices con = new DbConServices();
        static List<BranchModel> branch = new List<BranchModel>();
        StreamWriter file;
        string outputFolder = Application.StartupPath + "\\Output";
     //   string folderName = "";
        public static string ErrorMessage(string _errorMessage)
        {
            try
            {
            //    var ifExist = branch.FirstOrDefault(r => r.BRSTN == _BRSTN);
               
                //if (ifExist == null )
                //{
                    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\ErrorMessage.txt");
                    sw.WriteLine(_errorMessage);
                    sw.Close();
                    return _errorMessage;
            //    }
               
             //   return "";
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
        public void DoBlockProcess(List<OrderModel> _checks, frmMain _mainForm, string _outpuFolder)
        {
           
            var listofcheck = _checks.Select(r => r.ChkType).ToList();
            foreach (string Scheck in listofcheck)
            {
               
                if (Scheck == "A")
                {
                    if (_outpuFolder.Contains("Starter"))
                    {
                        string packkingListPath = outputFolder + "\\" + _outpuFolder + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {

                            string output = OutputServices.ConvertToBlockText(checks, "Starter Checks", "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);

                            file.WriteLine(output);
                        }
                    }
                    else if (_outpuFolder.Contains("Regular"))
                    {
                        string packkingListPath = outputFolder + "\\" + _outpuFolder + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();
                     
                        
                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {

                            string output = OutputServices.ConvertToBlockText(checks, "Regular Checks", "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);

                            file.WriteLine(output);
                        }
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "B")
                {
                    if (_outpuFolder.Contains("Regular"))
                    {
                        string packkingListPath = outputFolder + "\\" + _outpuFolder + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {

                            string output = OutputServices.ConvertToBlockText(checks, "Regular Checks", "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);

                            file.WriteLine(output);
                        }
                    }
                    else if (_outpuFolder.Contains("Starter"))
                    {
                        string packkingListPath = outputFolder + "\\" + _outpuFolder + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {

                            string output = OutputServices.ConvertToBlockText(checks, "Starter Checks", "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);

                            file.WriteLine(output);
                        }
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {

                if (Scheck == "CON")
                {

                    string packkingListPath = outputFolder + "\\" + _outpuFolder + "\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {

                        string output = OutputServices.ConvertToBlockText(checks, "Continues Check", "Continues Check", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);

                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {

                if (Scheck == "CV")
                {

                    string packkingListPath = outputFolder + "\\" + _outpuFolder + "\\BlockC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {

                        string output = OutputServices.ConvertToBlockText(checks, "Continues Check w/ Voucher", "Continues Check w/ Voucher", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);

                        file.WriteLine(output);
                    }

                }
            }
          
        }
        public void PackingText(List<OrderModel> _checksModel, frmMain _mainForm,string _outputFolder)
        {

            StreamWriter file;
            DbConServices db = new DbConServices();
          //  db.GetAllData(_checksModel, _mainForm._batchfile);
            var listofcheck = _checksModel.Select(e => e.ChkType).ToList();

            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "A")
                {

                    string packkingListPath = outputFolder + "\\"+ _outputFolder + "\\PackingP.txt";
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

                    string packkingListPath = outputFolder + "\\" + _outputFolder + "\\PackingC.txt";
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
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "CON")
                {

                    string packkingListPath = outputFolder + "\\" + _outputFolder + "\\PackingA.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingList(checks, "Continues Check", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "CV")
                {

                    string packkingListPath = outputFolder + "\\" + _outputFolder + "\\PackingB.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingList(checks, "Continues Check w/ Voucher", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
        }
        public void SaveToPackingDBF(List<OrderModel> _checks, string _batchNumber, frmMain _mainForm,string _outputFolder)
        {
            string dbConnection;
            string tempCheckType = "";
            int blockNo = 0, blockCounter = 0;
            DbConServices db = new DbConServices();
         //   db.GetAllData(_checks, _mainForm._batchfile);

            var listofchecks = _checks.Select(e => e.ChkType).Distinct().ToList();
            //for (int i = 0; i < listofchecks.Count; i++)
            //{
            //    if(_checks[i].BRSTN == null)
            //    {
            //        i++;
            //    }
            //    else
            //    {


            foreach (string checktype in listofchecks)
            {

                if (checktype == "A" || checktype == "B")
                {
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\"+ _outputFolder+"\\Packing.dbf" + "; Mode=ReadWrite;";

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
                        if (check.BRSTN == null)
                        { }
                        else
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
                         "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                         "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                        check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                        oCommand = new OleDbCommand(sql, oConnect);

                        oCommand.ExecuteNonQuery();
                        }
                            }
                            oConnect.Close();
                       // }
                   // }
                }
            }
            foreach (string checktype in listofchecks)
            {

                if (checktype == "CON")
                {
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _outputFolder + "\\Packing.dbf" + "; Mode=ReadWrite;";

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
                        if (check.BRSTN == null)
                        { }
                        else
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
                             "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                             "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                            check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                            oCommand = new OleDbCommand(sql, oConnect);

                            oCommand.ExecuteNonQuery();
                        }
                    }
                    oConnect.Close();
                    // }
                    // }
                }
            }
            foreach (string checktype in listofchecks)
            {

                if (checktype == "CV")
                {
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _outputFolder + "\\Packing.dbf" + "; Mode=ReadWrite;";

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
                        if (check.BRSTN == null)
                        { }
                        else
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
                             "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                             "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                            check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                            oCommand = new OleDbCommand(sql, oConnect);

                            oCommand.ExecuteNonQuery();
                        }
                    }
                    oConnect.Close();
                    // }
                    // }
                }
            }
        }
        public void PrinterFile(List<OrderModel> _checkModel, frmMain _mainForm,string _outputFolder)
        {

           // DbConServices db = new DbConServices();
           // db.GetAllData(_checkModel, _mainForm._batchfile);
            StreamWriter file;

            var listofchecks = _checkModel.Select(e => e.ChkType).Distinct().ToList();

            foreach (string checktype in listofchecks)
            {
                if (checktype == "A")
                {
                    string printerFilePathA = Application.StartupPath + "\\Output\\"+ _outputFolder+"\\AUB" + /*_mainForm.batchfile.Substring(0, 4)*/  "P.txt";
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
                    ZipfileServices.CopyPrinterFile(checktype, _mainForm,check[0].FileName);
                   // ZipFileServices.CopyPackingDBF(checktype, _mainForm);
                }

            }
            foreach (string checktype in listofchecks)
            {
                if (checktype == "B")
                {
                    string printerFilePath = Application.StartupPath + "\\Output\\"+ _outputFolder+"\\AUB" /*+ _mainForm.batchfile.Substring(0, 4)*/ + "C.txt";
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
            foreach (string checktype in listofchecks)
            {
                if (checktype == "CON")
                {
                    string printerFilePath = Application.StartupPath + "\\Output\\" + _outputFolder + "\\AUB" /*+ _mainForm.batchfile.Substring(0, 4)*/ + "C.txt";
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
            foreach (string checktype in listofchecks)
            {
                if (checktype == "CV")
                {
                    string printerFilePath = Application.StartupPath + "\\Output\\" + _outputFolder + "\\AUB" /*+ _mainForm.batchfile.Substring(0, 4)*/ + "C.txt";
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
        public List<OrderModelRb> Process(List<OrderModelRb> _orders, frmMain _main)
        {

            TypeofCheckModel checkType = new TypeofCheckModel();

            checkType.Angeles_Personal = new List<OrderModelRb>();
            checkType.Angeles_Commercial = new List<OrderModelRb>();
            checkType.Aspac_Personal = new List<OrderModelRb>();
            checkType.Aspac_Commercial = new List<OrderModelRb>();
            checkType.Bank_Mabuhay_Personal = new List<OrderModelRb>();
            checkType.Bank_Mabuhay_Commercial = new List<OrderModelRb>();
            checkType.Cardona_Personal = new List<OrderModelRb>();
            checkType.Cardona_Commercial = new List<OrderModelRb>();
            checkType.Dulag_Personal = new List<OrderModelRb>();
            checkType.Dulag_Commercial = new List<OrderModelRb>();
            checkType.Entreprenuer_Personal = new List<OrderModelRb>();
            checkType.Entreprenuer_Commercial = new List<OrderModelRb>();
            checkType.Fair_Personal = new List<OrderModelRb>();
            checkType.Fair_Commercial = new List<OrderModelRb>();
            checkType.Imus_Binan_Commercial = new List<OrderModelRb>();
            checkType.Imus_Binan_Personal = new List<OrderModelRb>();
            checkType.Kawit_Commercial = new List<OrderModelRb>();
            checkType.Kawit_Personal = new List<OrderModelRb>();
            checkType.Masuwerte_Commercial = new List<OrderModelRb>();
            checkType.Masuwerte_Personal = new List<OrderModelRb>();
            checkType.Mexico_Commercial = new List<OrderModelRb>();
            checkType.Mexico_Personal = new List<OrderModelRb>();
            checkType.Porac_Commercial = new List<OrderModelRb>();
            checkType.Porac_Personal = new List<OrderModelRb>();
            checkType.Progressive_Commercial = new List<OrderModelRb>();
            checkType.Progressive_Personal = new List<OrderModelRb>();
            checkType.Salinas_Commercial = new List<OrderModelRb>();
            checkType.Salinas_Personal = new List<OrderModelRb>();
            ZipfileServices zip = new ZipfileServices();

            foreach (OrderModelRb _check in _orders)
            {
                if(_check.BankName == "Aspac_Rural" && _check.ChkType == "A")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Aspac_Personal.Add(_check);

                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Aspac_Rural" && _check.ChkType == "B")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Aspac_Commercial.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if(_check.BankName == "Banko_Mabuhay" && _check.ChkType == "A")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Bank_Mabuhay_Personal.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Banko_Mabuhay" && _check.ChkType == "B")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Bank_Mabuhay_Commercial.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Imus_Rural_Bank" && _check.ChkType == "A")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Imus_Binan_Personal.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Imus_Rural_Bank" && _check.ChkType == "B")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Imus_Binan_Commercial.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Masuwerte" && _check.ChkType == "A")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Masuwerte_Personal.Add(_check);

                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                
                if (_check.BankName == "Masuwerte" && _check.ChkType == "B")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Masuwerte_Commercial.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Rural_Bank_of_Angeles" && _check.ChkType == "A")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Angeles_Personal.Add(_check);

                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }

                if (_check.BankName == "Rural_Bank_of_Angeles" && _check.ChkType == "B")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Angeles_Commercial.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Rural_Bank_of_Cardona" && _check.ChkType == "A")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Cardona_Personal.Add(_check);

                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }

                if (_check.BankName == "Rural_Bank_of_Cardona" && _check.ChkType == "B")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Cardona_Commercial.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Rural_Bank_of_Dulag" && _check.ChkType == "A")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Dulag_Personal.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }

                if (_check.BankName == "Rural_Bank_of_Dulag" && _check.ChkType == "B")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Dulag_Commercial.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Rural_Bank_of_Kawit" && _check.ChkType == "A")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Kawit_Personal.Add(_check);

                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }

                if (_check.BankName == "Rural_Bank_of_Kawit" && _check.ChkType == "B")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Kawit_Commercial.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Rural_Bank_of_Mexico" && _check.ChkType == "A")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Mexico_Personal.Add(_check);

                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }

                if (_check.BankName == "Rural_Bank_of_Mexico" && _check.ChkType == "B")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Mexico_Commercial.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Rural_Bank_of_Porac" && _check.ChkType == "A")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Porac_Personal.Add(_check);

                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }

                if (_check.BankName == "Rural_Bank_of_Porac" && _check.ChkType == "B")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Porac_Commercial.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                if (_check.BankName == "Rural_Bank_of_Salinas" && _check.ChkType == "A")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Porac_Personal.Add(_check);

                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }

                if (_check.BankName == "Rural_Bank_of_Salinas" && _check.ChkType == "B")
                {
                    zip.DeleteFiles(".txt", Application.StartupPath + "\\Output\\" + _check.BankName);
                    checkType.Porac_Commercial.Add(_check);
                    DoBlockProcessRB(checkType, _main);
                    PackingTextRB(checkType, _main);
                    SaveToPackingDBFRb(checkType, _main.batchfile, _main);
                    PrinterFileRb(checkType, _main);
                }
                //  DoBlockProcessRB(checkType,_main);
            }
            return _orders;
          
        }
        public void DoBlockProcessRB(TypeofCheckModel _ordersRB, frmMain _mainForm)
        {
            DbConServices db = new DbConServices();
            BranchModelRb rb = new BranchModelRb();
            Int64 SN = 0;
            Int64 EN = 0;
            if (_ordersRB.Aspac_Personal.Count > 0)
            {

                for (int f = 0; f < _ordersRB.Aspac_Personal.Count; f++)
                {
                    db.GetBranchByBRSTNRb(rb, _ordersRB.Aspac_Personal[f].BRSTN);
                    SN = rb.LastNo + 1;
                    for (int a = 0; a <= f; a++)
                    {

                        _ordersRB.Aspac_Personal[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Aspac_Personal[a].PcsPerbook) - 1;
                        _ordersRB.Aspac_Personal[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                }
                for (int i = 0; i < _ordersRB.Aspac_Personal.Count; i++)
                {
                  
                 
                  
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Aspac_Personal[i].BankName + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();
                       
                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Aspac_Personal, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    
                }
              
            }
            if (_ordersRB.Aspac_Commercial.Count > 0)
            {
                //for (int f = 0; f < _ordersRB.Aspac_Commercial.Count; f++)
                //{
                  //  db.GetBranchByBRSTNRb(rb, _ordersRB.Aspac_Commercial[f].BRSTN);
                  //  SN = rb.LastNo + 1;
                    for (int a = 0; a < _ordersRB.Aspac_Commercial.Count; a++)
                    {

                        _ordersRB.Aspac_Commercial[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Aspac_Commercial[a].PcsPerbook) - 1;
                        _ordersRB.Aspac_Commercial[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
               // }
                for (int i = 0; i < _ordersRB.Aspac_Commercial.Count; i++)
                {

                 
                   
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Aspac_Commercial[i].BankName + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();
                       
                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Aspac_Commercial, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    
                }

            }
            if (_ordersRB.Imus_Binan_Personal.Count > 0)
            {
                for (int f = 0; f < _ordersRB.Imus_Binan_Personal.Count; f++)
                {
                    db.GetBranchByBRSTNRb(rb, _ordersRB.Imus_Binan_Personal[f].BRSTN);
                    SN = rb.LastNo + 1;
                    for (int a = 0; a < _ordersRB.Imus_Binan_Personal.Count; a++)
                    {

                        _ordersRB.Imus_Binan_Personal[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Imus_Binan_Personal[a].PcsPerbook) - 1;
                        _ordersRB.Imus_Binan_Personal[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }

                }
                for (int i = 0; i < _ordersRB.Imus_Binan_Personal.Count; i++)
                {
                  

                    string packkingListPath = outputFolder + "\\" + _ordersRB.Imus_Binan_Personal[i].BankName + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        
                        file = File.CreateText(packkingListPath);
                        file.Close();
                       
                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{
                           
                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Imus_Binan_Personal, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    
                    
                }
            }
            if (_ordersRB.Imus_Binan_Commercial.Count > 0)
            {
                
                    for (int a = 0; a < _ordersRB.Imus_Binan_Commercial.Count; a++)
                    {

                        _ordersRB.Imus_Binan_Commercial[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Imus_Binan_Commercial[a].PcsPerbook) - 1;
                        _ordersRB.Imus_Binan_Commercial[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
             
                for (int i = 0; i < _ordersRB.Imus_Binan_Commercial.Count; i++)
                {
                 
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Imus_Binan_Commercial[i].BankName + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();
                       
                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Imus_Binan_Commercial, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Angeles_Personal.Count > 0)
            {
                for (int f = 0; f < _ordersRB.Angeles_Personal.Count; f++)
                {
                    db.GetBranchByBRSTNRb(rb, _ordersRB.Angeles_Personal[f].BRSTN);
                    SN = rb.LastNo + 1;
                    for (int a = 0; a <= f; a++)
                    {

                        _ordersRB.Angeles_Personal[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Angeles_Personal[a].PcsPerbook) - 1;
                        _ordersRB.Angeles_Personal[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                }

                for (int i = 0; i < _ordersRB.Angeles_Personal.Count; i++)
                {


                    
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Angeles_Personal[i].BankName + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Angeles_Personal, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Angeles_Commercial.Count > 0)
            {
                
                  //  db.GetBranchByBRSTNRb(rb, _ordersRB.Angeles_Commercial[f].BRSTN);
                  //  SN = rb.LastNo + 1;
                    for (int a = 0; a < _ordersRB.Angeles_Commercial.Count; a++)
                    {

                        _ordersRB.Angeles_Commercial[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Angeles_Commercial[a].PcsPerbook) - 1;
                        _ordersRB.Angeles_Commercial[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                

                for (int i = 0; i < _ordersRB.Angeles_Commercial.Count; i++)
                {


                    
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Angeles_Commercial[i].BankName + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Angeles_Commercial, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Bank_Mabuhay_Personal.Count > 0)
            {

                for (int f = 0; f < _ordersRB.Bank_Mabuhay_Personal.Count; f++)
                {
                    db.GetBranchByBRSTNRb(rb, _ordersRB.Bank_Mabuhay_Personal[f].BRSTN);
                    SN = rb.LastNo + 1;
                    for (int a = 0; a <= f; a++)
                    {

                        _ordersRB.Bank_Mabuhay_Personal[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Bank_Mabuhay_Personal[a].PcsPerbook) - 1;
                        _ordersRB.Bank_Mabuhay_Personal[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                }
                for (int i = 0; i < _ordersRB.Bank_Mabuhay_Personal.Count; i++)
                {


                   
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Bank_Mabuhay_Personal[i].BankName + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Bank_Mabuhay_Personal, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Bank_Mabuhay_Commercial.Count > 0)
            {
                for (int a = 0; a < _ordersRB.Bank_Mabuhay_Commercial.Count; a++)
                {

                    _ordersRB.Bank_Mabuhay_Commercial[a].StartingSerial = SN.ToString();
                    EN = SN + Int64.Parse(_ordersRB.Bank_Mabuhay_Commercial[a].PcsPerbook) - 1;
                    _ordersRB.Bank_Mabuhay_Commercial[a].EndingSerial = EN.ToString();
                    SN = EN + 1;
                }
                for (int i = 0; i < _ordersRB.Bank_Mabuhay_Commercial.Count; i++)
                {


                   
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Bank_Mabuhay_Commercial[i].BankName + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Bank_Mabuhay_Commercial, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Cardona_Personal.Count > 0)
            {
                for (int f = 0; f < _ordersRB.Cardona_Personal.Count; f++)
                {
                    db.GetBranchByBRSTNRb(rb, _ordersRB.Cardona_Personal[f].BRSTN);
                    SN = rb.LastNo + 1;
                    for (int a = 0; a <= f; a++)
                    {

                        _ordersRB.Cardona_Personal[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Cardona_Personal[a].PcsPerbook) - 1;
                        _ordersRB.Cardona_Personal[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                }
                for (int i = 0; i < _ordersRB.Cardona_Personal.Count; i++)
                {


                  
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Cardona_Personal[i].BankName + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Cardona_Personal, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Cardona_Commercial.Count > 0)
            {
                for (int a = 0; a < _ordersRB.Cardona_Commercial.Count; a++)
                {

                    _ordersRB.Cardona_Commercial[a].StartingSerial = SN.ToString();
                    EN = SN + Int64.Parse(_ordersRB.Cardona_Commercial[a].PcsPerbook) - 1;
                    _ordersRB.Cardona_Commercial[a].EndingSerial = EN.ToString();
                    SN = EN + 1;
                }
                for (int i = 0; i < _ordersRB.Cardona_Commercial.Count; i++)
                {


                   
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Cardona_Commercial[i].BankName + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Cardona_Commercial, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Dulag_Personal.Count > 0)
            {
                for (int f = 0; f < _ordersRB.Dulag_Personal.Count; f++)
                {
                    db.GetBranchByBRSTNRb(rb, _ordersRB.Dulag_Personal[f].BRSTN);
                    SN = rb.LastNo + 1;
                    for (int a = 0; a <= f; a++)
                    {

                        _ordersRB.Dulag_Personal[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Dulag_Personal[a].PcsPerbook) - 1;
                        _ordersRB.Dulag_Personal[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                }
                for (int i = 0; i < _ordersRB.Dulag_Personal.Count; i++)
                {


                  
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Dulag_Personal[i].BankName + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Dulag_Personal, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Dulag_Commercial.Count > 0)
            {
                for (int a = 0; a < _ordersRB.Dulag_Commercial.Count; a++)
                {

                    _ordersRB.Dulag_Commercial[a].StartingSerial = SN.ToString();
                    EN = SN + Int64.Parse(_ordersRB.Dulag_Commercial[a].PcsPerbook) - 1;
                    _ordersRB.Dulag_Commercial[a].EndingSerial = EN.ToString();
                    SN = EN + 1;
                }
                for (int i = 0; i < _ordersRB.Dulag_Commercial.Count; i++)
                {


                   
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Dulag_Commercial[i].BankName + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Dulag_Commercial, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Entreprenuer_Personal.Count > 0)
            {
                for (int f = 0; f < _ordersRB.Entreprenuer_Personal.Count; f++)
                {
                    db.GetBranchByBRSTNRb(rb, _ordersRB.Entreprenuer_Personal[f].BRSTN);
                    SN = rb.LastNo + 1;
                    for (int a = 0; a <= f; a++)
                    {

                        _ordersRB.Entreprenuer_Personal[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Entreprenuer_Personal[a].PcsPerbook) - 1;
                        _ordersRB.Entreprenuer_Personal[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                }
                for (int i = 0; i < _ordersRB.Entreprenuer_Personal.Count; i++)
                {


                        string packkingListPath = outputFolder + "\\" + _ordersRB.Entreprenuer_Personal[i].BankName + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Entreprenuer_Personal, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Entreprenuer_Commercial.Count > 0)
            {
                for (int a = 0; a < _ordersRB.Entreprenuer_Commercial.Count; a++)
                {

                    _ordersRB.Entreprenuer_Personal[a].StartingSerial = SN.ToString();
                    EN = SN + Int64.Parse(_ordersRB.Entreprenuer_Personal[a].PcsPerbook) - 1;
                    _ordersRB.Entreprenuer_Personal[a].EndingSerial = EN.ToString();
                    SN = EN + 1;
                }
                for (int i = 0; i < _ordersRB.Entreprenuer_Commercial.Count; i++)
                {


                    
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Entreprenuer_Commercial[i].BankName + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Entreprenuer_Commercial, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Fair_Personal.Count > 0)
            {
                for (int f = 0; f < _ordersRB.Fair_Personal.Count; f++)
                {
                    db.GetBranchByBRSTNRb(rb, _ordersRB.Fair_Personal[f].BRSTN);
                    SN = rb.LastNo + 1;
                    for (int a = 0; a <= f; a++)
                    {

                        _ordersRB.Fair_Personal[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Fair_Personal[a].PcsPerbook) - 1;
                        _ordersRB.Fair_Personal[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                }
                for (int i = 0; i < _ordersRB.Fair_Personal.Count; i++)
                {


                    
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Fair_Personal[i].BankName + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Fair_Personal, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Fair_Commercial.Count > 0)
            {
                for (int a = 0; a < _ordersRB.Fair_Commercial.Count; a++)
                {

                    _ordersRB.Fair_Commercial[a].StartingSerial = SN.ToString();
                    EN = SN + Int64.Parse(_ordersRB.Fair_Commercial[a].PcsPerbook) - 1;
                    _ordersRB.Fair_Commercial[a].EndingSerial = EN.ToString();
                    SN = EN + 1;
                }
                for (int i = 0; i < _ordersRB.Fair_Commercial.Count; i++)
                {


                   
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Fair_Commercial[i].BankName + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Fair_Commercial, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Kawit_Personal.Count > 0)
            {
                for (int f = 0; f < _ordersRB.Kawit_Personal.Count; f++)
                {
                    db.GetBranchByBRSTNRb(rb, _ordersRB.Kawit_Personal[f].BRSTN);
                    SN = rb.LastNo + 1;
                    for (int a = 0; a <= f; a++)
                    {

                        _ordersRB.Kawit_Personal[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Kawit_Personal[a].PcsPerbook) - 1;
                        _ordersRB.Kawit_Personal[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                }
                for (int i = 0; i < _ordersRB.Kawit_Personal.Count; i++)
                {


      
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Kawit_Personal[i].BankName + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Kawit_Personal, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Kawit_Commercial.Count > 0)
            {
                for (int a = 0; a < _ordersRB.Kawit_Commercial.Count; a++)
                {

                    _ordersRB.Kawit_Commercial[a].StartingSerial = SN.ToString();
                    EN = SN + Int64.Parse(_ordersRB.Kawit_Commercial[a].PcsPerbook) - 1;
                    _ordersRB.Kawit_Commercial[a].EndingSerial = EN.ToString();
                    SN = EN + 1;
                }
                for (int i = 0; i < _ordersRB.Kawit_Commercial.Count; i++)
                {


                    
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Kawit_Commercial[i].BankName + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Kawit_Commercial, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Masuwerte_Personal.Count > 0)
            {

                for (int f = 0; f < _ordersRB.Masuwerte_Personal.Count; f++)
                {
                    db.GetBranchByBRSTNRb(rb, _ordersRB.Masuwerte_Personal[f].BRSTN);
                    SN = rb.LastNo + 1;
                    for (int a = 0; a <= f; a++)
                    {

                        _ordersRB.Masuwerte_Personal[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Masuwerte_Personal[a].PcsPerbook) - 1;
                        _ordersRB.Masuwerte_Personal[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                }

                for (int i = 0; i < _ordersRB.Masuwerte_Personal.Count; i++)
                {


                    
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Masuwerte_Personal[i].BankName + "\\BlockP.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Masuwerte_Personal, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            if (_ordersRB.Masuwerte_Commercial.Count > 0)
            {
               
                    for (int a = 0; a < _ordersRB.Masuwerte_Commercial.Count; a++)
                    {

                        _ordersRB.Masuwerte_Commercial[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_ordersRB.Masuwerte_Commercial[a].PcsPerbook) - 1;
                        _ordersRB.Masuwerte_Commercial[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                

                for (int i = 0; i < _ordersRB.Masuwerte_Commercial.Count; i++)
                {


                    
                        string packkingListPath = outputFolder + "\\" + _ordersRB.Masuwerte_Commercial[i].BankName + "\\BlockC.txt";
                        if (File.Exists(packkingListPath))
                            File.Delete(packkingListPath);
                        // var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
                        file = File.CreateText(packkingListPath);
                        file.Close();

                        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                        {
                            //for (int i = 0; i < check; i++)
                            //{

                            string output = OutputServices.ConvertToBlockTextRB(_ordersRB.Masuwerte_Commercial, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                            //  }
                            file.WriteLine(output);
                        }
                    

                }
            }
            //var listofcheck = _checks.Select(r => r.ChkType).ToList();

            //for (int i = 0; i < listofcheck.Count; i++)
            //{
            //    //if (_checks[i].BankName == "Aspac_Rural")
            //    //{
            //        if (_checks[i].ChkType == "A")
            //        {
            //            string packkingListPath = outputFolder + "\\"+_checks[i].BankName + "\\BlockP.txt";
            //            if (File.Exists(packkingListPath))
            //                File.Delete(packkingListPath);
            //            var checks = _checks.Where(a => a.ChkType == _checks[i].ChkType).Distinct().ToList();
            //            file = File.CreateText(packkingListPath);
            //            file.Close();

            //            using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
            //            {
            //                //for (int i = 0; i < check; i++)
            //                //{

            //                string output = OutputServices.ConvertToBlockTextRB(checks, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
            //                //  }
            //                file.WriteLine(output);
            //            }
            //        }
            //   // }
            //}

            //foreach (string Scheck in listofcheck)
            //{
            //    if (Scheck == "A")
            //    {

            //        string packkingListPath = outputFolder + "\\"+"\\BlockP.txt";
            //        if (File.Exists(packkingListPath))
            //            File.Delete(packkingListPath);
            //        var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
            //        file = File.CreateText(packkingListPath);
            //        file.Close();

            //        using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
            //        {
            //            //for (int i = 0; i < check; i++)
            //            //{

            //                string output = OutputServices.ConvertToBlockTextRB(checks, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
            //          //  }
            //            file.WriteLine(output);
            //        }

            //    }
            //}
        }
        public void PackingTextRB(TypeofCheckModel _checksModel, frmMain _mainForm)
        {

            StreamWriter file;
            DbConServices db = new DbConServices();
            BranchModelRb rb = new BranchModelRb();
            Int64 SN = 0;
            Int64 EN = 0;
            //  db.GetAllData(_checksModel, _mainForm._batchfile);
         //   var listofcheck = _checksModel.Select(e => e.).ToList();

            if(_checksModel.Angeles_Personal.Count >0)
            {
                for (int f = 0; f < _checksModel.Angeles_Personal.Count; f++)
                {
                    db.GetBranchByBRSTNRb(rb, _checksModel.Angeles_Personal[f].BRSTN);
                    SN = rb.LastNo + 1;
                    for (int a = 0; a <= f; a++)
                    {

                        _checksModel.Angeles_Personal[a].StartingSerial = SN.ToString();
                        EN = SN + Int64.Parse(_checksModel.Angeles_Personal[a].PcsPerbook) - 1;
                        _checksModel.Angeles_Personal[a].EndingSerial = EN.ToString();
                        SN = EN + 1;
                    }
                }
                for (int i = 0; i < _checksModel.Angeles_Personal.Count; i++)
                {

                

                    string packkingListPath = outputFolder + "\\"+ _checksModel.Angeles_Personal[0].BankName+ "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                   // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Angeles_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }

            if (_checksModel.Angeles_Commercial.Count > 0)
            {
                for (int a = 0; a < _checksModel.Angeles_Commercial.Count; a++)
                {

                    _checksModel.Angeles_Personal[a].StartingSerial = SN.ToString();
                    EN = SN + Int64.Parse(_checksModel.Angeles_Personal[a].PcsPerbook) - 1;
                    _checksModel.Angeles_Personal[a].EndingSerial = EN.ToString();
                    SN = EN + 1;
                }
                for (int i = 0; i < _checksModel.Angeles_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Angeles_Commercial[0].BankName + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Angeles_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Aspac_Personal.Count > 0)
            {
              for (int i = 0; i < _checksModel.Aspac_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Aspac_Personal[0].BankName + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Aspac_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Aspac_Commercial.Count > 0)
            {
               
                for (int i = 0; i < _checksModel.Aspac_Commercial.Count; i++)
                {

                    string packkingListPath = outputFolder + "\\" + _checksModel.Aspac_Commercial[0].BankName + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Aspac_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Bank_Mabuhay_Personal.Count > 0)
            {
                for (int i = 0; i < _checksModel.Bank_Mabuhay_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Bank_Mabuhay_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Bank_Mabuhay_Commercial.Count > 0)
            {
                for (int i = 0; i < _checksModel.Bank_Mabuhay_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Bank_Mabuhay_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Cardona_Personal.Count > 0)
            {
                for (int i = 0; i < _checksModel.Cardona_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Cardona_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Cardona_Commercial.Count > 0)
            {
                for (int i = 0; i < _checksModel.Cardona_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Cardona_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Dulag_Personal.Count > 0)
            {
                for (int i = 0; i < _checksModel.Dulag_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Dulag_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Dulag_Commercial.Count > 0)
            {
                for (int i = 0; i < _checksModel.Dulag_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Dulag_Commercial[0].BankName + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Dulag_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Entreprenuer_Personal.Count > 0)
            {
                for (int i = 0; i < _checksModel.Entreprenuer_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Entreprenuer_Personal[0].BankName + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Entreprenuer_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Entreprenuer_Commercial.Count > 0)
            {

                for (int i = 0; i < _checksModel.Entreprenuer_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Entreprenuer_Commercial[0].BankName + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Entreprenuer_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }

            }
            if (_checksModel.Imus_Binan_Personal.Count > 0)
            {
                for (int i = 0; i < _checksModel.Imus_Binan_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Imus_Binan_Personal[0].BankName + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Imus_Binan_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Imus_Binan_Commercial.Count > 0)
            {
                for (int i = 0; i < _checksModel.Imus_Binan_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + _checksModel.Imus_Binan_Commercial[0].BankName + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Imus_Binan_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Kawit_Personal.Count > 0)
            {
                for (int i = 0; i < _checksModel.Kawit_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Kawit_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Kawit_Commercial.Count > 0)
            {
                for (int i = 0; i < _checksModel.Kawit_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Kawit_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Masuwerte_Personal.Count > 0)
            {
                for (int i = 0; i < _checksModel.Masuwerte_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Masuwerte_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Masuwerte_Commercial.Count > 0)
            {
                for (int i = 0; i < _checksModel.Masuwerte_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Masuwerte_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Mexico_Personal.Count > 0)
            {
                for (int i = 0; i < _checksModel.Mexico_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Mexico_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Mexico_Commercial.Count > 0)
            {
                for (int i = 0; i < _checksModel.Mexico_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Mexico_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Porac_Personal.Count > 0)
            {
                for (int i = 0; i < _checksModel.Porac_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Porac_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Porac_Commercial.Count > 0)
            {
                for (int i = 0; i < _checksModel.Porac_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Porac_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Salinas_Personal.Count > 0)
            {
                for (int i = 0; i < _checksModel.Salinas_Personal.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Salinas_Personal, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            if (_checksModel.Salinas_Commercial.Count > 0)
            {
                for (int i = 0; i < _checksModel.Salinas_Commercial.Count; i++)
                {



                    string packkingListPath = outputFolder + "\\" + frmMain.outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    // var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListRb(_checksModel.Salinas_Commercial, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
           
        }
        public void SaveToPackingDBFRb(TypeofCheckModel _checks, string _batchNumber, frmMain _mainForm)
        {
            string dbConnection;
            string tempCheckType = "";
            int blockNo = 0, blockCounter = 0;
            DbConServices db = new DbConServices();
            //   db.GetAllData(_checks, _mainForm._batchfile);
            
            //   var listofchecks = _checks.Select(e => e.ChkType).Distinct().ToList();
            if (_checks.Aspac_Personal.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Aspac_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                //foreach (string checktype in listofchecks)
                //{
                //for (int i = 0; i < _checks.Aspac_Personal.Count; i++)
                //{



                //if (checktype == "A" || checktype == "B")
                //{


                //Check if packing file exists
                //if (!File.Exists(_filepath))
                //{
                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Aspac_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
                oConnect.Close();
            }
             if (  _checks.Aspac_Commercial.Count > 0)
             {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Aspac_Commercial[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                    OleDbCommand oCommand2;
                    oConnect2.Open();
                    foreach (var check in _checks.Aspac_Commercial)
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
                         "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                         "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                        check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                        oCommand2 = new OleDbCommand(sql, oConnect2);

                        oCommand2.ExecuteNonQuery();
                    }
                    oConnect2.Close();
                    //}
                
            }
            if (_checks.Angeles_Personal.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Angeles_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                //foreach (string checktype in listofchecks)
                //{
                //for (int i = 0; i < _checks.Aspac_Personal.Count; i++)
                //{



                //if (checktype == "A" || checktype == "B")
                //{


                //Check if packing file exists
                //if (!File.Exists(_filepath))
                //{
                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Angeles_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
            
                oConnect.Close();

            }
            if(_checks.Angeles_Commercial.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Angeles_Commercial[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                OleDbCommand oCommand2;
                oConnect2.Open();
                foreach (var check in _checks.Angeles_Commercial)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand2 = new OleDbCommand(sql, oConnect2);

                    oCommand2.ExecuteNonQuery();
                }
                oConnect2.Close();
                //}

            }
            if (_checks.Bank_Mabuhay_Personal.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Bank_Mabuhay_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                //foreach (string checktype in listofchecks)
                //{
                //for (int i = 0; i < _checks.Aspac_Personal.Count; i++)
                //{



                //if (checktype == "A" || checktype == "B")
                //{


                //Check if packing file exists
                //if (!File.Exists(_filepath))
                //{
                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Bank_Mabuhay_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
                oConnect.Close();
            }
            if(_checks.Bank_Mabuhay_Commercial.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Bank_Mabuhay_Commercial[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                OleDbCommand oCommand2;
                oConnect2.Open();
                foreach (var check in _checks.Bank_Mabuhay_Commercial)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand2 = new OleDbCommand(sql, oConnect2);

                    oCommand2.ExecuteNonQuery();
                }
                oConnect2.Close();
                //}

            }
            if (_checks.Cardona_Personal.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Cardona_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
               
                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Cardona_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
                oConnect.Close();
            }
            if (_checks.Cardona_Commercial.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Cardona_Commercial[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                OleDbCommand oCommand2;
                oConnect2.Open();
                foreach (var check in _checks.Cardona_Commercial)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand2 = new OleDbCommand(sql, oConnect2);

                    oCommand2.ExecuteNonQuery();
                }
                oConnect2.Close();
                //}

            }
            if (_checks.Dulag_Personal.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Dulag_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";

                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Dulag_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
                oConnect.Close();
            }
            if(_checks.Dulag_Commercial.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Dulag_Commercial[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                OleDbCommand oCommand2;
                oConnect2.Open();
                foreach (var check in _checks.Dulag_Commercial)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand2 = new OleDbCommand(sql, oConnect2);

                    oCommand2.ExecuteNonQuery();
                }
                oConnect2.Close();
                //}

            }
            if (_checks.Entreprenuer_Personal.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Entreprenuer_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";

                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Entreprenuer_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
                oConnect.Close();
            }
            if(_checks.Entreprenuer_Commercial.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Entreprenuer_Commercial[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                OleDbCommand oCommand2;
                oConnect2.Open();
                foreach (var check in _checks.Entreprenuer_Commercial)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand2 = new OleDbCommand(sql, oConnect2);

                    oCommand2.ExecuteNonQuery();
                }
                oConnect2.Close();
                //}

            }
            if (_checks.Imus_Binan_Personal.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Imus_Binan_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";

                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Imus_Binan_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
                oConnect.Close();
            }
            if (_checks.Imus_Binan_Commercial.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Imus_Binan_Commercial[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                OleDbCommand oCommand2;
                oConnect2.Open();
                foreach (var check in _checks.Imus_Binan_Commercial)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand2 = new OleDbCommand(sql, oConnect2);

                    oCommand2.ExecuteNonQuery();
                }
                oConnect2.Close();
                //}

            }
            if (_checks.Kawit_Personal.Count > 0)
            {
             dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Kawit_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";

                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Kawit_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
                oConnect.Close();
            }
            if (_checks.Kawit_Commercial.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Kawit_Commercial[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                OleDbCommand oCommand2;
                oConnect2.Open();
                foreach (var check in _checks.Kawit_Commercial)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand2 = new OleDbCommand(sql, oConnect2);

                    oCommand2.ExecuteNonQuery();
                }
                oConnect2.Close();
                //}

            }
            if (_checks.Masuwerte_Personal.Count > 0)
            {
              dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Masuwerte_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";

                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Masuwerte_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
                oConnect.Close();
            }
            if(_checks.Masuwerte_Commercial.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Masuwerte_Commercial[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                OleDbCommand oCommand2;
                oConnect2.Open();
                foreach (var check in _checks.Masuwerte_Commercial)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand2 = new OleDbCommand(sql, oConnect2);

                    oCommand2.ExecuteNonQuery();
                }
                oConnect2.Close();
                //}

            }
            if (_checks.Mexico_Personal.Count > 0)
            {
             dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Mexico_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";

                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Mexico_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
                oConnect.Close();
            }
            if(_checks.Mexico_Commercial.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Mexico_Commercial[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                OleDbCommand oCommand2;
                oConnect2.Open();
                foreach (var check in _checks.Mexico_Commercial)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand2 = new OleDbCommand(sql, oConnect2);

                    oCommand2.ExecuteNonQuery();
                }
                oConnect2.Close();
                //}

            }
            if (_checks.Porac_Personal.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Porac_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";

                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Porac_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
                oConnect.Close();
            }
            if(_checks.Porac_Commercial.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Porac_Commercial[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                OleDbCommand oCommand2;
                oConnect2.Open();
                foreach (var check in _checks.Porac_Commercial)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand2 = new OleDbCommand(sql, oConnect2);

                    oCommand2.ExecuteNonQuery();
                }
                oConnect2.Close();
                //}

            }
            if (_checks.Salinas_Personal.Count > 0)
            {
              dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Salinas_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";

                OleDbConnection oConnect = new OleDbConnection(dbConnection);
                OleDbCommand oCommand;
                oConnect.Open();
                oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                oCommand.ExecuteNonQuery();
                foreach (var check in _checks.Salinas_Personal)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand = new OleDbCommand(sql, oConnect);

                    oCommand.ExecuteNonQuery();
                }
                oConnect.Close();
            }
            if(_checks.Salinas_Commercial.Count > 0)
            {
                dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _checks.Salinas_Personal[0].BankName + "\\Packing.dbf" + "; Mode=ReadWrite;";
                OleDbConnection oConnect2 = new OleDbConnection(dbConnection);
                OleDbCommand oCommand2;
                oConnect2.Open();
                foreach (var check in _checks.Salinas_Commercial)
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
                     "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                     "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                    check.StartingSerial + "','" + check.EndingSerial + "','" + check.Company + "','" + check.Company + "')";

                    oCommand2 = new OleDbCommand(sql, oConnect2);

                    oCommand2.ExecuteNonQuery();
                }
                oConnect2.Close();
                //}

            }
        }
        public void PrinterFileRb(TypeofCheckModel _checkModel, frmMain _mainForm)
        {

            // DbConServices db = new DbConServices();
            // db.GetAllData(_checkModel, _mainForm._batchfile);
            StreamWriter file;

            /// var listofchecks = _checkModel.Select(e => e.ChkType).Distinct().ToList();
            if (_checkModel.Angeles_Personal.Count > 0)
            {
                //foreach (string checktype in _checkModel.Angeles_Personal)
                //{
                //    if (checktype == "A")
                //    {
                //foreach(var check in _checkModel.Angeles_Personal)
                //{ 
                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Angeles_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";
                // var check = _checkModel.Where(e => e.ChkType == checktype).ToList();

                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                //for (int a = 0; a < check.Count; a++)
                //{


                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Angeles_Personal);

                    file.WriteLine(output);
                }
                //}
                //  ZipFileServices.CopyPrinterFile(checktype, _mainForm);
                // ZipFileServices.CopyPackingDBF(checktype, _mainForm);
            }

            if(_checkModel.Angeles_Commercial.Count >0)
            { 
                //foreach (string checktype in listofchecks)
                //{
                //    if (checktype == "B")
                //    {
                        string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Angeles_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";
                    //    var check = _checkModel.Where(e => e.ChkType == checktype).ToList();
                        if (File.Exists(printerFilePath))
                            File.Delete(printerFilePath);

                        file = File.CreateText(printerFilePath);
                        file.Close();
                        //for (int a = 0; a < check.Count; a++)
                        //{


                        using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                        {
                            string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Angeles_Commercial);

                            file.WriteLine(output);
                        }
           
            }
            if (_checkModel.Aspac_Personal.Count > 0)
            {
               
                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Aspac_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";
                
                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();
              
                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Aspac_Personal);

                    file.WriteLine(output);
                }
               
            }

            if (_checkModel.Aspac_Commercial.Count > 0)
            {
               
                string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Aspac_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";
               
                if (File.Exists(printerFilePath))
                    File.Delete(printerFilePath);

                file = File.CreateText(printerFilePath);
                file.Close();
              
                using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Aspac_Commercial);

                    file.WriteLine(output);
                }

            }
            if (_checkModel.Bank_Mabuhay_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Bank_Mabuhay_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";

                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Bank_Mabuhay_Personal);

                    file.WriteLine(output);
                }

            }

            if (_checkModel.Bank_Mabuhay_Commercial.Count > 0)
            {

                string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Bank_Mabuhay_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";

                if (File.Exists(printerFilePath))
                    File.Delete(printerFilePath);

                file = File.CreateText(printerFilePath);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Bank_Mabuhay_Commercial);

                    file.WriteLine(output);
                }

            }
            if (_checkModel.Cardona_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Cardona_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";

                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Cardona_Personal);

                    file.WriteLine(output);
                }

            }

            if (_checkModel.Cardona_Commercial.Count > 0)
            {

                string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Cardona_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";

                if (File.Exists(printerFilePath))
                    File.Delete(printerFilePath);

                file = File.CreateText(printerFilePath);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Cardona_Commercial);

                    file.WriteLine(output);
                }

            }
            if (_checkModel.Dulag_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Dulag_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";

                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Dulag_Personal);

                    file.WriteLine(output);
                }

            }

            if (_checkModel.Dulag_Commercial.Count > 0)
            {

                string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Dulag_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";

                if (File.Exists(printerFilePath))
                    File.Delete(printerFilePath);

                file = File.CreateText(printerFilePath);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Dulag_Commercial);

                    file.WriteLine(output);
                }

            }
            if (_checkModel.Entreprenuer_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Entreprenuer_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";

                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Entreprenuer_Personal);

                    file.WriteLine(output);
                }

            }

            if (_checkModel.Entreprenuer_Commercial.Count > 0)
            {

                string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Entreprenuer_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";

                if (File.Exists(printerFilePath))
                    File.Delete(printerFilePath);

                file = File.CreateText(printerFilePath);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Entreprenuer_Commercial);

                    file.WriteLine(output);
                }

            }
            if (_checkModel.Imus_Binan_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Imus_Binan_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";

                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Imus_Binan_Personal);

                    file.WriteLine(output);
                }

            }

            if (_checkModel.Imus_Binan_Commercial.Count > 0)
            {

                string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Imus_Binan_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";

                if (File.Exists(printerFilePath))
                    File.Delete(printerFilePath);

                file = File.CreateText(printerFilePath);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Imus_Binan_Commercial);

                    file.WriteLine(output);
                }

            }
            if (_checkModel.Kawit_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Kawit_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";

                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Kawit_Personal);

                    file.WriteLine(output);
                }

            }

            if (_checkModel.Kawit_Commercial.Count > 0)
            {

                string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Kawit_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";

                if (File.Exists(printerFilePath))
                    File.Delete(printerFilePath);

                file = File.CreateText(printerFilePath);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Kawit_Commercial);

                    file.WriteLine(output);
                }

            }
            if (_checkModel.Masuwerte_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Masuwerte_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";

                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Masuwerte_Personal);

                    file.WriteLine(output);
                }

            }

            if (_checkModel.Masuwerte_Commercial.Count > 0)
            {

                string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Masuwerte_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";

                if (File.Exists(printerFilePath))
                    File.Delete(printerFilePath);

                file = File.CreateText(printerFilePath);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Masuwerte_Commercial);

                    file.WriteLine(output);
                }

            }
            if (_checkModel.Mexico_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Mexico_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";

                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Mexico_Personal);

                    file.WriteLine(output);
                }

            }

            if (_checkModel.Mexico_Commercial.Count > 0)
            {

                string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Mexico_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";

                if (File.Exists(printerFilePath))
                    File.Delete(printerFilePath);

                file = File.CreateText(printerFilePath);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Mexico_Commercial);

                    file.WriteLine(output);
                }

            }
            if (_checkModel.Porac_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Porac_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";

                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Porac_Personal);

                    file.WriteLine(output);
                }

            }

            if (_checkModel.Porac_Commercial.Count > 0)
            {

                string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Porac_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";

                if (File.Exists(printerFilePath))
                    File.Delete(printerFilePath);

                file = File.CreateText(printerFilePath);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Porac_Commercial);

                    file.WriteLine(output);
                }

            }
            if (_checkModel.Salinas_Personal.Count > 0)
            {

                string printerFilePathA = Application.StartupPath + "\\Output\\" + _checkModel.Salinas_Personal[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "P.txt";

                if (File.Exists(printerFilePathA))
                    File.Delete(printerFilePathA);

                file = File.CreateText(printerFilePathA);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Salinas_Personal);

                    file.WriteLine(output);
                }

            }

            if (_checkModel.Salinas_Commercial.Count > 0)
            {

                string printerFilePath = Application.StartupPath + "\\Output\\" + _checkModel.Salinas_Commercial[0].BankName + "\\AUB" + _mainForm.batchfile.Substring(0, 4) + "C.txt";

                if (File.Exists(printerFilePath))
                    File.Delete(printerFilePath);

                file = File.CreateText(printerFilePath);
                file.Close();

                using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                {
                    string output = OutputServices.ConvertToPrinterFileRB(_checkModel.Salinas_Commercial);

                    file.WriteLine(output);
                }

            }

        }
       //public void WriteOrderFile(List<OrderModel> _order)
       // {
       //     Excel.Application xlAl = new Excel.Application();
       //     Excel.Workbook xlWorkBook;
       //     Excel.Worksheet xlWorkSheet;
       //     object misValue = System.Reflection.Missing.Value;
       //     xlWorkBook = xlAl.Workbooks.Add(misValue);
       //     xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
       //     xlWorkSheet.Cells[1, 1] = "BRSTN";
       //     xlWorkSheet.Cells[1, 2] = "AccountNo";
       //     xlWorkSheet.Cells[1, 3] = "Account Name";
       //     xlWorkSheet.Cells[1, 4] = "Account Name 2";
       //     xlWorkSheet.Cells[1, 5] = "ChkType";
       //     xlWorkSheet.Cells[1, 6] = "Cheque Name";
       //     xlWorkSheet.Cells[1, 7] = "Branch Name";
            
       //     for (int i = 0; i < _order.Count; i++)
       //     {
       //         if (_order[i].BRSTN == null)
       //         {
                    
       //         }
       //         else
       //         {
       //             xlWorkSheet.Cells[i+2, 1] = _order[i].BRSTN;
       //             xlWorkSheet.Cells[i+1 , 2] = _order[i].AccountNo;
       //             xlWorkSheet.Cells[i+1, 3] = _order[i].AccountName;
       //             xlWorkSheet.Cells[i+1, 4] = _order[i].AccountName2;
       //             xlWorkSheet.Cells[i+1, 5] = _order[i].ChkType;
       //             xlWorkSheet.Cells[i +1, 6] = _order[i].ChkName;
       //             xlWorkSheet.Cells[i+1, 7] = _order[i].BranchName;
       //         }
                
       //     }
       //     xlWorkBook.SaveAs(Application.StartupPath + "\\Order.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
       //     xlWorkBook.Close(true, misValue, misValue);
       //     xlAl.Quit();
            
       //    // string path = Application.StartupPath + "\\OrderFile.txt";;
       //     //if (File.Exists(path))
       //     //    File.Delete(path);

       //     //file = File.CreateText(path);
       //     //file.Close();
            
       //     //using (file = new StreamWriter(File.Open(path, FileMode.OpenOrCreate,FileAccess.ReadWrite)))
       //     //{
       //     //    // var listofcchecks = _order.Select(e => e.BRSTN).ToList();
       //     //    for (int i = 0; i < _order.Count; i++)
       //     //    {
       //     //        if (_order[i].BRSTN == null)
       //     //        {
       //     //            i++;
       //     //        }
       //     //        else

       //     //        {
       //     //            string output = _order[i].BRSTN + _order[i].AccountName + _order[i].AccountName2 + _order[i].AccountNo + _order[i].BranchName
       //     //                            + _order[i].Address2 + _order[i].Address3 + _order[i].Address4 + _order[i].Address5 + _order[i].ChkName + _order[i].ChkType 
       //     //                            + _order[i].deliveryDate + _order[i].EndingSerial + _order[i].StartingSerial ;


       //     //            //  string output = OutputServices.ConvertToPrinterFileRB(_order);

       //     //            file.WriteLine(output);
       //     //        }
       //     //    }
       //     //    file.Close();
       //     //}
       // }
        public void DoBlockProcessM(List<ManualOrderModel> _checks, Encode _mainForm, string _outputFolder)
        {

            var listofcheck = _checks.Select(r => r.ChkType).ToList();
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "A")
                {

                    string packkingListPath = outputFolder + "\\" + _outputFolder + "\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{


                        string output = OutputServices.ConvertToBlockTextM(checks, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "MC")
                {

                    string packkingListPath = outputFolder + "\\" + _outputFolder + "\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{


                        string output = OutputServices.ConvertToBlockTextM(checks, "MANAGER'S CHECK", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "MCS")
                {

                    string packkingListPath = outputFolder + "\\" + _outputFolder + "\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        //for (int i = 0; i < check; i++)
                        //{


                        string output = OutputServices.ConvertToBlockTextM(checks, "MANAGER'S CHECK (SMART)", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn.userName);
                        //  }
                        file.WriteLine(output);
                    }

                }
            }
        }
        public void PackingTextM(List<ManualOrderModel> _checksModel, Encode _mainForm, string _outputFolder)
        {

            StreamWriter file;
            DbConServices db = new DbConServices();
            //  db.GetAllData(_checksModel, _mainForm._batchfile);
            var listofcheck = _checksModel.Select(e => e.ChkType).ToList();

            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "MCS")
                {

                    string packkingListPath = outputFolder + "\\" + _outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListM(checks, "MANAGER'S CHECK (SMART)", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "B")
                {

                    string packkingListPath = outputFolder + "\\" + _outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListM(checks, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "MC")
                {

                    string packkingListPath = outputFolder + "\\" + _outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPackingListM(checks, "MANAGER'S CHECK", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
        }
        public void SaveToPackingDBFM(List<ManualOrderModel> _checks, string _batchNumber, Encode _mainForm, string _outputFolder)
        {
            string dbConnection;
            string tempCheckType = "";
            int blockNo = 0, blockCounter = 0;
            DbConServices db = new DbConServices();
            //   db.GetAllData(_checks, _mainForm._batchfile);

            var listofchecks = _checks.Select(e => e.ChkType).Distinct().ToList();

           
            foreach (string checktype in listofchecks)
            {

                if (checktype == "MC")
                {
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _outputFolder + "\\Packing.dbf" + "; Mode=ReadWrite;";

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
                         "NO_BKS, CK_NO_B, CK_NO_E,DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                         "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                        check.StartingSerial + "','" + check.EndingSerial + "','CPC','"+ check.ChkType+"')";

                        oCommand = new OleDbCommand(sql, oConnect);

                        oCommand.ExecuteNonQuery();
                    }
                    oConnect.Close();
                }
            }
            foreach (string checktype in listofchecks)
            {

                if (checktype == "MCS")
                {
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + _outputFolder + "\\Packing.dbf" + "; Mode=ReadWrite;";

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
                         "NO_BKS, CK_NO_B, CK_NO_E,DELIVERTO, CHKNAME) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                         "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                        check.StartingSerial + "','" + check.EndingSerial + "','CPC','" + check.ChkType + "')";

                        oCommand = new OleDbCommand(sql, oConnect);

                        oCommand.ExecuteNonQuery();
                    }
                    oConnect.Close();
                }
            }
        }
        public void PrinterFileM(List<ManualOrderModel> _checkModel, Encode _mainForm, string _outputFolder)
        {

            // DbConServices db = new DbConServices();
            // db.GetAllData(_checkModel, _mainForm._batchfile);
            StreamWriter file;

            var listofchecks = _checkModel.Select(e => e.ChkType).Distinct().ToList();

            foreach (string checktype in listofchecks)
            {
                if (checktype == "MCS")
                {
                    string printerFilePathA = Application.StartupPath + "\\Output\\" + _outputFolder + "\\AUB" + /*_mainForm.batchfile.Substring(0, 4)*/  "P.txt";
                    var check = _checkModel.Where(e => e.ChkType == checktype).ToList();
                    if (File.Exists(printerFilePathA))
                        File.Delete(printerFilePathA);

                    file = File.CreateText(printerFilePathA);
                    file.Close();

                    //for (int a = 0; a < check.Count; a++)
                    //{


                    using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPrinterFileM(check);

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
                    string printerFilePath = Application.StartupPath + "\\Output\\" + _outputFolder + "\\AUB" /*+ _mainForm.batchfile.Substring(0, 4)*/ + "C.txt";
                    var check = _checkModel.Where(e => e.ChkType == checktype).ToList();
                    if (File.Exists(printerFilePath))
                        File.Delete(printerFilePath);

                    file = File.CreateText(printerFilePath);
                    file.Close();
                    //for (int a = 0; a < check.Count; a++)
                    //{


                    using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPrinterFileM(check);

                        file.WriteLine(output);
                    }
                    //}
                    // ZipFileServices.CopyPrinterFile(checktype, _mainForm);
                    //ZipFileServices.CopyPackingDBF(checktype, _mainForm);
                }
            }
            foreach (string checktype in listofchecks)
            {
                if (checktype == "MC")
                {
                    string printerFilePathA = Application.StartupPath + "\\Output\\" + _outputFolder + "\\AUB" + /*_mainForm.batchfile.Substring(0, 4)*/  "P.txt";
                    var check = _checkModel.Where(e => e.ChkType == checktype).ToList();
                    if (File.Exists(printerFilePathA))
                        File.Delete(printerFilePathA);

                    file = File.CreateText(printerFilePathA);
                    file.Close();

                    //for (int a = 0; a < check.Count; a++)
                    //{


                    using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                    {
                        string output = OutputServices.ConvertToPrinterFileM(check);

                        file.WriteLine(output);
                    }
                    //}
                    //  ZipFileServices.CopyPrinterFile(checktype, _mainForm);
                    // ZipFileServices.CopyPackingDBF(checktype, _mainForm);
                }

            }
        }
        //private string ListofChecks(List<OrderModelRb> _orderChecks,frmMain _main)
        //{
        //    TypeofCheckModel checkType = new TypeofCheckModel();

        //    checkType.Angeles = new List<OrderModelRb>();
        //    checkType.Aspac_Commercial = new List<OrderModelRb>();
        //    checkType.Aspac_Personal = new List<OrderModelRb>();
        //    checkType.Bank_Mabuhay = new List<OrderModelRb>();
        //    checkType.Cardona = new List<OrderModelRb>();
        //    checkType.Dulag = new List<OrderModelRb>();
        //    checkType.Entreprenuer = new List<OrderModelRb>();
        //    checkType.Fair = new List<OrderModelRb>();
        //    checkType.Imus_Binan = new List<OrderModelRb>();
        //    checkType.Kawit = new List<OrderModelRb>();
        //    checkType.Masuwerte = new List<OrderModelRb>();
        //    checkType.Mexico = new List<OrderModelRb>();
        //    checkType.Porac = new List<OrderModelRb>();
        //    checkType.Progressive = new List<OrderModelRb>();
        //    checkType.Salinas = new List<OrderModelRb>();
        //    return "";//Return blank if there is no error
        //}
    }
}
