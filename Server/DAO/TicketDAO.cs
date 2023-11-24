using Microsoft.EntityFrameworkCore;
using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server.DAO
{
	internal class TicketDAO : DAO<Ticket>
	{
		public override void Upsert(Ticket item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Upsert(item);
				db.SaveChanges();
			}
		}

		public override Ticket Get(int id)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Tickets.First(t => t.Id == id);
			}
		}

		public override List<Ticket> GetAll()
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Tickets.ToList();
			}
		}

		public override void Remove(Ticket item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Remove(item);
				db.SaveChanges();
			}
		}
	}
}
