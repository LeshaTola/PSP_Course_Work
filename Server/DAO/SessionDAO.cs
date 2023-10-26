using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server.DAO
{
	public class SessionDAO : DAO<Session>
	{
		public override void Add(Session item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Sessions.Add(item);
				db.SaveChanges();
			}
		}

		public override Session Get(int id)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Sessions.FirstOrDefault(s => s.Id == id);
			}
		}

		public override List<Session> GetAll()
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Sessions.ToList();
			}
		}

		public override void Remove(Session item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Sessions.Remove(item);
				db.SaveChanges();
			}
		}

		public override void Update(Session item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Sessions.Update(item);
				db.SaveChanges();
			}
		}
	}
}
