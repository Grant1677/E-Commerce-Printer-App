using System;
using System.Collections.Generic;
using System.Reflection;
using PrinterApp.Model;
using SQLite;
using Xamarin.Forms;

namespace PrinterApp
{	
	public partial class ViewCartPage : ContentPage
	{	
		public ViewCartPage ()
		{
			InitializeComponent ();
            
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<CartItems>();
                var items = connection.Table<CartItems>().Where(i => i.CartID == App.currUser.CartID);
                var convertedItems = items.ToList();
                var printerList = new List<Printer>();

                foreach(var item in convertedItems)
                {
                    var printer = connection.Table<Printer>().Where(i => i.PrinterID == item.PrinterID);
                    var convertedPrinter = printer.ToList();

                    if (convertedPrinter.Count > 0)
                    {
                        printerList.Add(convertedPrinter[0]);
                    }
                    
                    
                }

                itemsCollectionView.ItemsSource = printerList;

                var cart = connection.Table<Cart>().Where(i => i.CartID == App.currUser.CartID);
                var convertedCart = cart.ToList();
                double totalCost = convertedCart[0].GetTotalCost();
                cartTotal.Text = "Total: $" + totalCost.ToString();

            }
        }


        public async void CheckoutButton(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new CheckoutPage());
        }

        async void itemsCollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                Printer printer = e.CurrentSelection[0] as Printer;
                
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
                {
                    var item = connection.Table<CartItems>().Where(i => i.CartID == App.currUser.CartID && i.PrinterID == printer.PrinterID);
                    var convertedItem = item.ToList();
                    
                    string action = await DisplayActionSheet("Confirm Removal: " + printer.ModelNum, "Cancel", null, "Confirm");
                    if (action == "Confirm")
                    {
                        connection.Delete(convertedItem[0]);
                        OnAppearing();
                    }

                }
            }
        }
    }
}

