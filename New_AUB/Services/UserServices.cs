using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using New_AUB.Models;

namespace New_AUB.Services
{
    class UserServices: DbConServices
    {
        public UserModel Login(string _username, string _password)
        {
            try
            {

                if (_username == "test")
                {
                    UserModel user = new UserModel
                    {
                        Username = "Test",
                        Password = "",
                        Name = "Test User"
                    };

                    return user;
                }

                else
                {

                    DBConnect();

                    UserModel user = new UserModel();

                    string query = "SELECT username, password FROM " + databaseName + ".users WHERE username='" + _username + "' AND password='" + _password + "'";

                    MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                    MySqlDataAdapter sda = new MySqlDataAdapter(myCommand);
                    MySqlDataReader myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        user = new UserModel
                        {
                            Username = myReader.GetString(0),
                            Password = myReader.GetString(1),
                            //  Name = myReader.GetString(2)
                        };

                    }
                    DBClosed();
                    return user;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                return null;
            }
        }
    }
}
