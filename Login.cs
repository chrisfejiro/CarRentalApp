using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class Login : Form
    {
        private readonly Car_RentalEntities _db;
        public Login()
        {
            InitializeComponent();
            _db=new Car_RentalEntities();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SHA256 sha = SHA256.Create();

                var username = tbUsername.Text.Trim();
                var password = tbPassword.Text;
                var hashPassword=Util.HashPassword(password);    

                //Check for matching username,password and active flag
                var user= _db.Users.FirstOrDefault(q=>q.username == username && q.password==hashPassword && q.IsActive==true);
                if(user == null)
                {
                    MessageBox.Show("Please provide valid credentials");
                }
                else
                {
                     var mainWindow = new MainWindow(this,user);  
                    mainWindow.Show();  
                    Hide();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Something went wrong .Please try again");
            }
        }
    }
}
