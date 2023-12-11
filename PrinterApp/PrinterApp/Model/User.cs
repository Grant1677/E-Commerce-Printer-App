using SQLite;

namespace PrinterApp.Model
{
	public class User
	{
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }  //Make this the primary key

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int CartID { get; set; }

        public void CreateCart()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                Cart cart = new Cart();
                connection.CreateTable<Cart>();
                int newCart = connection.Insert(cart);
                CartID = cart.CartID;

            }
                
        }

    }
}

