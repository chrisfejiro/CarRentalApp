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
    public partial class ManageRentalRecord : Form
    {
        private readonly Car_RentalEntities _db;
        public ManageRentalRecord()
        {
            InitializeComponent();
            _db = new Car_RentalEntities();
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddEditRentalRecord();
            {
                MdiParent = this.MdiParent;
            };
            addRentalRecord.Show();

        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {

            try
            {
                //get Id of selecred row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;
                //query database for record
                var record = _db.CarRentalRecords.FirstOrDefault(w => w.Id == id);

                //launch AddEditVehicle window with data
                var addEditRentalRecord = new AddEditRentalRecord(record)
                {
                    MdiParent = this.MdiParent
                };
                addEditRentalRecord.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            };

        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                //get Id of selecred row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;
                //query database for record
                var record = _db.CarRentalRecords.FirstOrDefault(w => w.Id == id);
                //delete vehicle for record
                _db.CarRentalRecords.Remove(record);
                _db.SaveChanges();
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void ManageRentalRecord_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateGrid();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error:{ex.Message}");
            }
        }

        private void PopulateGrid()
        {
            var records = _db.CarRentalRecords.Select(u => new
            {
                Customer=u.CustomerName,
                DateOut=u.DateRented,
                DateIn=u.DateReturned,
                Id=u.Id,
                Cost=u.Cost,    
                Car=u.TypesOfCar.Make + " " + u.TypesOfCar.Model,
            }).ToList();

            gvRecordList.DataSource = records;
            gvRecordList.Columns["DateIn"].HeaderText = "Date In";
            gvRecordList.Columns["DateOut"].HeaderText = "Date Out";
            gvRecordList.Columns["Id"].Visible = false;
        }

       
    }
}
