using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using New_AUB.Models;
using New_AUB.Services;

namespace New_AUB
{
    public partial class Encode : Form
    {
        public Encode()
        {
            InitializeComponent();
            cmbBranch.Text = "  ------------------------Select Branch-----------------------";
            dateTime = dateTimePicker1.MinDate = DateTime.Now; //Disable selection of backdated dates to prevent errors 
        }
        BranchModel branch = new BranchModel();
        List<BranchModel> branchList = new List<BranchModel>();
        List<BranchModelRb> branchList2 = new List<BranchModelRb>();
        DbConServices con = new DbConServices();
        List<ManualOrderModel> orderList = new List<ManualOrderModel>();
        ProcessServices proc = new ProcessServices();
        ZipfileServices zip = new ZipfileServices();
        public string batchfile = "";
        public DateTime deliveryDate;
        DateTime dateTime;
        public static string outputfolder = "";
        string chkType = "";
        Int64 endSR = 0;
        Int64 startSN = 0;
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtAccountName_TextChanged(object sender, EventArgs e)
        {
            txtAccountName.CharacterCasing = CharacterCasing.Upper;
        }

        private void Encode_Load(object sender, EventArgs e)
        {
            txtAccountNo.MaxLength = 12;
            txtAccountName.MaxLength = 50;
            txtAccountName2.MaxLength = 50;
            LoadChkType();
            dgvColumns();
        }
        private void LoadChkType()
        {
            cmbChkType.Items.Add("Manager's Check");
            cmbChkType.Items.Add("Manager's Check (Smart)");
            cmbChkType.Items.Add("Manager's Check (Continues)");
            cmbChkType.Items.Add("Manager's Check (Sheeted)");
            cmbChkType.Items.Add("Gift Check");
            cmbChkType.Items.Add("Time Deposit - Peso");
            cmbChkType.Items.Add("Time Deposit - Dollar");
            cmbChkType.Items.Add("Charge Slip");
        }
        private void dgvColumns()
        {
            dgvOrderList.Columns.Add("BRSTN", "BRSTN");
            dgvOrderList.Columns.Add("AccountNo", "AccountNo");
            dgvOrderList.Columns.Add("AccountName", "Account Name");
            dgvOrderList.Columns.Add("AccountName2", "Account Name 2");
            dgvOrderList.Columns.Add("ChkType", "Type");
            dgvOrderList.Columns.Add("ChequeName", "Cheque Name");
            dgvOrderList.Columns.Add("Quantity", "Quantity");
            dgvOrderList.Columns.Add("PcsPerBook", "PcsPerBook");

        }

        private void txtBrstn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBrstn_Enter(object sender, EventArgs e)
        {

        }

        private void txtBrstn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
           // con.GetBranchByBRSTN(branch, txtBrstn.Text);
            //lblBRSTN.Text = branch.Address1;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int qty = int.Parse(txtQty.Text);
            
            // con.GetBranchByBRSTN(branch, txtBrstn.Text);
            con.GetAllBranches(branchList);
            
           
         //   order.Quantity = Int64.Parse(txtQty.Text);
            var listofbranch = branchList.FirstOrDefault(r => r.BRSTN == txtBrstn.Text);
            startSN = listofbranch.MC_LastNo + 1;
            // startSN = listofbranch.MCS_LastNo + 1;
            string chkType = cmbChkType.Text;
            for (int a = 0; a < qty; a++)
            {
                ManualOrderModel order = new ManualOrderModel();
                order.BRSTN = txtBrstn.Text;
                order.ChkName = chkType;
                if (order.ChkName == "Manager's Check")
                {
                    order.ChkName = "Manager''s Check";
                    order.ChkType = "MC";

                    order.StartingSerial = startSN.ToString();
                }
                else if (order.ChkName == "Manager's Check (Smart)")
                {
                    order.ChkName = "Manager''s Check (Smart)";
                    order.ChkType = "MCS";

                    order.StartingSerial = startSN.ToString();
                }
                else if (order.ChkName == "Manager's Check (Continues)")
                {

                    order.ChkName = "Manager''s Check (Continues)";
                    order.ChkType = "MC_CONT";
                    //startSN = listofbranch.MC_LastNo + 1;
                    order.StartingSerial = startSN.ToString();
                }

                order.AccountNo = txtAccountNo.Text;
                order.AccountName = txtAccountName.Text;
                order.AccountName2 = txtAccountName2.Text;

                order.PcsPerbook = "50";
                order.Quantity = 1;

                //for (int i = 0; i < branchList.Count; i++)
                //{

                //}
                order.BranchName = listofbranch.Address1;
                order.Address2 = listofbranch.Address2;
                order.Address3 = listofbranch.Address3;
                order.Address4 = listofbranch.Address4;


                batchfile = txtBatch.Text;
                 endSR = Int64.Parse(order.StartingSerial) + 49;
                order.EndingSerial = endSR.ToString();
                startSN = endSR + 1;
                dgvOrderList.Rows.Add(order.BRSTN, order.AccountNo, order.AccountName, order.AccountName2, order.ChkType, order.ChkName, order.Quantity, order.PcsPerbook);

                orderList.Add(order);
               
            }
            ClearAllInputText();

        }

        private void lblPcsPerbook_Click(object sender, EventArgs e)
        {

        }

