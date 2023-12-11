using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PrinterApp
{	
	public partial class SettingsPage : ContentPage
	{	
		public SettingsPage ()
		{
			InitializeComponent ();
		}

        async void LogoutButton(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}

