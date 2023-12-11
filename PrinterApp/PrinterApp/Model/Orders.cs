using System;
using SQLite;

namespace PrinterApp.Model
{
	public class Orders
	{
        [PrimaryKey, AutoIncrement]
        public int OrderID { get; set; }
        public DateTime Date { get; set; }
        public string CreditCardNum { get; set; }
        public string Address { get; set; }
        public string PhoneNum { get; set; }
        public string ZipCode { get; set; }
        public double TotalCost { get; set; }
        public int UserID { get; set; }
    }
}

