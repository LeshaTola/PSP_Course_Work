using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server.DAO
{
	internal class CinemaDAO : DAO<Cinema>
	{
		public override void Upsert(Cinema item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				var cinema = db.Cinemas.Find(item.Id);
				if (cinema != null)
				{
					db.Cinemas.Update(item);
				}
				else
				{
					db.Cinemas.Add(item);
				}

				db.SaveChanges();
			}
		}

		public override Cinema Get(int id)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Cinemas.FirstOrDefault(c => c.Id == id);
			}
		}

		public override List<Cinema> GetAll()
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Cinemas.ToList();
			}
		}

		public override void Remove(Cinema item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Cinemas.Remove(item);
				db.SaveChanges();
			}
		}
	}
}
