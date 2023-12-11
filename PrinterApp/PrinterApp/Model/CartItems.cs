using System;
using SQLite;

namespace PrinterApp.Model
{
	public class CartItems
	{
        [PrimaryKey, AutoIncrement]
        public int CartItemsID { get; set; }

        public int CartID { get; set; }

        public int PrinterID { get; set; }

    }
}

