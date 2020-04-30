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


namespace New_AUB
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        //  OrderModel order = new OrderModel();
        string[] FileName;
        string[] file;
        string[] Extension;
        string[] BRSTN;
        string[] AccountNo;
        string[] AccountName;
        string[] AccountName2;
        string[] Quantity;
        string[] ChkType;
        int orderCounter = 0;
        List<OrderModel> orderList = new List<OrderModel>();
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
            string[] list = Directory.GetFiles(Application.StartupPath + "\\Head\\");
            //Getting Filename and Extension
            if (list != null)
            {



                for (int i = 0; i < list.Length; i++)
                {
                   
                    //reading and storing details from the order files
                    string[] lines = File.ReadAllLines(Application.StartupPath + "\\Head\\" + Path.GetFileNameWithoutExtension(list[i]) + ".txt");
                    for (int b = 0; b < lines.Length; b++)
                    {

                        OrderModel order = new OrderModel();

                        if (lines[b].Substring(78, 1) == "2")
                        {

                            order.AccountName2 = lines[b].Substring(22, 35);

                        }
                        else
                        {
                            order.BRSTN = lines[b].Substring(1, 9);
                            order.ChkType = lines[b].Substring(0, 1);
                            order.AccountNo = lines[b].Substring(10, 12);
                            order.AccountName = lines[b].Substring(23, 35);
                            order.Quantity = Int64.Parse(lines[b].Substring(81, 2));
                            //                            MessageBox.Show(lines[b].Substring(78, 1));

                        }
                        if (order.ChkType == "B")
                            order.PcsPerbook = "100";
                        else
                            order.PcsPerbook = "50";
                        order.Extension = Path.GetExtension(list[i]);
                        order.FileName = Path.GetFileNameWithoutExtension(list[i]);
                        orderList.Add(order);

                        //MessageBox.Show(orderList[b].AccountName);
                    }

                 
                    //Extension[i] = Path.GetExtension(list[i]);
                    //FileName[i] = Path.GetFileNameWithoutExtension(list[i]);
                    //file[i] = Path.GetFileName(list[i]);
                    //orderCounter++;
                }
            }
            else
                MessageBox.Show("BOBO!!!!");


            //end of getting filename and extension
            //getting details in each line per order file
            // string[] lines = File.ReadAllLines(Application.StartupPath + "\\Head\\" + FileName[0] + ".txt");
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    //if (lines[i].Length == 84)
            //    //{
            //    //    continue;

            //    //}
            //    //else
            //    //{

            //        /*ChkType[i] = */lines[i].Substring(0, 1);
            //        //BRSTN[i] = lines[i].Substring(1, 9);
            //       // AccountNo[i] = lines[i].Substring(10, 11);
            //   // }
            //}

            //       File.WriteAllLines("D:\\Order.txt", lines);
            //for (int i = 0; i < orderCounter; i++)
            //{
            //    OrderModel order = new OrderModel();
            //    if (orderCounter != 0)
            //    {
            //       order.FileName = FileName[i];
            //        order.Extension = Extension[i];
            //        orderList.Add(order);
            //    }
            //}

            //MessageBox.Show(orderList[0].AccountName);
            BindingSource checkBind = new BindingSource();
            checkBind.DataSource = orderList;
            dataGridView1.DataSource = checkBind;
            MessageBox.Show("Done!");
        }
    }
}
