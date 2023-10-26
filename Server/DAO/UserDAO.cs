using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server.DAO
{
	public class UserDAO : DAO<User>
	{
		public override void Add(User user)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Users.Add(user);
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

		public override void Update(User user)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Users.Update(user);
				db.SaveChanges();
			}
		}
	}
}
