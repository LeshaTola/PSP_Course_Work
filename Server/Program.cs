using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server
{
	public class Program
	{
		static void Main(string[] args)
		{
			/*AddUser(new User()
			{
				Login = "admin",
				Password = "admin",
				Email = "Tola.Lesha@mail.ru"
			});*/
			//var dao = new UserDAO();
			//Console.WriteLine(dao.Get(1).Login);
		}

		public static void AddUser(User user)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Users.Add(user);
				db.SaveChanges();
			}
		}
	}
}
