using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class ResetPassword : Form
    {
        private User _user;
        private readonly Car_RentalEntities _db;
        public ResetPassword(User user)
        {
            InitializeComponent();
            _db = new Car_RentalEntities();
            _user = user;
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                var password = tbNewPassword.Text;
                var confirmPassword = tbConfirmPassword.Text;
                var user = _db.Users.FirstOrDefault(q => q.id == _user.id);
                if (password != confirmPassword)
                {
                    MessageBox.Show("Password do not match.Please try again!");

                }
                user.password = Util.HashPassword(password);
                _db.SaveChanges();
                MessageBox.Show("Password was reset successfully");
            }
            catch (Exception)
            {
                MessageBox.Show("An error has occured.Please try again");
            }
        }
    }
}
