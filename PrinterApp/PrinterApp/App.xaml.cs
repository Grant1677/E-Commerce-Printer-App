using PrinterApp.Model;
using Xamarin.Forms;

namespace PrinterApp
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static User currUser;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());

            DatabaseLocation = databaseLocation;
        }
    }
}