        private void cmbChkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChkType.Text == "Manager's Check")
            {
                lblPcsPerbook.Text = "50 Pcs/Book";
                chkType = "MC";
              
                cmbBranch.Items.Clear();
                cmbBranch.Text = "  ------------------------Select Branch-----------------------";
                con.GetAllBranches(branchList);
             
                for (int i = 0; i < branchList.Count; i++)
                {
                    cmbBranch.Items.Add(branchList[i].Address1);
                  
                }
                outputfolder = "Managers_Checks";
                
                    
            }
            else if (cmbChkType.Text == "Manager's Check (Smart)")
            {
                lblPcsPerbook.Text = "50 Pcs/Book";
                chkType = "MCS";
              
                cmbBranch.Items.Clear();
                cmbBranch.Text = "  ------------------------Select Branch-----------------------";
                con.GetAllBranches(branchList);
              //  cmbChkType.Text = "Manager''s Check (Smart)";
                for (int i = 0; i < branchList.Count; i++)
                {
                    cmbBranch.Items.Add(branchList[i].Address1);

                }
                outputfolder = "Managers_Checks\\Smart";
            }
            else if (cmbChkType.Text == "Manager's Check (Continues)")
            {
                lblPcsPerbook.Text = "50 Pcs/Book";
           
                txtBrstn.Text = "011020011";
                cmbBranch.Items.Clear();
                cmbBranch.Text = "  ------------------------Select Branch-----------------------";
                cmbBranch.Items.Add("REMEDIAL UNIT");
                cmbBranch.Items.Add("HUMAN RESOURCE GROUP");
                cmbBranch.Items.Add("ACCOUNTING DEPARTMENT");

                con.GetAllBranchesRB(branchList2);
             
                outputfolder = "Managers_Checks\\Continues";
               
            }
          
            else if (cmbChkType.Text == "Charge Slip")
            {
                lblPcsPerbook.Text = "50 Pcs/Book";
               
                cmbBranch.Items.Clear();
                cmbBranch.Refresh();
                outputfolder = "Charge Slip";

            }
            else if (cmbChkType.Text == "Time Deposit - Peso")
            {
                lblPcsPerbook.Text = "50 Pcs/Book";
               
                cmbBranch.Items.Clear();
                cmbBranch.Refresh();
                outputfolder = "Time_Deposit\\Peso";

            }
            else if (cmbChkType.Text == "Time Deposit - Dollar")
            {
                lblPcsPerbook.Text = "50 Pcs/Book";
               
                cmbBranch.Items.Clear();
                cmbBranch.Refresh();
                outputfolder = "Time_Deposit\\Dollar";
            }

        }
        private void ClearAllInputText()
        {
            //cmbBranch.Text = "";
            cmbChkType.Text = "";
            txtAccountName.Text = "";
            txtAccountName2.Text = "";
            txtAccountNo.Text = "";
            txtQty.Text = "";
            //_ChkType = "";
           // _Brstn = "";
        }

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {

            proc.DoBlockProcessM(orderList,this,outputfolder);
           
            proc.PackingTextM(orderList, this, outputfolder);
            proc.PrinterFileM(orderList, this, outputfolder);
            proc.SaveToPackingDBFM(orderList, batchfile, this, outputfolder);
            for (int i = 0; i < orderList.Count; i++)
            {
                con.SavedDatatoDatabaseM(orderList[i], batchfile,deliveryDate);
            }
            zip.ZipFileM(frmLogIn.userName, this,orderList);

            MessageBox.Show("Done!!");
            Environment.Exit(0); 
        }

        private void cmbChkType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if(e.KeyChar == (char)Keys.Back)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  cmbBranch.Items.Clear();
          
            for (int i = 0; i < branchList.Count; i++)
            {
                if (cmbBranch.Text == branchList[i].Address1)
                {
                    txtBrstn.Text = branchList[i].BRSTN;
                    if(chkType == "MC")
                    lblLastNo.Text = branchList[i].MC_LastNo.ToString();
                    else if(chkType == "MCS")
                    lblLastNo.Text = branchList[i].MCS_LastNo.ToString();
                    txtAccountNo.Text = branchList[i].AccountNo;
                }
                
            }
           

             if (cmbBranch.Text == "REMEDIAL UNIT")
            {
                txtAccountNo.Text = "001070000021";
                var listofbranch = branchList2.FirstOrDefault(p => p.AccountNo == txtAccountNo.Text);
               // txtAccountNo.Text = listofbranch.AccountNo;
                   startSN = listofbranch.LastNo;
                    lblLastNo.Text = startSN.ToString();
            }
            else if (cmbBranch.Text == "HUMAN RESOURCE GROUP")
            {
                txtAccountNo.Text = "001070000047";
                var listofbranch = branchList2.FirstOrDefault(p => p.AccountNo == txtAccountNo.Text);
                txtAccountNo.Text = listofbranch.AccountNo;

                startSN = listofbranch.LastNo;
                lblLastNo.Text = startSN.ToString();
            }
            else if (cmbBranch.Text == "ACCOUNTING DEPARTMENT")
            {
                txtAccountNo.Text = "001070000034";
                var listofbranch = branchList2.FirstOrDefault(p => p.AccountNo == txtAccountNo.Text);
                txtAccountNo.Text = listofbranch.AccountNo;
                startSN = listofbranch.LastNo;
                lblLastNo.Text = startSN.ToString();
            }

        }

        private void lblBRSTN_Click(object sender, EventArgs e)
        {

        }

        private void txtAccountName2_TextChanged(object sender, EventArgs e)
        {
            txtAccountName2.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtAccountNo_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void cmbBranch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }
    }
}
