using System;
using PrinterApp.Model;
using System.Collections.Generic;

using Xamarin.Forms;
using SQLite;

namespace PrinterApp
{	
	public partial class PrinterInfoPage : ContentPage
	{
        public PrinterInfoPage()
        {
            InitializeComponent();
        }

        async void AddPrinterToDatabase(object sender, EventArgs e)
        {
            Printer printer = new Printer
            {
                ManName = manNameEntry.Text,
                ModelNum = modelNumEntry.Text,
                Quantity = int.Parse(quantityEntry.Text),
                Description = descriptionEntry.Text,
                Price = float.Parse(priceEntry.Text),
                Category = categoryEntry.Text,
                Type = typeEntry.Text,
                IsColor = bool.Parse(isColorEntry.Text)

            };

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                //Create a new table of Printer
                connection.CreateTable<Printer>();

                //Insert Printer information into Printer table
                int rows = connection.Insert(printer);

                if (rows > 0)
                {
                    await DisplayAlert("Success", "Printer information inserted!", "Ok");
                    await Navigation.PopAsync();
                }

                else
                    await DisplayAlert("Failure", "Printer information NOT inserted!", "Ok");
            }
        }
    }
}