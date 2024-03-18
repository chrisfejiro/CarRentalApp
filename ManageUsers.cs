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
    public partial class ManageUsers : Form
    {
        private readonly Car_RentalEntities _db;
        public ManageUsers()
        {
            InitializeComponent();
            _db=new Car_RentalEntities();   
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            if (Utils.FormIsOpen("AddUser"))
            {
                var addUser = new AddUser(this);
                addUser.MdiParent = this.MdiParent;
                addUser.Show();
            }
        

        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                //get Id of selecred row
                var id = (int)gvUserList.SelectedRows[0].Cells["id"].Value;
                //query database for record
                var user = _db.Users.FirstOrDefault(w => w.id == id);
                var hashed_password = Util.DefaultHashedPassword();
                user.password = hashed_password;
                _db.SaveChanges();

                MessageBox.Show($"{user.username}'s Password has been reset!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            };
        }

        private void btnDeactivatePassword_Click(object sender, EventArgs e)
        {
            try
            {
                //get Id of selecred row
                var id = (int)gvUserList.SelectedRows[0].Cells["id"].Value;
                //query database for record
                var user = _db.Users.FirstOrDefault(w => w.id == id);

                user.IsActive = user.IsActive == true ? false : true;
                _db.SaveChanges();

                MessageBox.Show($"{user.username}'s active status has changed ");
                PopulateGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            };
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        public void PopulateGrid()
        {
            var users = _db.Users.Select(q =>new
            {
                q.id,
                q.username,
                q.UserRoles.FirstOrDefault().Role.Name,
                q.IsActive,
            }).ToList();
            gvUserList.DataSource = users;
            gvUserList.Columns["username"].HeaderText = "Username";
            gvUserList.Columns["name"].HeaderText = "Role Name";
            gvUserList.Columns["isActive"].HeaderText = "Active";
            gvUserList.Columns["id"].Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }
    }
}
