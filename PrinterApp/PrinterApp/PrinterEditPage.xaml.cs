using System;
using PrinterApp.Model;
using SQLite;
using Xamarin.Forms;

namespace PrinterApp
{	
	public partial class PrinterEditPage : ContentPage
	{
        private readonly Printer currPrinter;

        public PrinterEditPage (Printer printer)
		{
			currPrinter = printer;
			InitializeComponent ();
			manNameEntry.Text = currPrinter.ManName;
			modelNumEntry.Text = currPrinter.ModelNum;
			quantityEntry.Text = (currPrinter.Quantity).ToString();
			descriptionEntry.Text = currPrinter.Description;
			priceEntry.Text = (currPrinter.Price).ToString();
			categoryEntry.Text = currPrinter.Category;
			typeEntry.Text = currPrinter.Type;
			isColorEntry.Text = (currPrinter.IsColor).ToString();

		}

        async void SubmitEdit(object sender, EventArgs e)
        {
            currPrinter.Quantity = int.Parse(quantityEntry.Text);
            currPrinter.Description = descriptionEntry.Text;
            currPrinter.Price = float.Parse(priceEntry.Text);
            currPrinter.Category = categoryEntry.Text;
            currPrinter.Type = typeEntry.Text;
            currPrinter.IsColor = bool.Parse(isColorEntry.Text);

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Printer>();
                
                int rows = connection.Update(currPrinter);

                if (rows > 0)
                {
                    await DisplayAlert("Success", "Printer edited.", "Ok");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Failure", "Printer not deleted!", "Ok");
                }
            }
        }
    }
}