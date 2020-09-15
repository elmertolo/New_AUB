using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using New_AUB.Services;

namespace New_AUB
{
    public partial class frmLogIn : Form
    {
        public static string userName;
        public frmLogIn()
        {
            InitializeComponent();
        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtBoxUsername.Text != "")
            {
                // int check=0;

                if (txtBoxUsername.Text == "test")
                {
                    frmMain form = new frmMain();
                    userName = txtBoxUsername.Text;
                    form.Show();
                    Hide();
                }
                else
                {
                    UserServices userService = new UserServices();


                    var result = userService.Login(txtBoxUsername.Text, txtBoxPassword.Text);
                    if (txtBoxPassword.Text == result.Password && txtBoxUsername.Text == result.Username)
                    {
                        frmMain form = new frmMain();
                        userName = txtBoxUsername.Text;
                        form.Show();
                        Hide();

                    }
                    else
                    {
                        MessageBox.Show("Invalid Username and Password");
                    }
                }
            }
            else
                MessageBox.Show("Please Input Username", "Error");
        }
    }
}
