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
        ProcessServices process = new ProcessServices();
        List<OrderModel> orderList = new List<OrderModel>();
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
            if (Directory.GetFiles(Application.StartupPath + "\\Head\\").Length == 0) // if the path folder is empty
                MessageBox.Show("No files found in directory path", "***System Error***");
            else
            {
                string[] list = Directory.GetFiles(Application.StartupPath + "\\Head\\");

      

                if (list != null)
                {
                   


                    for (int i = 0; i < list.Length; i++)
                    {

                        //reading and storing details from the order files
                     
                        string[] lines = File.ReadAllLines(Application.StartupPath + "\\Head\\" + Path.GetFileNameWithoutExtension(list[i]) + ".txt");
                        for (int b = 0; b < lines.Length; b++)
                        {

                            OrderModel order = new OrderModel();

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
                                
                                order.BRSTN = lines[b].Substring(1, 9);
                                order.ChkType = lines[b].Substring(0, 1);
                                order.AccountNo = lines[b].Substring(10, 12);
                                order.AccountName = lines[b].Substring(23, 35);
                            
                                order.Quantity = int.Parse(lines[b].Substring(81, 2));
                              

                            //}
                            //}
                            if (order.ChkType == "B")
                                order.PcsPerbook = "100";
                            else
                                order.PcsPerbook = "50";
                            order.Extension = Path.GetExtension(list[i]);
                            order.FileName = Path.GetFileNameWithoutExtension(list[i]);
                          //  errorMessage += ProcessServices.CheckInBranches(order.BRSTN, order.FileName);
                            
                            //checking if the branches is existing in database
                            var listofbranch = branch.FirstOrDefault(r => r.BRSTN == order.BRSTN);
                            order.StartingSerial = listofbranch.Reg_LastNo.ToString();

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
                            orderList.Add(order);

                        }


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

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Int64 endingserial = 0;
            var listofchecks = orderList.Select(r => r.BRSTN).ToList();
            for (int i = 0; i < orderList.Count; i++)
            {
                orderList[i].EndingSerial = (Int64.Parse(orderList[i].StartingSerial) +(Int64.Parse(orderList[i].PcsPerbook) * orderList[i].Quantity)).ToString();

            }

            //List<OrderModel> dprocess = new List<OrderModel>();
           
            process.PackingText(orderList, this);
            process.DoBlockProcess(orderList, this);
            process.PrinterFile(orderList, this);
            process.SaveToPackingDBF(orderList,batchfile ,this);
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
