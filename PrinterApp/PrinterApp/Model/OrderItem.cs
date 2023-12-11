using System;
using SQLite;

namespace PrinterApp.Model
{
	public class OrderItem
	{
        [PrimaryKey, AutoIncrement]
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int PrinterID { get; set; }
    }
}

