using System;
using System.Collections.Generic;
using SQLite;
using PrinterApp.Model;
using Xamarin.Forms;

namespace PrinterApp
{	
	public partial class PrinterMenuPage : ContentPage
	{
        public PrinterMenuPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Printer>();
                var printers = connection.Table<Printer>().ToList();
                printerCollectionView.ItemsSource = printers;
            }
            

        }

        async void AddPrinter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrinterInfoPage());
        }

        async void RemovePrinter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrinterRemovalPage());
        }

        async void EditPrinter(object sender, EventArgs e)
        {
            string action = await DisplayPromptAsync("Enter a model number to edit", "Model Num: ");
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Printer>();
                var selectedPrinter = connection.Table<Printer>().Where(i => i.ModelNum == action);
                var convertedPrinter = selectedPrinter.ToList();
                if(convertedPrinter.Count > 0)
                {
                    await Navigation.PushAsync(new PrinterEditPage(convertedPrinter[0]));
                }
                else
                {
                    await DisplayAlert("Failure", "Did not find printer model number", "Ok");
                }
                
            }
        }

    }
}

