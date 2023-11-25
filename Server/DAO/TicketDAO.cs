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
				var cinema = Get(item.Id);
				if (cinema != null)
				{
					db.Tickets.Update(item);
				}
				else
				{
					db.Tickets.Add(item);
				}
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
