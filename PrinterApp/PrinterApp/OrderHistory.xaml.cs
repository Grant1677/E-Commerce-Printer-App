using System;
using System.Collections.Generic;
using PrinterApp.Model;
using SQLite;
using Xamarin.Forms;

namespace PrinterApp
{	
	public partial class OrderHistory : ContentPage
	{	
		public OrderHistory ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Orders>();
                var orders = connection.Table<Orders>().ToList();
                orderCollectionView.ItemsSource = orders;
            }
        }

        void orderCollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                Orders order = e.CurrentSelection[0] as Orders;
                Navigation.PushAsync(new OrderDetailsPage(order));
            }
        }

        //********* DO NOT USE THIS UNLESS U WANT TO REMOVE ALL STUFF FROM DATABASE!!!!!!!!******
        //async void ClearData(object sender, EventArgs e)
        //{
            

        //    using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
        //    {
        //        connection.DeleteAll<OrderItem>();
        //        connection.DeleteAll<Orders>();
        //        connection.DeleteAll<CartItems>();

        //        OnAppearing();
        //    }
        //}


    }
}

