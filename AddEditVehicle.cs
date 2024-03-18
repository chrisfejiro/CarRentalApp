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
    public partial class AddEditVehicle : Form
    {
        private readonly bool isEditMode;
        private ManageVehicleListing _manageVehicleListing;
        private readonly Car_RentalEntities _db;
        public AddEditVehicle(ManageVehicleListing manageVehicleListing=null)
        {
            InitializeComponent();
            lblTitle.Text = "Add New Vehicle";
            this.Text = "AA new Vehicle";
            isEditMode = false;
            _manageVehicleListing = manageVehicleListing;
            _db = new Car_RentalEntities();
        }

        public AddEditVehicle(TypesOfCar carToEdit, ManageVehicleListing manageVehicleListing=null)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle";
            _manageVehicleListing = manageVehicleListing;
            this.Text = "Edit Vehicle";
            if (carToEdit == null ) 
            {
                MessageBox.Show("Please ensure that you selected a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new Car_RentalEntities();
                PopulateFields(carToEdit);
            }
        }

        private void PopulateFields(TypesOfCar car)
        {
            try
            {
                lblId.Text = car.Id.ToString();
                tbMake.Text = car.Make;
                tbModel.Text = car.Model;
                tbVIN.Text = car.VIN;
                tbYear.Text = car.Year.ToString();
                tbLicensed.Text = car.LicensePlateNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            };
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try 
            {
                if(string.IsNullOrWhiteSpace(tbMake.Text)|| string.IsNullOrWhiteSpace(tbModel.Text))
                {
                    MessageBox.Show("Please ensure that you provide a make and a model");
                }
                else
                {
                    if (isEditMode)
                    {
                        //Edit Code here
                        var id = int.Parse(lblId.Text);
                        var car = _db.TypesOfCars.FirstOrDefault(x => x.Id == id);
                        car.Model = tbModel.Text;
                        car.VIN = tbVIN.Text;
                        car.Make = tbMake.Text;
                        car.Year = int.Parse(tbYear.Text);
                        car.LicensePlateNumber = tbLicensed.Text;
                    }
                    else
                    {
                        //Added validation for make and model of cars being added
                        //Add code here
                        var newCar = new TypesOfCar
                        {
                            LicensePlateNumber = tbLicensed.Text,
                            Make = tbMake.Text,
                            Model = tbModel.Text,
                            VIN = tbVIN.Text,
                            Year = int.Parse(tbYear.Text),
                        };
                        _db.TypesOfCars.Add(newCar);
                    }
                    _db.SaveChanges();
                    _manageVehicleListing.PopulateGrid();
                    MessageBox.Show(" Operation Completed.Refresh Grid To See Changes");
                    Close();
                }
             } 
            catch (Exception ex) 
            {
                MessageBox.Show($"Error : {ex.Message}");
            };
          

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
