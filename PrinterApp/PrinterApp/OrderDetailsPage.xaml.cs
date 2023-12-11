using System;
using System.Collections.Generic;
using PrinterApp.Model;
using SQLite;
using Xamarin.Forms;

namespace PrinterApp
{	
	public partial class OrderDetailsPage : ContentPage
	{
        private readonly Orders order;
        public OrderDetailsPage (Orders o)
		{
			InitializeComponent ();
            
            order = o;

		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<User>();
                var user = connection.Table<User>().Where(i => i.UserID == order.UserID);
                var convertedUser = user.ToList();

                OrderDetails.Text = "Order Details for Order ID: " + order.OrderID;
                UserID.Text = "User ID: " + convertedUser[0].UserID;
                Name.Text = "Name: " + convertedUser[0].FirstName + " " + convertedUser[0].LastName;
                OrderAddress.Text = "Address: " + order.Address;
                ZipCode.Text = "Zip Code: " + order.ZipCode;
                PhoneNumber.Text = "Phone Number: " + order.PhoneNum;
                DateOrdered.Text = "Date Ordered: " + order.Date.ToString();
                CreditCartNumber.Text = "Credit Card Number: " + order.CreditCardNum;
                TotalCost.Text = "$" + order.TotalCost.ToString();




                var items = connection.Table<OrderItem>().Where(i => i.OrderID == order.OrderID);
                var convertedItems = items.ToList();

                List<Printer> printerList = new List<Printer>();

                foreach(var printer in convertedItems)
                {
                    var selectedPrinter = connection.Table<Printer>().Where(i => i.PrinterID == printer.PrinterID);
                    var convertedSelectedPrinter = selectedPrinter.ToList();
                    printerList.Add(convertedSelectedPrinter[0]);
                }

                orderItemCollectionView.ItemsSource = printerList;
            }
        }

        
    }
}

