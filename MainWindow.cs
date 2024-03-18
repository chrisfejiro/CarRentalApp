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
    public partial class MainWindow : Form
    {
        private readonly Login _login;
        public string _RoleName;
        public User _user;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(Login login,User user)
        {
            InitializeComponent();
            _RoleName = user.UserRoles.FirstOrDefault().Role.shortname;
            _user = user;   
            _login = login;
        }

        private void addRentalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddEditRentalRecord"))
            {
                var addRentalRecords = new AddEditRentalRecord();
                addRentalRecords.MdiParent = this;
                addRentalRecords.Show();
            }
        }

        private void manageVehicleListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////going to be checking all the open forms in the applications,whether they are opened
            //var OpenForms = Application.OpenForms.Cast<Form>();//giving a list of the data the form
            //var isOpen = OpenForms.Any(q => q.Name == "ManageVehicleListing");

            if (!Utils.FormIsOpen("ManageVehicleListing"))
            {
                var vehicleListing = new ManageVehicleListing();
                vehicleListing.MdiParent = this;
                vehicleListing.Show();
            }

          
        }

        private void viewArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("ManageRentalRecords"))
            {
                var manageRentalRecord = new ManageRentalRecord
                {
                    MdiParent = this,
                };
                manageRentalRecord.Show();
            }
          
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _login.Close();
        }

        private void lblManageUsers_Click(object sender, EventArgs e)
        {

            if (!Utils.FormIsOpen("ManageUsers"))
            {
                var manageUser = new ManageUsers()
                {
                    MdiParent = this,
                };
                manageUser.Show();
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (_user.password == Util.DefaultHashedPassword())
            {
                var resetPassword = new ResetPassword(_user);
                resetPassword.ShowDialog();
            }
            var username = _user.username;
            tsiLoginText.Text = $"Logged in as {username}";
            if (_RoleName !="admin")
            {
                lblManageUsers.Visible = false;

            }
        }
    }  
}
    

