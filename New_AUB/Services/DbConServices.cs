using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using New_AUB.Models;
using System.Diagnostics;
using System.IO;

namespace New_AUB.Services
{
    class DbConServices
    {
        public MySqlConnection myConnect;
       // private int serial = 1;
        public string databaseName = "";
        public void DBConnect()
        {
            try
            {
                string DBConnection = "";

                //if (frmLogIn.userName == "test")
                //{
                    DBConnection = "datasource=localhost;port=3306;username=root;password=corpcaptive; convert zero datetime=True;";

                    databaseName = "captive_database";
                    //MessageBox.Show(databaseName);
                //}
                //else
                //{
                //    //  DBConnection = "";
                //    DBConnection = "datasource=192.168.0.254;port=3306;username=root;password=CorpCaptive; convert zero datetime=True;";
                //    // MessageBox.Show("HELLO");
                //    databaseName = "captive_database";
                //    // MessageBox.Show(databaseName);

                //}


                myConnect = new MySqlConnection(DBConnection);

                myConnect.Open();

            }
            catch (Exception Error)
            {

                MessageBox.Show(Error.Message, "System Error");
            }
        }// end of function

        public void DBClosed()
        {
            myConnect.Close();
        }
        // end of function
        public List<BranchModel> GetAllBranches(List<BranchModel> _branches)
        {
            DBConnect();
            string sql = "Select  BRSTN,Address1,Address2,Address3,Address4,Address5,Address6,Company,BranchCode,BaeStock, Reg_LastNo, Adv_LastNo,AccountNo,MC_LastNo,MCS_LastNo,Con_LastNo,CV_LastNo from " +databaseName+".aub_branches";
            //List<BranchModel> Branches = new List<BranchModel>();

            MySqlCommand myCommand = new MySqlCommand(sql, myConnect);

            MySqlDataReader myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                BranchModel branch = new BranchModel();

                branch.BRSTN = myReader.GetString(0);

                
                branch.Address1 = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";

                branch.Address2 = !myReader.IsDBNull(2) ? myReader.GetString(2) : "";

                branch.Address3 = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";

                branch.Address4 = !myReader.IsDBNull(4) ? myReader.GetString(4) : "";

                branch.Address5 = !myReader.IsDBNull(5) ? myReader.GetString(5) : "";
                branch.Address6 = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";
                branch.Company = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                branch.BranchCode = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                branch.BaeStock = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                branch.Reg_LastNo = !myReader.IsDBNull(10) ? myReader.GetInt64(10) :0;
                branch.Adv_LastNo = !myReader.IsDBNull(11) ? myReader.GetInt64(11) : 0;
                branch.AccountNo = !myReader.IsDBNull(12) ? myReader.GetString(12):"";
                branch.MC_LastNo = !myReader.IsDBNull(13) ? myReader.GetInt64(13) : 0;
                branch.MCS_LastNo = !myReader.IsDBNull(14) ? myReader.GetInt64(14) : 0;
                branch.Con_LastNo = !myReader.IsDBNull(15) ? myReader.GetInt64(15) : 0;
                branch.CV_LastNo = !myReader.IsDBNull(16) ? myReader.GetInt64(16) : 0;
                //  branch.Binan_LastNo = !myReader.IsDBNull(11) ? myReader.GetInt64(11) : 0;


                _branches.Add(branch);
            }//END OF WHILE
            DBClosed();

            return _branches;

        }
        public List<BranchModelRb> GetAllBranchesRB(List<BranchModelRb> _branches)
        {
            DBConnect();
            string sql = "Select * from " + databaseName + ".aub_rb_branches";
            //List<BranchModel> Branches = new List<BranchModel>();

            MySqlCommand myCommand = new MySqlCommand(sql, myConnect);

            MySqlDataReader myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                BranchModelRb branch = new BranchModelRb();

                branch.BRSTN = myReader.GetString(0);


                branch.Address1 = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";

                branch.Address2 = !myReader.IsDBNull(2) ? myReader.GetString(2) : "";

                branch.Address3 = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";

                branch.Address4 = !myReader.IsDBNull(4) ? myReader.GetString(4) : "";

                branch.Address5 = !myReader.IsDBNull(5) ? myReader.GetString(5) : "";
                branch.Address6 = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";
                branch.AccountNo = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                branch.LastNo = !myReader.IsDBNull(8) ? myReader.GetInt64(8) : 0;
               // branch.BranchCode = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                //branch.BaeStock = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
               // branch.Reg_LastNo = !myReader.IsDBNull(10) ? myReader.GetInt64(10) : 0;

              //  branch.Binan_LastNo = !myReader.IsDBNull(11) ? myReader.GetInt64(11) : 0;


                _branches.Add(branch);
            }//END OF WHILE
            DBClosed();

