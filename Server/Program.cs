using System.Net;
using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server
{
	public class Program
	{
		static async Task Main(string[] args)
		{
			using (var context = new ApplicationContext())
			{
				context.Init();
				context.SaveChanges();
			}
			Server server = new Server(IPAddress.Any, 14447);
			await server.StartServerAsync();
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
