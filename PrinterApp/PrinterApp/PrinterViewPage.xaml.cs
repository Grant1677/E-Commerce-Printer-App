using System;
using Xamarin.Forms;
using PrinterApp.Model;
using SQLite;
using System.Linq;

namespace PrinterApp
{
    public partial class PrinterViewPage : ContentPage
	{

        private readonly Printer print;
        public PrinterViewPage (Printer p)
		{       
            InitializeComponent ();
            getData(p);
            print = p;
            myButton.Clicked += AddToCartButton;
        }

        private void getData (Printer printer)
        {
            PrinterHeaderInfo.Text = printer.ManName + " - " +
                                     printer.ModelNum + " (" +
                                     printer.Category + ")";

            PrinterPrice.Text = "$" + printer.Price.ToString();

            PrinterQuantity.Text = printer.Quantity.ToString() + " left in stock";

            PrinterDescription.Text = "Information: " + printer.Description;

        }

        async void AddToCartButton(object sender, EventArgs e)
        {
            if (print != null && print.Quantity > 0)
            {
                print.UpdateQuantity();
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
                {
                    CartItems item = new CartItems();
                    item.CartID = App.currUser.CartID;
                    item.PrinterID = print.PrinterID;
                    connection.CreateTable<CartItems>();
                    connection.Insert(item);
                    
                }
                await Navigation.PushAsync(new ViewCartPage());
            }
            else
            {
                await DisplayAlert("Error", "Out of stock", "Ok");
            }
        }

        async void ViewCart(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ViewCartPage());
        }
    }
}

