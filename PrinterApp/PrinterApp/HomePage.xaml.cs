using System;
using SQLite;
using PrinterApp.Model;
using Xamarin.Forms;
using System.Windows.Input;
using System.Linq;
using System.Xml.Linq;


namespace PrinterApp
{	
	public partial class HomePage : ContentPage
	{
        public HomePage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Printer>();

                var printers = connection.Table<Printer>().ToList();

                printerCollectionView.ItemsSource = printers;
            }
        }

        async void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        async void ViewCart(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewCartPage());
        }

        public void ViewAllInOne(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Printer>();

                var printers = connection.Table<Printer>().Where(i => i.Category == "All-in-one");

                printerCollectionView.ItemsSource = printers.ToList();
            }
            PrinterCategory.Text = "All-in-one Printers";
        }

        public void ViewInkjet(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Printer>();

                var printers = connection.Table<Printer>().Where(i => i.Category == "Inkjet");

                printerCollectionView.ItemsSource = printers.ToList();
            }
            PrinterCategory.Text = "Inkjet Printers";
        }

        public void ViewLaser(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Printer>();

                var printers = connection.Table<Printer>().Where(i => i.Category == "Laser");

                printerCollectionView.ItemsSource = printers.ToList();
            }
            PrinterCategory.Text = "Laser Printers";
        }

        public void ViewAll(object sender, EventArgs e)
        {
            OnAppearing();

            PrinterCategory.Text = "All Printers";
        }

        async void printerCollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                Printer printer = e.CurrentSelection[0] as Printer;

                await Navigation.PushAsync(new PrinterViewPage(printer));
            }
        }
    }
}