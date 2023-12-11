using System;
using System.Collections.Generic;
using PrinterApp.Model;
using SQLite;
using Xamarin.Forms;

namespace PrinterApp
{
    public partial class CheckoutPage : ContentPage
    {
        public CheckoutPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                var cart = connection.Table<Cart>().Where(i => i.CartID == App.currUser.CartID);
                var convertedCart = cart.ToList();
                double totalCost = convertedCart[0].GetTotalCost();
                orderTotal.Text = "Total: $" + totalCost.ToString();

            }
        }

        void ConfirmAndPay(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                var userCart = connection.Table<Cart>().Where(i => i.CartID == App.currUser.CartID);
                var convertedUserCart = userCart.ToList();
                
                Orders order = new Orders()
                {
                    Address = addressEntry.Text,
                    Date = DateTime.Now,
                    UserID = App.currUser.UserID,
                    CreditCardNum = cardNumEntry.Text,
                    TotalCost = convertedUserCart[0].GetTotalCost(),
                    PhoneNum = phoneNumEntry.Text,
                    ZipCode = zipCodeEntry.Text
                };

                connection.CreateTable<Orders>();

                int rows = connection.Insert(order);

                var addedOrder = connection.Table<Orders>().Where(i => i.UserID == order.UserID && i.Date == order.Date);

                var convertedAddedOrder = addedOrder.ToList();

                var currItems = connection.Table<CartItems>().Where(i => i.CartID == App.currUser.CartID);

                connection.CreateTable<OrderItem>();

                foreach (var item in currItems)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        PrinterID = item.PrinterID,
                        OrderID = convertedAddedOrder[0].OrderID,
                    };

                    connection.Insert(orderItem);

                    connection.CreateTable<CartItems>();

                    var currCartItems = connection.Table<CartItems>().Where(i => i.CartID == App.currUser.CartID);

                    var convertedCurrCartItems = currCartItems.ToList();

                    foreach(var temp in convertedCurrCartItems)
                    {
                        connection.Delete(temp);
                    }
                }

                if (rows > 0)
                {
                    Navigation.PushAsync(new OrderConfirmationPage());
                }

                else
                {
                    DisplayAlert("Failure", "Checkout failed!", "Ok");
                }
            }
        }
    }
}

