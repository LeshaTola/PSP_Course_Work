using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server.DAO
{
	internal class HallDAO : DAO<Hall>
	{
		public override void Upsert(Hall item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				var cinema = db.Halls.Find(item.Id);
				if (cinema != null)
				{
					db.Halls.Update(item);
				}
				else
				{
					db.Halls.Add(item);
				}
				db.SaveChanges();
			}
		}

		public override Hall Get(int id)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Halls.FirstOrDefault(h => h.Id == id);
			}
		}

		public override List<Hall> GetAll()
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Halls.ToList();
			}
		}

		public override void Remove(Hall item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Halls.Remove(item);
				db.SaveChanges();
			}
		}
	}
}
