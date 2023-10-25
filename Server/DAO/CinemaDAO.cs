using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server.DAO
{
	internal class CinemaDAO : DAO<Cinema>
	{
		public override void Add(Cinema item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Cinema.Add(item);
				db.SaveChanges();
			}
		}

		public override Cinema Get(int id)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Cinema.FirstOrDefault(c => c.Id == id);
			}
		}

		public override List<Cinema> GetAll()
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Cinema.ToList();
			}
		}

		public override void Remove(Cinema item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Cinema.Remove(item);
				db.SaveChanges();
			}
		}

		public override void Update(Cinema item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Cinema.Update(item);
				db.SaveChanges();
			}
		}
	}
}
