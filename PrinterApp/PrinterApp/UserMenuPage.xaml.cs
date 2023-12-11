using System;
using System.Collections.Generic;
using SQLite;
using PrinterApp.Model;
using Xamarin.Forms;

namespace PrinterApp
{	
	public partial class UserMenuPage : ContentPage
	{	
		public UserMenuPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<User>();
                var users = connection.Table<User>().ToList();
                userCollectionView.ItemsSource = users;
            }
        }

        async void RemoveUser(object sender, EventArgs e)
        {
            int action = int.Parse(await DisplayPromptAsync("Enter a User ID to remove", "User ID: "));
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<User>();
                var selectedUser = connection.Table<User>().Where(i => i.UserID == action);
                var convertedUser = selectedUser.ToList();
                


                if (convertedUser.Count > 0)
                {
                    string confirmAction = await DisplayActionSheet("Confirm Removal: " + convertedUser[0].UserID, "Cancel", null, "Confirm");
                    if (confirmAction == "Confirm")
                    {
                        int rows = connection.Delete(convertedUser[0]);
                        if (rows > 0)
                        {
                            await DisplayAlert("Success", "User deleted.", "Ok");
                            OnAppearing();
                        }
                        else
                        {
                            await DisplayAlert("Failure", "User not deleted!", "Ok");
                        }
                    }

                }
                else
                {
                    await DisplayAlert("Failure", "User ID not found!", "Ok");

                }
                

            }
        }
    }
}

