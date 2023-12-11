using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PrinterApp
{	
	public partial class OrderConfirmationPage : ContentPage
	{	
		public OrderConfirmationPage ()
		{
			InitializeComponent ();
		}

        async void BackToLogin(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        async void BackToHome(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}

