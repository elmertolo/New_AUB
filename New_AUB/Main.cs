using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using New_AUB.Models;
using New_AUB.Services;
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
        List<BranchModelRb> branchRb = new List<BranchModelRb>();
        ProcessServices process = new ProcessServices();
        List<OrderModel> orderList = new List<OrderModel>();
        List<OrderModel> orderList2 = new List<OrderModel>();
        List<OrderModelRb> orderListRb = new List<OrderModelRb>();
        public frmMain()
        {

            InitializeComponent();
            dateTime = dateTimePicker1.MinDate = DateTime.Now; //Disable selection of backdated dates to prevent errors  
        }
        //  OrderModel order = new OrderModel();
        //string[] FileName;
        //string[] file;
        //string[] Extension;
        //string[] BRSTN;
        //string[] AccountNo;
        //string[] AccountName;
        //string[] AccountName2;
        //string[] Quantity;
        //string[] ChkType;
        //int orderCounter = 0;
        
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
            string errorMessage = "";
            batchfile = txtBatch.Text;
            con.GetAllBranches(branch);// get all details in branch database
            con.GetAllBranchesRB(branchRb);// get all details in branch database
            deliveryDate = dateTimePicker1.Value;
            if(deliveryDate == dateTime)
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

                        if (Extension == "TXT")
                        {
                            if (list != null)
                            {



                                for (int i = 0; i < list.Length; i++)
                                {

                                    //reading and storing details from the order files

                                    string[] lines = File.ReadAllLines(Application.StartupPath + "\\Head\\" + Path.GetFileNameWithoutExtension(list[i]) + ".txt");
                                   
                                  for (int b = 0; b < lines.Length; b++)
                                  {  OrderModel order = new OrderModel();

                                        if (lines[b].Substring(78, 1) == "2")
                                        {

                                            orderList[b-1].AccountName2 = lines[b].Substring(22, 35);
                                           //  b++;
                                         
                                        }



                                        //if (lines[b].Substring(78, 1) == "2")
                                        //{

                                        //    orderList[b - 1].AccountName2 = lines[b].Substring(22, 35);
                                        //    //order.BRSTN = lines[b].Substring(1, 9);
                                        //    //order.ChkType = lines[b].Substring(0, 1);
                                        //    //order.AccountNo = lines[b].Substring(10, 12);
                                        //    //order.AccountName = lines[b].Substring(23, 35);
                                        //    //order.Quantity = int.Parse(lines[b].Substring(81, 2));
                                        //}
                                        //else
                                        //{

                                        //else
                                        //{
                                            //for (int r = 0; r < int.Parse(lines[b].Substring(81, 2)); r++)
                                            //{
                                              
                                                //var sort = (from c in orderList
                                                //            orderby c.BRSTN, c.AccountNo
                                                //            ascending
                                                //            select c).ToList();

                                                order.BRSTN = lines[b].Substring(1, 9);
                                                order.ChkType = lines[b].Substring(0, 1);
                                                order.AccountNo = lines[b].Substring(10, 12);
                                                order.AccountName = lines[b].Substring(22, 35);

                                                order.Quantity = 1;




                                                if (order.ChkType == "B")
                                                {
                                                    order.ChkName = "Regular Commercial Checks";
                                                    order.PcsPerbook = "100";
                                                }
                                                else

                                                {
                                                    order.ChkName = "Regular Commercial Checks";
                                                    order.PcsPerbook = "50";
                                                }
                                                order.Extension = Path.GetExtension(list[i]);
                                                order.FileName = Path.GetFileNameWithoutExtension(list[i]);
                                                  //errorMessage += ProcessServices.CheckInBranches(order.BRSTN, order.FileName);
                                                order.deliveryDate = deliveryDate;
                                                //checking if the branches is existing in database

                                                var listofbranch = branch.FirstOrDefault(sr => sr.BRSTN == order.BRSTN);
                                                Int64 LastNo = listofbranch.Reg_LastNo;
                                                 order.StartingSerial = (LastNo + 1).ToString();

                                                 order.EndingSerial = (listofbranch.Reg_LastNo + Int64.Parse(order.PcsPerbook)).ToString();

                                               
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

                                                
                                                //   order.Quantity = 1;
                                                orderList.Add(order);
                                                listofbranch.Reg_LastNo = Int64.Parse(order.EndingSerial);
                                                con.UpdateRef(listofbranch);

                                      
                                        //  }

                                        //}

                                        //} 
                                        //if()
                                        //{
                                        //    MessageBox.Show(orderList[b].AccountNo);
                                        //}

                                    }
                                }
                                process.WriteOrderFile(orderList);
                            }
                        }
                        else if (Extension == "XLS" || Extension == "XLSX")
                        {
                           // string _fileName = "";
                            for (int i = 0; i < list.Length; i++)
                            {

                               // _fileName = Path.GetFileName(list[i]);
                                Excel.Application xlApp = new Excel.Application();
                                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(FileName);

                                int SheetsCount = xlWorkbook.Sheets.Count;
                                for (int b = 0; b < SheetsCount; b++)
                                {
                                    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[b + 1];
                                    Excel.Range xlRange = xlWorksheet.UsedRange;

                                    int rowCount = xlRange.Rows.Count;
                                    int colCount = xlRange.Columns.Count;
                                    string SheetName = xlWorksheet.Name.ToUpper();

                                   
                                  //  int row = 5;

                                    for (int c = 0; c < rowCount-4; c++)
                                    {
                                        OrderModelRb check = new OrderModelRb();
                                        check.BRSTN = xlRange.Cells[c+5, 8].Text;
                                        check.ChkName = xlRange.Cells[c + 5, 3].Text;
                                        check.AccountNo = xlRange.Cells[c + 5, 7].Text;
                                        check.AccountName = xlRange.Cells[c + 5, 10].Text;
                                        check.AccountNoRb = xlRange.Cells[c + 5, 9].Text;
                                        check.Quantity = Int64.Parse(xlRange.Cells[c + 5, 1].Text);
                                        var listofbranch = branchRb.FirstOrDefault(r => r.BRSTN == check.BRSTN);
                                        check.BranchName = listofbranch.Address1;
                                        check.Address2 = listofbranch.Address2;
                                        check.Address3 = listofbranch.Address3;
                                        check.StartingSerial = (listofbranch.LastNo + 1).ToString();


                                        if(check.ChkName.Contains("Personal"))
                                        {
                                            check.ChkType = "A";
                                            check.PcsPerbook = "50";
                                        }

                                        else
                                        {
                                            check.ChkType = "B";
                                            check.PcsPerbook = "100";
                                        }
                                        orderListRb.Add(check);
                                       // row++;
                                    }

                                   
                                    
                                }

                            }
                            
                            MessageBox.Show("Hellow World!" + orderListRb[0].AccountName);
                        }

                    }
                        if (errorMessage == "")
                    {
                        //toolStripProgressBar.Visible = false;

                        BindingSource checkBind = new BindingSource();
                        checkBind.DataSource = orderList;
                        dataGridView1.DataSource = checkBind;
                        MessageBox.Show("No Errors Found", "System Message");

                        generateToolStripMenuItem.Enabled = true;

                        checkToolStripMenuItem.Enabled = false;
                    }
                    else
                        MessageBox.Show(errorMessage, "System Error");
                 }
                 
            }
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Int64 endingserial = 0;
            var listofchecks = orderList.Select(r => r.BRSTN).ToList();

           

            

            //List<OrderModel> dprocess = new List<OrderModel>();
            if (orderList != null)
            {

                for (int i = 0; i < orderList.Count; i++)
                {
                    //for (int b = 0; b < orderList[i].Quantity; b++)
                    //{
                        orderList[i].EndingSerial = (Int64.Parse(orderList[i].StartingSerial) + Int64.Parse(orderList[i].PcsPerbook)).ToString();
                        // orderList[i].StartingSerial = (Int64.Parse(orderList[i].EndingSerial) + 1).ToString();
                   // }
                }

                process.PackingText(orderList, this);
                process.DoBlockProcess(orderList, this);
                process.PrinterFile(orderList, this);
                process.SaveToPackingDBF(orderList, batchfile, this);
            }
            if(orderListRb != null)
            {
                for (int i = 0; i < orderListRb.Count; i++)
                {
                    orderListRb[i].EndingSerial = (Int64.Parse(orderListRb[i].StartingSerial) + (Int64.Parse(orderListRb[i].PcsPerbook) * orderListRb[i].Quantity)).ToString();

                }
                process.DoBlockProcessRB(orderListRb,this);
                process.PackingTextRB(orderListRb, this);
                process.PrinterFileRb(orderListRb, this);
                process.SaveToPackingDBFRb(orderListRb,batchfile, this);
            }
            for (int i = 0; i < orderList.Count; i++)
            {
                
                con.SavedDatatoDatabase(orderList[i], batchfile);
            }
            MessageBox.Show("Done!");
        }

        private void encodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Encode frmencode = new Encode();
            frmencode.Show();
            this.Hide();
        }
    }
}
