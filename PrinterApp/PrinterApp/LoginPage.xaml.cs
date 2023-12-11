using System;
using PrinterApp.Model;
using SQLite;
using Xamarin.Forms;

namespace PrinterApp
{	
	public partial class LoginPage : ContentPage
	{
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<User>();

                var users = connection.Table<User>().ToList();
            }
        }

        public async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = usernameEntry.Text,

                Password = passwordEntry.Text
            };

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<User>();

                var selectedUser = connection.Table<User>().Where(i => i.Username == user.Username
                                                                    && i.Password == user.Password);

                var convertedUser = selectedUser.ToList();

                if (convertedUser.Count > 0)
                {
                    var loggedIn = new User
                    {
                        UserID = convertedUser[0].UserID,

                        Username = convertedUser[0].Username,

                        Password = convertedUser[0].Password,

                        FirstName = convertedUser[0].FirstName,

                        LastName = convertedUser[0].LastName,

                        Email = convertedUser[0].Email,

                        CartID = convertedUser[0].CartID
                    };

                    App.currUser = loggedIn;

                    if (convertedUser[0].Username == "admin" && convertedUser[0].Password == "admin")
                    {
                        usernameEntry.Text = string.Empty;

                        passwordEntry.Text = string.Empty;

                        await Navigation.PushAsync(new AdminPage());
                    }
                    else
                    {
                        usernameEntry.Text = string.Empty;

                        passwordEntry.Text = string.Empty;

                        await Navigation.PushAsync(new HomePage());
                    }
                }

                else
                {
                    await DisplayAlert("Failure", "Did not find user. Try again!", "Ok");

                    usernameEntry.Text = string.Empty;

                    passwordEntry.Text = string.Empty;
                }  
            }
        }
    }
}