            return _branches;

        }

        public OrderModel SavedDatatoDatabase(OrderModel _check, string _batch)
        {
            if (_check.BRSTN == null)
            {

            }
            else
            {
                string sql = "INSERT INTO captive_database.aub_history(Date,Time,DeliveryDate,ChkType,ChequeName,BRSTN,AccountNo,Name1,Name2,Address1,BranchCode,Address2,Address3,Batch,StartingSerial, EndingSerial)VALUES(" +

                            "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                            "'" + _check.deliveryDate.ToString("yyyy-MM-dd") + "'," +
                            "'" + _check.ChkType + "'," +
                            "'" + _check.ChkName + "'," +
                            "'" + _check.BRSTN + "'," +
                            "'" + _check.AccountNo + "'," +
                            "'" + _check.AccountName.Replace("'", "''") + "'," +
                            "'" + _check.AccountName2.Replace("'", "''") + "'," +
                            "'" + _check.BranchName.Replace("'", "''") + "'," +
                            "'" + _check.BranchCode + "'," +
                            "'" + _check.Address2.Replace("'", "''") + "'," +
                            "'" + _check.Address3.Replace("'", "''") + "'," +
                          //  "'" + _check.Address4.Replace("'", "''") + "'," +
                         //   "'" + _check.Address5.Replace("'", "''") + "'," +
                          //  "'" + _check.Address6.Replace("'", "''") + "'," +
                            "'" + _batch + "'," +
                            "'" + _check.StartingSerial + "'," +
                            "'" + _check.EndingSerial + "')";



                DBConnect();
                MySqlCommand myCommand = new MySqlCommand(sql, myConnect);

                myCommand.ExecuteNonQuery();
                DBClosed();
                
            }
            return _check;
        }// end of function
        public OrderModelRb SavedDatatoDatabaseRB(OrderModelRb _check, string _batch,DateTime _deliveryDate)
        {
            if (_check.BRSTN == null)
            {

            }
            else
            {
                string sql = "INSERT INTO captive_database.aub_history(Date,Time,DeliveryDate,ChkType,ChequeName,BRSTN,AccountNo,Name1,Name2,Address1,BranchCode,Address2,Address3,Address4,Address5,Address6,Batch,StartingSerial, EndingSerial)VALUES(" +

                            "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                            "'" + _deliveryDate.ToString("yyyy-MM-dd") + "'," +
                            "'" + _check.ChkType + "'," +
                            "'" + _check.ChkName + "'," +
                            "'" + _check.BRSTN + "'," +
                            "'" + _check.AccountNo + "'," +
                            "'" + _check.AccountName.Replace("'", "''") + "'," +
                            "'" + _check.AccountName2.Replace("'", "''") + "'," +
                            "'" + _check.BranchName.Replace("'", "''") + "'," +
                            "'" + _check.BranchCode + "'," +
                            "'" + _check.Address2.Replace("'", "''") + "'," +
                            "'" + _check.Address3.Replace("'", "''") + "'," +
                              "'" + _check.Address4.Replace("'", "''") + "'," +
                               "'" + _check.Address5.Replace("'", "''") + "'," +
                              "'" + _check.Address6.Replace("'", "''") + "'," +
                            "'" + _batch + "'," +
                            "'" + _check.StartingSerial + "'," +
                            "'" + _check.EndingSerial + "')";



                DBConnect();
                MySqlCommand myCommand = new MySqlCommand(sql, myConnect);

                myCommand.ExecuteNonQuery();
                DBClosed();

            }
            return _check;
        }// end of function
        public BranchModel UpdateRef(BranchModel _ref)
        {
            DBConnect();
            string sql = "Update captive_database.aub_branches SET Reg_LastNo = '" + _ref.Reg_LastNo + "',Con_LastNo ='"+_ref.Con_LastNo+"'" +
                ",CV_LastNo ='"+_ref.CV_LastNo+"',Adv_LastNo ='"+_ref.Adv_LastNo+"',MC_LastNo ='"+_ref.MC_LastNo +"',MCS_LastNo = '"+_ref.MCS_LastNo+"', ModifiedDate = '" + _ref.Date.ToString("yyyy-MM-dd") + "' where BRSTN = '" + _ref.BRSTN  + "'";
            MySqlCommand cmd = new MySqlCommand(sql, myConnect);

            MySqlDataReader myReader = cmd.ExecuteReader();
            DBClosed();
            return _ref;

        }// end of function
        public BranchModelRb UpdateRefRb(BranchModelRb _ref)
        {
            DBConnect();
            string sql = "Update captive_database.aub_rb_branches SET LastNo = '" + _ref.LastNo +"' ,LastDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' where BRSTN = '" + _ref.BRSTN + "'";
            MySqlCommand cmd = new MySqlCommand(sql, myConnect);

            MySqlDataReader myReader = cmd.ExecuteReader();
            DBClosed();
            return _ref;

        }// end of function
        public BranchModel GetBranchByBRSTN(BranchModel _model, string _brstn)
        {
            DBConnect();
            string sql = "SELECT BRSTN,Address1,Address2,Address3,Address4    from " + databaseName + ".aub_branches where BRSTN ='" + _brstn + "';";
            //List<BranchModel> Branches = new List<BranchModel>();

            MySqlCommand myCommand = new MySqlCommand(sql, myConnect);

            MySqlDataReader myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
               

                // branch.DRNumber = myReader.GetString(0);

                _model.BRSTN = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                _model.Address1 = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                _model.Address2 = !myReader.IsDBNull(2) ? myReader.GetString(2) : "";
                _model.Address3 = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";
                _model.Address4 = !myReader.IsDBNull(4) ? myReader.GetString(4) : "";
                //branch.Date = !myReader.IsDBNull(11) ? myReader.GetDateTime(11) : DateTime.Today;
                //  branch.Binan_LastNo = !myReader.IsDBNull(11) ? myReader.GetInt64(11) : 0;


                // _model.Add(branch);
            }//END OF WHILE
            DBClosed();

            return _model;

        }
        public BranchModelRb GetBranchByBRSTNRb(BranchModelRb _model, string _brstn)
        {
            DBConnect();
            string sql = "SELECT BRSTN,Address1,Address2,Address3,LastNo FROM captive_database.aub_rb_branches  where BRSTN ='" + _brstn + "';";
            //List<BranchModel> Branches = new List<BranchModel>();

            MySqlCommand myCommand = new MySqlCommand(sql, myConnect);

            MySqlDataReader myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {

                BranchModelRb branch = new BranchModelRb();
                // branch.DRNumber = myReader.GetString(0);

                _model.BRSTN = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                _model.Address1 = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                _model.Address2 = !myReader.IsDBNull(2) ? myReader.GetString(2) : "";
                _model.Address3 = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";
                // branch.Address4 = !myReader.IsDBNull(4) ? myReader.GetString(4) : "";
                //branch.Date = !myReader.IsDBNull(11) ? myReader.GetDateTime(11) : DateTime.Today;
                _model.LastNo = !myReader.IsDBNull(4) ? myReader.GetInt64(4) : 0;


             //   _model.Add(branch);
            }//END OF WHILE
            DBClosed();

            return _model;

        }
        public List<MySqlLocatorModel> GetMySQLLocations()
        {
            MySqlConnection connect = new MySqlConnection("datasource=192.168.0.254 ;port=3306;username=root;password=CorpCaptive");

            connect.Open();

            MySqlCommand myCommand = new MySqlCommand("SELECT * FROM captive_database.mysqldump_location", connect);

            MySqlDataReader myReader = myCommand.ExecuteReader();

            List<MySqlLocatorModel> sqlLocator = new List<MySqlLocatorModel>();

            while (myReader.Read())
            {
                MySqlLocatorModel myLocator = new MySqlLocatorModel
                {
                    PrimaryKey = myReader.GetInt32(0),
                    Location = myReader.GetString(1)
                };

                sqlLocator.Add(myLocator);
            }

            connect.Close();

            return sqlLocator;
        }//end of Function
        public void DumpMySQL()
        {
            string dbname = frmMain.banks+ "_branches";
            string outputFolder = Application.StartupPath + @"\Output\" + frmMain.outputFolder;
            Process proc = new Process();

            proc.StartInfo.FileName = "cmd.exe";

            proc.StartInfo.UseShellExecute = false;

            proc.StartInfo.WorkingDirectory = GetMySqlPath().ToUpper().Replace("MYSQLDUMP.EXE", "");

            proc.StartInfo.RedirectStandardInput = true;

            proc.StartInfo.RedirectStandardOutput = true;

            proc.Start();

            StreamWriter myStreamWriter = proc.StandardInput;

            string temp = "mysqldump.exe --user=root --password=CorpCaptive --host=192.168.0.254 captive_database " + dbname + " > " +
                outputFolder + "\\" + DateTime.Today.ToShortDateString().Replace("/", ".") + "-" + dbname + ".SQL";

            myStreamWriter.WriteLine(temp);

            dbname = "aub_history";

            temp = "mysqldump.exe --user=root --password=password=CorpCaptive --host=192.168.0.254 captive_database " + dbname + " > " +
                 outputFolder + "\\" + DateTime.Today.ToShortDateString().Replace("/", ".") + "-" + dbname + ".SQL";

            myStreamWriter.WriteLine(temp);

            myStreamWriter.Close();

            proc.WaitForExit();

            proc.Close();
        }//end of Function
        public string GetMySqlPath()
        {
            var mySQLocator = GetMySQLLocations();

            foreach (var loc in mySQLocator)
            {
                if (File.Exists(loc.Location))
                    return loc.Location;
            }

            return "";
        } //end of Function

        
    }
}
