using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SQLite;
using PrinterApp.Model;

namespace PrinterApp
{
    public partial class PrinterRemovalPage : ContentPage
    {
        public PrinterRemovalPage()
        {
            InitializeComponent();
        }

        public async void ConfirmRemoval(object sender, EventArgs e)
        {
            var printer = new Printer
            {
                ManName = manNameEntry.Text,
                ModelNum = modelNumEntry.Text
            };

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Printer>();
                var selectedPrinter = connection.Table<Printer>().Where(i => i.ManName == printer.ManName && i.ModelNum == printer.ModelNum);
                var convertedPrinter = selectedPrinter.ToList();

                if (convertedPrinter.Count > 0)
                {
                    string action = await DisplayActionSheet("Confirm Removal: " + convertedPrinter[0].ModelNum, "Cancel", null, "Confirm");
                    if (action == "Confirm")
                    {
                        int rows = connection.Delete(convertedPrinter[0]);
                        if(rows > 0)
                        {
                            await DisplayAlert("Success", "Printer deleted.", "Ok");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Failure", "Printer not deleted!", "Ok");
                        }
                    }
                    
                }
                else
                {
                    await DisplayAlert("Failure", "Printer not found!", "Ok");
                    manNameEntry.Text = string.Empty;
                    modelNumEntry.Text = string.Empty;
                }


            }
        }

        public async void CancelAction(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrinterMenuPage());
        }

    }
}

