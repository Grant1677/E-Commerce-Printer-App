using System;
using System.Collections.Generic;
using SQLite;
using PrinterApp.Model;
using Xamarin.Forms;

namespace PrinterApp
{	
	public partial class RegisterPage : ContentPage
	{
        public RegisterPage()
        {
            InitializeComponent();
        }

        private bool RegistrationValidation()
        {
            bool returnVal;
            returnVal = true;
            
            if (string.IsNullOrEmpty(lastnameEntry.Text) || string.IsNullOrWhiteSpace(lastnameEntry.Text))
            {
                DisplayAlert("Failure", "Please enter a Last Name.", "Ok");
                lastnameEntry.PlaceholderColor = Color.Red;
                returnVal = false;
            }
            if (string.IsNullOrEmpty(firstnameEntry.Text) || string.IsNullOrWhiteSpace(firstnameEntry.Text))
            {
                DisplayAlert("Failure", "Please enter a First Name.", "Ok");
                firstnameEntry.PlaceholderColor = Color.Red;
                returnVal = false;
            }
            if (string.IsNullOrEmpty(usernameEntry.Text) || string.IsNullOrWhiteSpace(usernameEntry.Text))
            {
                DisplayAlert("Failure", "Please enter a Username.", "Ok");
                usernameEntry.PlaceholderColor = Color.Red;
                returnVal = false;
            }
            if (string.IsNullOrEmpty(passwordEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                DisplayAlert("Failure", "Please enter a Password.", "Ok");
                passwordEntry.PlaceholderColor = Color.Red;
                returnVal = false;
            }
            if (string.IsNullOrEmpty(emailEntry.Text) || string.IsNullOrWhiteSpace(emailEntry.Text))
            {
                DisplayAlert("Failure", "Please enter a Email Address.", "Ok");
                emailEntry.PlaceholderColor = Color.Red;
                returnVal = false;
            }

            return returnVal;
        }

        private void OnRegisterAccountButtonClicked(object sender, EventArgs e)
        {
            //Registration validation
            if (!RegistrationValidation())
            {
                return;
            }
            

            //Create a new SQLite connection
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {

                User user = new User()
                {
                    FirstName = firstnameEntry.Text,
                    LastName = lastnameEntry.Text,
                    Username = usernameEntry.Text,
                    Password = passwordEntry.Text,
                    Email = emailEntry.Text
                    
                };

                //Create a new instance of the cart for a particular user
                user.CreateCart();


                //Create a new table of users
                connection.CreateTable<User>();

                //Check to see if there is a username and email to see if it matches anything in the database

                var selectedUser = connection.Table<User>().Where(i => i.Username == user.Username || i.Email == user.Email).FirstOrDefault();
                if(selectedUser == null)
                {
                    //Insert user information into user table
                    int rows = connection.Insert(user);

                    if (rows > 0)
                    {
                        DisplayAlert("Success", "User information inserted!", "Ok");
                        Navigation.PopAsync();
                    }

                    else
                        DisplayAlert("Failure", "User information NOT inserted!", "Ok");
                }
                else
                {
                    DisplayAlert("Failure", "Username or Email already exists!", "Ok");
                    emailEntry.Text = string.Empty;
                    passwordEntry.Text = string.Empty;
                    emailEntry.Text = string.Empty;
                    firstnameEntry.Text = string.Empty;
                    lastnameEntry.Text = string.Empty;
                }
            }
        }

        private void BackToLogin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

    }
}

