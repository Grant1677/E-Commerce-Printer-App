using SQLite;

namespace PrinterApp.Model
{
	public class Printer
	{
        [PrimaryKey, AutoIncrement]
        public int PrinterID { get; set; }

        public string ManName { get; set; }

        public string ModelNum { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }

        public bool IsColor { get; set; }

        public void UpdateQuantity()
        {
            Quantity -= 1; 
        }
    }
}

