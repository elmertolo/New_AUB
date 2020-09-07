using New_AUB.Models;
using New_AUB.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace New_AUB
{
    public partial class frmMain : Form
    {
        public string batchfile = "";
        public DateTime deliveryDate;
        DateTime dateTime;
        DbConServices con = new DbConServices();
        List<BranchModel> branch = new List<BranchModel>();
        List<BranchModel> updateBranch = new List<BranchModel>();
        List<BranchModelRb> branchRb = new List<BranchModelRb>();
        ProcessServices process = new ProcessServices();
        List<OrderModel> orderList = new List<OrderModel>();
        List<OrderModel> orderList2 = new List<OrderModel>();
        List<OrderModelRb> orderListRb = new List<OrderModelRb>();
        ZipfileServices z = new ZipfileServices();
        public static string outputFolder = "Regular Checks";
        public static string banks = "";
        public static string batch = "";
        Int64 startsSN = 0;
        Int64 endSN = 0;
        public static string _fileName = "";
        public frmMain()
        {

            InitializeComponent();
            dateTime = dateTimePicker1.MinDate = DateTime.Now; //Disable selection of backdated dates to prevent errors  
        }
           
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
         

           
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            Encode en = new Encode();
            en.Show();
            this.Hide();
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            encodeToolStripMenuItem.Enabled = false;
            string errorMessage = "";
            batchfile = txtBatch.Text;
            con.GetAllBranches(branch);// get all details in branch database
            con.GetAllBranchesRB(branchRb);// get all details in rural branch database
            deliveryDate = dateTimePicker1.Value;
          if (txtBatch.Text == "")
          {
                MessageBox.Show("Please Enter Batch Number!!!");
          }
          else
          {
                if (deliveryDate == dateTime)
                {
                    MessageBox.Show("Please set Delivery Date!");
                }
                else
                {


                    deliveryDate = dateTimePicker1.Value;
                    if (Directory.GetFiles(Application.StartupPath + "\\Head\\").Length == 0) // if the path folder is empty
                        MessageBox.Show("No files found in directory path", "***System Error***");
                    else
                    {
                        string[] list = Directory.GetFiles(Application.StartupPath + "\\Head\\");

                        string Extension = "";

                        foreach (string FileName in list)
                        {
                            //Get the Extension Name
                            int LoopCount = FileName.ToString().Length - 2;
                            while (LoopCount > 0)
                            {

                                if (FileName.ToString().Substring(LoopCount, 1) == "." && Extension == "")
                                {
                                    Extension = FileName.ToString().Substring(LoopCount + 1, FileName.ToString().Length - LoopCount - 1).ToUpper();
                                }

                                LoopCount = LoopCount - 1;
                            }
                            //MessageBox.Show(Extension);
                            string Cont = "";
                            if (Extension == "TXT")
                            {
                                if (list != null)
                                {

                                    for (int i = 0; i < list.Length; i++)
                                    {
                                        //reading and storing details from the order files
                                        string[] lines = File.ReadAllLines(FileName);
                                       Cont = Path.GetFileNameWithoutExtension(list[i]);

                                        if (Cont.Contains("CON") || Cont.Contains("CV")) //Checking the file name to identify their checktype
                                        {

                                            for (int a = 1; a < lines.Length - 1; a++)
                                            {

                                                int qty = int.Parse(lines[a].Substring(0, 3));
                                             
                                                //Checking if the branch code is exsiting in the database
                                                var listofbranch = branch.FirstOrDefault(sr => sr.BranchCode == lines[a].Substring(65, 3));
                                                if (listofbranch == null)
                                                {
                                                    MessageBox.Show("Branch Code Does not exist in branches Table!!");
                                                    errorMessage += "Branch Code Does not exist in branches Table!!";
                                                    ProcessServices.ErrorMessage(errorMessage);
                                                }
                                                else
                                                {
                                                    if (Cont.Contains("CON"))
                                                        startsSN = listofbranch.Con_LastNo + 1;
                                                    else
                                                        startsSN = listofbranch.CV_LastNo + 1;

                                                    if (listofbranch.Company == "TGP")// Check if there is a Tone Guide branch in the order file
                                                    {

                                                        MessageBox.Show("This " + listofbranch.BRSTN + " : is Tone Guide Branch Please get series from them!!!");
                                                        errorMessage += "This " + listofbranch.BRSTN + " : is Tone Guide Branch Please get series from them!!!";
                                                        ProcessServices.ErrorMessage(errorMessage);
                                                        Environment.Exit(0);
                                                    }
                                                    else
                                                    {
                                                        for (int r = 0; r < qty; r++) //Loop for quantity of the order
                                                        {
                                                            OrderModel order = new OrderModel();

                                                            order.Quantity = 1;
                                                            if (Cont.Substring(11, 3) == "CON")
                                                            {

                                                                order.ChkType = "CON";
                                                                order.ChkName = "Continues Check";
                                                                outputFolder = "Continues_Check";
                                                                order.StartingSerial = startsSN.ToString();
                                                                endSN = startsSN + 49;

                                                            }
                                                            else if (Cont.Substring(11, 2) == "CV")
                                                            {
                                                                order.ChkType = "CV";
                                                                order.ChkName = "Check with Voucher";
                                                                outputFolder = "Check_with_Voucher";
                                                                order.StartingSerial = startsSN.ToString();
                                                                endSN = startsSN + 99;

                                                            }

                                                            order.BRSTN = listofbranch.BRSTN;
                                                            order.BranchName = listofbranch.Address1;
                                                            order.Address2 = listofbranch.Address2;
                                                            order.Address3 = listofbranch.Address3;
                                                            order.Address3 = listofbranch.Address4;
                                                            order.Address3 = listofbranch.Address5;
                                                            order.Address3 = listofbranch.Address6;
                                                            order.BranchCode = listofbranch.BranchCode;

                                                            order.AccountName = lines[a].Substring(65, 35);
                                                            order.AccountNo = lines[a].Substring(121, 12);
                                                            order.Extension = Path.GetExtension(list[i]);
                                                            order.FileName = Path.GetFileNameWithoutExtension(list[i]);
                                                            batch = order.FileName;

                                                            if (order.AccountName2 == null)
                                                                order.AccountName2 = " ";
                                                            order.EndingSerial = endSN.ToString();


                                                            orderList.Add(order);
                                                            startsSN = endSN + 1;
                                                        }
                                                        if(Cont.Contains("CON"))
                                                          listofbranch.Con_LastNo = endSN;
                                                        else
                                                          listofbranch.CV_LastNo = endSN;
                                                        updateBranch.Add(listofbranch);
                                                    }
                                                }
                                            }
                                        }

                                        else if(Cont.Contains("(A)") || Cont.Contains("(R") || Cont.Contains("(S")) //Checking the file name to identify their checktype
                                        {
                                            
                                                for (int b = 0; b < lines.Length -1; b++)
                                                {
                                                if (lines[b].Length < 10)
                                                {
                                                    b++;
                                                  //  MessageBox.Show("Adik kana !!");

                                                }
                                                else
                                                {
                                                     int qty = int.Parse(lines[b].Substring(81, 2)); // Getting quantity per line from the order  file

                                                    // Getting data from database according on the BRSTN and Checking if the BRSTN in the order file is existing in the database
                                                    var listofbranch = branch.FirstOrDefault(sr => sr.BRSTN == lines[b].Substring(1, 9));
                                                    if (listofbranch == null)
                                                    {
                                                        MessageBox.Show("BRSTN : " +lines[b].Substring(1,9)+" Does not exist in branches Table!!");
                                                        errorMessage += "BRSTN : " + lines[b].Substring(1, 9) + " Does not exist in branches Table!!";
                                                        ProcessServices.ErrorMessage(errorMessage);
                                                        Environment.Exit(0);
                                                    }
                                                    else
                                                    {
                                                        if (Cont.Contains("A")) //Getting Last Number series for the starting series base on product type
                                                            startsSN = listofbranch.Adv_LastNo + 1;
                                                        else
                                                            startsSN = listofbranch.Reg_LastNo + 1;


                                                        if (listofbranch.Company == "TGP") // Checking if there is a Tone Guide branch in the order file
                                                        {

                                                            MessageBox.Show("This " + listofbranch.BRSTN + " : is Tone Guide Branch Please get series from them!!!");
                                                            errorMessage += "This " + listofbranch.BRSTN + " : is Tone Guide Branch Please get series from them!!!";
                                                            ProcessServices.ErrorMessage(errorMessage);
                                                            Environment.Exit(0);
                                                        }
                                                        else
                                                        {
                                                            for (int cc = 0; cc <qty ; cc++)
                                                            {


                                                                OrderModel order = new OrderModel();
                                                             
                                                                    if (lines[b].Substring(78, 1) == "2")
                                                                    {

                                                                        orderList[b - 1].AccountName2 = lines[b].Substring(22, 35); //Gettting 2nd Name
                                                                        //b++;

                                                                    }
                                                                    else
                                                                    {
                                                                    

                                                                        order.BRSTN = lines[b].Substring(1, 9);
                                                                        order.ChkType = lines[b].Substring(0, 1);
                                                                        order.AccountNo = lines[b].Substring(10, 12);
                                                                        order.AccountName = lines[b].Substring(22, 35);

                                                                        order.Quantity = 1;

                                                                        order.Extension = Path.GetExtension(list[i]);
                                                                        order.FileName = Path.GetFileNameWithoutExtension(list[i]);
                                                                        batch = order.FileName;
                                                                    if (order.ChkType == "B")
                                                                    {
                                                                        if (order.FileName.Substring(11, 1) == "A" && order.FileName.Substring(2, 3) == "CAP")
                                                                        {
                                                                            outputFolder = "Advantage\\Captive";
                                                                            order.ChkName = "Advantage Commercial Checks";
                                                                        }
                                                                        else if (order.FileName.Substring(11, 1) == "A" && order.FileName.Substring(2, 3) == "SEC")
                                                                        {
                                                                            outputFolder = "Advantage\\SecurForms";
                                                                            order.ChkName = "Advantage Commercial Checks";
                                                                        }

                                                                        else if (order.FileName.Substring(11, 1) == "S")
                                                                        {
                                                                            outputFolder = "StarterChecks";
                                                                            order.ChkName = "Starter Check Commercial";
                                                                        }
                                                                        else if (order.FileName.Substring(11, 1) == "R")
                                                                        {
                                                                            outputFolder = "Regular Checks";
                                                                            order.ChkName = "Regular Commercial Checks";

                                                                        }
                                                                        order.PcsPerbook = "100";
                                                                    }
                                                                    else

                                                                    {
                                                                        if (order.FileName.Substring(11, 1) == "A" && order.FileName.Substring(2, 3) == "CAP")
                                                                        {
                                                                            outputFolder = "Advantage\\Captive";
                                                                            order.ChkName = "Advantage Personal Checks";
                                                                        }
                                                                        else if (order.FileName.Substring(11, 1) == "A" && order.FileName.Substring(2, 3) == "SEC")
                                                                        {
                                                                            outputFolder = "Advantage\\SecurForms";
                                                                            order.ChkName = "Advantage Personal Checks";
                                                                        }
                                                                        else if (order.FileName.Substring(11, 1) == "S" && order.FileName.Substring(2, 3) == "SEC")
                                                                        {
                                                                            outputFolder = "Starter_Checks\\SecurForms";
                                                                            order.ChkName = "Starter Check Personal";
                                                                        }
                                                                        else if (order.FileName.Substring(11, 1) == "S" && order.FileName.Substring(2, 3) == "CAP")
                                                                        {
                                                                            outputFolder = "Starter_Checks\\Captive";
                                                                            order.ChkName = "Starter Check Personal";
                                                                        }
                                                                        else if (order.FileName.Substring(11, 1) == "R")
                                                                        {
                                                                            outputFolder = "Regular Checks";
                                                                            order.ChkName = "Regular Personal Checks";

                                                                        }
                                                                        order.PcsPerbook = "50";
                                                                    }

                                                                        //errorMessage += ProcessServices.CheckInBranches(order.BRSTN, order.FileName);
                                                                        order.deliveryDate = deliveryDate;
                                                                        //checking if the branches is existing in database

                                                                        order.StartingSerial = startsSN.ToString();
                                                                        if (order.ChkType == "A")

                                                                            endSN = startsSN + 49;
                                                                        else if (order.ChkType == "B")
                                                                            endSN = startsSN + 99;
                                                                        order.EndingSerial = endSN.ToString();
                                                                        order.BranchName = listofbranch.Address1;
                                                                        order.Address2 = listofbranch.Address2;
                                                                        order.Address3 = listofbranch.Address3;
                                                                        order.Address4 = listofbranch.Address4;
                                                                        order.Address5 = listofbranch.Address5;
                                                                        order.Address6 = listofbranch.Address6;
                                                                        order.BranchCode = listofbranch.BranchCode;
                                                                        order.BaeStock = listofbranch.BaeStock;
                                                                        order.Company = listofbranch.Company;

                                                                        if (order.AccountName2 == null)
                                                                            order.AccountName2 = " ";

                                                                        //   listofbranch.Reg_LastNo = Int64.Parse(order.EndingSerial);
                                                                        //  con.UpdateRef(listofbranch);

                                                                        //   order.Quantity = 1;
                                                                        orderList.Add(order);
                                                                        startsSN = endSN + 1;
                                                                     
                                                                    }
                                                               
                                                            }
                                                            if (Cont.Contains("A"))
                                                                listofbranch.Adv_LastNo = endSN;
                                                            else
                                                                listofbranch.Reg_LastNo = endSN;
                                                            updateBranch.Add(listofbranch);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    // process.WriteOrderFile(orderList);
                                }
                            }
                            else if (Extension == "XLS" || Extension == "XLSX")
                            {
                                // string _fileName = "";
                                for (int i = 0; i < list.Length; i++)
                                {

                                    // _fileName = Path.GetFileName(list[i]);
                                    Excel.Application xlApp = new Excel.Application();
                                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                                    int SheetsCount = xlWorkbook.Sheets.Count;
                                    for (int b = 0; b < SheetsCount; b++)
                                    {
                                        Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[b + 1];
                                        Excel.Range xlRange = xlWorksheet.UsedRange;

                                        int rowCount = xlRange.Rows.Count;
                                        int colCount = xlRange.Columns.Count;
                                        string SheetName = xlWorksheet.Name.ToUpper();
                                       // int rowCounter = 0;
                                        //string rbBrstn;
                                        //for (int d = 0; d < rowCount - 4; d++)
                                        //{
                                        //  BranchModelRb bRb = new BranchModelRb();
                                        //  int row = 5;
                                        //  con.GetBranchByBRSTNRb(branchRb);
                                        //List<BranchModelRb> rbBranchList = new List<BranchModelRb>();
                                        //for (int z = 0; z < branchRb.Count; z++)
                                        //{
                                        //    rbBranchList.Add(branchRb[z]);
                                        //}
                                        //rbBrstn = xlRange.Cells[rowCounter + 5, 8].Text;

                                        for (int c = 0; c < rowCount - 4; c++)
                                        {
                                            //    while (rbBranchList[rowCounter].BRSTN != rbBrstn)
                                            //    {
                                            //        startsSN = rbBranchList[rowCounter].LastNo + 1;

                                            //        rowCounter++;
                                            //    }
                                            string accountname = "";
                                             
                                            //startsSN = listofbranch.LastNo + 1;
                                            //check.StartingSerial = startsSN.ToString();
                                            int qty = int.Parse(xlRange.Cells[c + 5, 1].Text);
                                            for (int a = 0; a < qty; a++)// adding to List and multiply by quantity order
                                            {
                                                OrderModelRb check = new OrderModelRb();
                                             
                                                check.BRSTN = xlRange.Cells[c + 5, 8].Text;
                                                    check.ChkName = xlRange.Cells[c + 5, 3].Text;
                                                    check.BankName = xlRange.Cells[c + 5, 5].Text;
                                                    check.AccountNo = xlRange.Cells[c + 5, 7].Text;
                                                    accountname = xlRange.Cells[c + 5, 10].Text;
                                                    check.AccountNoRb = xlRange.Cells[c + 5, 9].Text;
                                                 accountname.Trim();
                                                //  check.Quantity = Int64.Parse(xlRange.Cells[rowCounter + 5, 1].Text);
                                             
                                                if (accountname.Length > 30)
                                                {
                                                    int LoopCount5 = accountname.Length;
                                                    // For OR
                                                    while (LoopCount5 > 0)
                                                    {
                                                        if (check.AccountName == "" && check.AccountName2 == "" && LoopCount5 < accountname.Length - 5)
                                                        {

                                                            if (accountname.Substring(LoopCount5, 4) == " Or " || accountname.Substring(LoopCount5, 4) == " or ")
                                                            {
                                                                check.AccountName = accountname.Substring(0, LoopCount5 + 3);
                                                                check.AccountName2 = accountname.Substring(LoopCount5 + 4, accountname.Length - LoopCount5 - 4);
                                                            }
                                                        }
                                                        LoopCount5 = LoopCount5 - 1;
                                                    }
                                                    // For OR/&
                                                    LoopCount5 = accountname.Length;
                                                    while (LoopCount5 > 0)
                                                    {
                                                        if (check.AccountName == "" && check.AccountName2 == "" && LoopCount5 < accountname.Length - 5)
                                                        {

                                                            if (accountname.Substring(LoopCount5, 4) == "&/OR" || accountname.Substring(LoopCount5, 4) == "&/OR ")
                                                            {
                                                                check.AccountName = accountname.Substring(0, LoopCount5 + 4);
                                                                check.AccountName2 = accountname.Substring(LoopCount5 + 4, accountname.Length - LoopCount5 - 4);
                                                            }
                                                        }
                                                        LoopCount5 = LoopCount5 - 1;
                                                    }
                                                }
                                                else
                                                {
                                                    check.AccountName = accountname;
                                                    check.AccountName2 = "";
                                                }

                                                    if (check.BankName.Contains("BINAN"))
                                                    {
                                                        check.BankName = "Imus_Rural_Bank";
                                                        outputFolder = "Imus_Rural_Bank";
                                                    check.FileName = "Imus_Rural_Bank";
                                                    }
                                                    else if (check.BankName.Contains("ANGELES"))
                                                    {
                                                        check.BankName = "Rural_Bank_of_Angeles";
                                                        outputFolder = "Rural_Bank_of_Angeles";
                                                  
                                                }
                                                    else if (check.BankName.Contains("CARDONA"))
                                                    {
                                                        check.BankName = "Rural_Bank_of_Cardona";
                                                        outputFolder = "Rural_Bank_of_Cardona";
                                                  
                                                }
                                                    else if (check.BankName.Contains("DULAG"))
                                                    {
                                                        check.BankName = "Rural_Bank_of_Dulag";
                                                        outputFolder = "Rural_Bank_of_Dulag";
                                                   
                                                }
                                                    else if (check.BankName.Contains("MABUHAY"))
                                                    {
                                                        check.BankName = "Banko_Mabuhay";
                                                        outputFolder = "Banko_Mabuhay";
                                                 
                                                }
                                                    else if (check.BankName.Contains("MASUWERTE"))
                                                    {
                                                        check.BankName = "Masuwerte";
                                                        outputFolder = "Masuwerte";
                                                   
                                                }
                                                    else if (check.BankName.Contains("ASPAC"))
                                                    {
                                                        check.BankName = "Aspac_Rural";
                                                        outputFolder = "Aspac_Rural";
                                                   
                                                }
                                                    else if (check.BankName.Contains("KAWIT"))
                                                    {
                                                        check.BankName = "Rural_Bank_of_Kawit";
                                                        outputFolder = "Rural_Bank_of_Kawit";
                                                  
                                                }
                                                    else if (check.BankName.Contains("MEXICO"))
                                                    {
                                                        check.BankName = "Rural_Bank_of_Mexico";
                                                        outputFolder = "Rural_Bank_of_Mexico";
                                                  
                                                }
                                                    else if (check.BankName.Contains("PORAC"))
                                                    {
                                                        check.BankName = "Rural_Bank_of_Porac";
                                                        outputFolder = "Rural_Bank_of_Porac";
                                                 
                                                }
                                                    else if (check.BankName.Contains("SALINAS"))
                                                    {
                                                        check.BankName = "Rural_Bank_of_Salinas";
                                                        outputFolder = "Rural_Bank_of_Salinas";
                                                   
                                                }
                                                var listofbranch = branchRb.FirstOrDefault(r => r.BRSTN == check.BRSTN);
                                                check.BranchName =listofbranch.Address1;
                                                check.Address2 = listofbranch.Address2;
                                                check.Address3 = listofbranch.Address3;
                                                check.Address4 = listofbranch.Address4;
                                                check.Address5 = listofbranch.Address5;
                                                check.Address6 = listofbranch.Address6;


                                                    if (check.ChkName.Contains("Personal"))
                                                    {
                                                        check.ChkType = "A";
                                                        check.PcsPerbook = "50";
                                                    }

                                                    else
                                                    {
                                                        check.ChkType = "B";
                                                        check.PcsPerbook = "100";
                                                    }
                                                 //  check.StartingSerial = startsSN.ToString();
                                                   // endSN = startsSN + (Int64.Parse(check.PcsPerbook) - 1);

                                                 //  check.EndingSerial = endSN.ToString();

                                                    orderListRb.Add(check);
                                                    //  listofbranch.LastNo = endSN;
                                                     
                                                //    rbBranchList[rowCounter].LastNo = endSN;
                                                 //   startsSN = endSN + 1;
                                                    //srowCounter = 0;
                                                //  con.UpdateRefRb(listofbranch);
                                            }
                                                // row++;
                                               // rowCounter++;
                                           
                                               
                                                                           
                                            }
                                        //}
                                    }

                                }

                              //  MessageBox.Show("Hellow World!" + orderListRb[0].AccountName);
                            }

                        }
                        if (errorMessage == "")
                        {
                            //toolStripProgressBar.Visible = false;

                            BindingSource checkBind = new BindingSource();
                            checkBind.DataSource = orderListRb;
                            dataGridView1.DataSource = checkBind;
                            lblTotal.Text = orderListRb.Count.ToString();
                            MessageBox.Show("No Errors Found", "System Message");

                            generateToolStripMenuItem.Enabled = true;

                            checkToolStripMenuItem.Enabled = false;
                    
                        }
                        else
                            MessageBox.Show(errorMessage, "System Error");
                    }
                }
            }
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Int64 endingserial = 0;
          //  ZipfileServices zip = new ZipfileServices();
           // var listofchecks = orderList.Select(r => r.BRSTN).ToList();

            if (orderList != null)
            {

              
                process.DoBlockProcess(orderList, this, outputFolder);
                process.PackingText(orderList, this,outputFolder);
                process.PrinterFile(orderList, this, outputFolder);
                process.SaveToPackingDBF(orderList, batchfile, this, outputFolder);
                for (int i = 0; i < orderList.Count; i++)
                {

                    con.SavedDatatoDatabase(orderList[i], batchfile);
                }

                for (int f = 0; f < updateBranch.Count; f++)//Updating Serial
                {
                    con.UpdateRef(updateBranch[f]);
                }
            }

            if(orderListRb != null)
            {
                List<OrderModelRb> listofRb = new List<OrderModelRb>();
                for (int i = 0; i < orderListRb.Count; i++)
                {
                    //startsSN = Int64.Parse(orderListRb[i].StartingSerial);
                    //for (int a = 0; a < orderListRb[i].Quantity; a++)
                    //{
                    //    //     OrderModelRb rb = new OrderModelRb();
                     
                    //    if (orderListRb[i].ChkType == "B")
                    //        endSN = startsSN + 99;
                    //    else
                    //        endSN = startsSN + 49;

                       
                        
                    //    listofRb.Add(orderListRb[i]);
                    //    listofRb[i].StartingSerial = startsSN.ToString();
                    //     listofRb[i].EndingSerial = endSN.ToString();
                    //    startsSN = endSN + 1;
                       // orderListRb[i].StartingSerial = (endSN + 1).ToString();
                   // }
                 //   orderListRb[i].EndingSerial = ((Int64.Parse(orderListRb[i].StartingSerial) + (Int64.Parse(orderListRb[i].PcsPerbook) * orderListRb[i].Quantity)) - 1).ToString();

                }
                
                process.Process(orderListRb,this);

                //for (int i = 0; i < orderListRb.Count; i++)
                //{
                //    con.SavedDatatoDatabaseRB(orderListRb[i], batchfile,deliveryDate);
                //}

               // orderListRb.Distinct();
                z.ZipFileRb(frmLogIn.userName, this,orderListRb);
            }
            
           
         //con.UpdateRef()
           // z.ZipFileS("Elmer", this);
           // z.CopyZipFile("Elmer", this);
            //ZipfileServices.CopyPacking("Elmer", this);
            MessageBox.Show("Done!");
            Environment.Exit(0);
        }

        private void encodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Encode frmencode = new Encode();
            frmencode.Show();
            this.Hide();
        }
    }
}
