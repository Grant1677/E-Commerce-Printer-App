using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PrinterApp
{	
	public partial class AdminPage : ContentPage
	{	
		public AdminPage ()
		{
			InitializeComponent ();
		}

        async void OnPrinterMenuButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrinterMenuPage());
        }

        async void OnUserMenuButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserMenuPage());
        }

        async void ViewPastOrdersButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderHistory());
        }

        async void LogoutOfAdmin(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}

