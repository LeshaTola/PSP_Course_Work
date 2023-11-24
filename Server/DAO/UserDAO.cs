using Microsoft.EntityFrameworkCore;
using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server.DAO
{
	public class UserDAO : DAO<User>
	{
		public override void Upsert(User user)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Users.Upsert(user).On(c => c.Id).Run();
				db.SaveChanges();
			}
		}

		public override User Get(int id)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Users.First(u => u.Id == id);
			}
		}

		public override List<User> GetAll()
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Users.ToList();
			}
		}

		public override void Remove(User user)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Users.Remove(user);
				db.SaveChanges();
			}
		}
	}
}
