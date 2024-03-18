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
    public partial class ManageVehicleListing : Form
    {
        private readonly Car_RentalEntities _db;
        public ManageVehicleListing()
        {
            InitializeComponent();
            _db = new Car_RentalEntities(); 
        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            try 
            {
                var cars = _db.TypesOfCars.Select(x => new { Make = x.Make, Model = x.Model, VIN = x.VIN, year = x.Year, LicensePlateNumber = x.LicensePlateNumber, Id = x.Id }).ToList();
                gvVehicleList.DataSource = cars;
                gvVehicleList.Columns[4].HeaderText = "License Plate Number";
                gvVehicleList.Columns[5].Visible = false;
                //gvVehicleList.Columns[0].HeaderText = "ID";
                //gvVehicleList.Columns[1].HeaderText = "NAME";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            };
            
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            var addEditVehicle = new AddEditVehicle(this);//this keyword means the name of the class i am currently in ,that is manageVehicleListing class
              addEditVehicle.ShowDialog();
            addEditVehicle.MdiParent = this.MdiParent;
          
          
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try 
            {
                //get Id of selecred row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;
                //query database for record
                var car = _db.TypesOfCars.FirstOrDefault(w => w.Id == id);

                //launch AddEditVehicle window with data
                var addEditVehicle = new AddEditVehicle(car,this)
                {
                    MdiParent = this.MdiParent
                };
                addEditVehicle.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            };
            
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                //get Id of selecred row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;
                //query database for record
                var car = _db.TypesOfCars.FirstOrDefault(w => w.Id == id);

                DialogResult dr = MessageBox.Show("Are You Sure You Want To Delete This Record?", "Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
               if (dr == DialogResult.Yes)
                {
                    //delete vehicle for record
                    _db.TypesOfCars.Remove(car);
                    _db.SaveChanges();
                }  
               PopulateGrid();
     } 
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            }
           
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Simple Refrs Option
            PopulateGrid();
        }
        public void PopulateGrid() 
        {
            //Select a custom model colllection of cars from database
            var cars = _db.TypesOfCars.Select(d => new
            {
                Make = d.Make,
                Model = d.Model,
                VIN = d.VIN,
                Year = d.Year,
                LicensePlateNumber = d.LicensePlateNumber,
                d.Id
            }).ToList();
            gvVehicleList.DataSource=cars;
            gvVehicleList.Columns[4].HeaderText = " License Plate Number";
            //Hiding column for Id.Changing from hard coded value to the name,to make it more dynamic
            gvVehicleList.Columns["Id"].Visible = false;
        }
    }
}
