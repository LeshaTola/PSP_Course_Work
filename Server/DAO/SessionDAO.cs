using Microsoft.EntityFrameworkCore;
using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server.DAO
{
	public class SessionDAO : DAO<Session>
	{
		public override void Upsert(Session item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Sessions.Upsert(item);
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
	}
}
