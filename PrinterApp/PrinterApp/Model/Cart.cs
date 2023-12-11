using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PrinterApp.Model
{
	public class Cart
	{
        [PrimaryKey, AutoIncrement]
        public int CartID { get; set; }

        public double GetTotalCost()
        {
            double cost = 0.0;

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<CartItems>();
                var selectedItems = connection.Table<CartItems>().Where(i => i.CartID == CartID);
                var convertedItems = selectedItems.ToList();
                foreach (var item in convertedItems)
                {
                    connection.CreateTable<Printer>();
                    var selectedPrinter = connection.Table<Printer>().Where(i => i.PrinterID == item.PrinterID);
                    var convertedPrinter = selectedPrinter.ToList();
                    if (convertedPrinter.Count > 0)
                    {
                        cost += convertedPrinter[0].Price;
                    }

                    
                }

                return cost;
            }
        }
    }
}

