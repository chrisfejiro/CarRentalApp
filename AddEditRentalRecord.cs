using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddEditRentalRecord : Form
    {
        private readonly bool isEditMode;
        private readonly Car_RentalEntities _db;
        public AddEditRentalRecord()
        {
            InitializeComponent();
             lblTitle.Text = "Add New Rental";
            this.Text = "Add new Rental";
            isEditMode = false;
            _db = new Car_RentalEntities();

        }
        public AddEditRentalRecord(CarRentalRecord recordToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Rental Record";
            this.Text = "Edit Rental Record";
            if (recordToEdit == null)
            {
                MessageBox.Show("Please ensure that you selected a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new Car_RentalEntities();
                PopulateFields(recordToEdit);
            }  
        }

        private void PopulateFields(CarRentalRecord recordToEdit)
        {
            tbCustomerName.Text = recordToEdit.CustomerName;
              DateRented.Value = (DateTime)recordToEdit.DateRented;
            DateReturned.Value = (DateTime)recordToEdit.DateReturned;
            CostText.Text = recordToEdit.Cost.ToString();
            lblRecordId.Text =  recordToEdit.Id.ToString();
         }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = tbCustomerName.Text;
                var dateOut = DateRented.Value;
                var dateIn = DateReturned.Value;
                var carType = cbTypeOfCar.Text; //or cbTypeOfCar.SelectedItem.ToString()
                double cost = Convert.ToDouble(CostText.Text);
                var isValid = true;
                var errorMessage = string.Empty;

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(carType))
                {

                    isValid = false;
                    errorMessage += "Error: Please enter missing data ";
                }
                if (dateOut > dateIn)
                {
                    isValid = false;
                    errorMessage += "Error: Illegal date selection\n\r";
                }

                if (isValid)
                {
                    //Declare an object of the record to be added
                    var rentalRecord = new CarRentalRecord();
                    if (isEditMode)
                    {
                        //if in edit mode,then get the ID and retrieve the record from the database and place
                        //the result in the record object
                        var Id = int.Parse(lblRecordId.Text);
                        rentalRecord = _db.CarRentalRecords.FirstOrDefault(q => q.Id == Id);
                    }
                    //Populate record object with values from the form
                    rentalRecord.CustomerName = customerName;
                    rentalRecord.DateRented = dateOut;
                    rentalRecord.DateReturned = dateIn;
                    rentalRecord.Cost = (decimal)cost;
                    rentalRecord.TypeOfCarId = (int)cbTypeOfCar.SelectedValue;
                    //If not in edit mode,then add the record object to the database

                    if (!isEditMode)
                        _db.CarRentalRecords.Add(rentalRecord);

                    //Save changes made to the entity
                    _db.SaveChanges();

                    MessageBox.Show($"Customer Name: {customerName}\n\r"
                   + $"Date Rented : {dateOut}"
                   + $"Date Returned: {dateIn}\n\r"
                    + $"Cost : {cost}\n\r"
                   + $"Car Type: {carType}\n\r"
                   + $"THANK YOU FOR YOUR BUSINESS");
                    Close();

                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                //throw  ,this ends the program when there is an  error
            }
           
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            var cars = _db.TypesOfCars.Select(q => new { Id=q.Id, Name=q.Make + " " + q.Model}).ToList();
            cbTypeOfCar.DataSource = cars;//it is telling the form it should take its data source from cars
            cbTypeOfCar.DisplayMember = "Name";//what the form will display
            cbTypeOfCar.ValueMember = "Id";//it is telling the form that the type of car should be valued by Id 
           
        }

       
    }
}
