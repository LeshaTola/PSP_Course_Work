using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server.DAO
{
	public class FilmDAO : DAO<Film>
	{
		public override void Add(Film item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Films.Add(item);
				db.SaveChanges();
			}
		}

		public override Film Get(int id)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Films.FirstOrDefault(f => f.Id == id);
			}
		}

		public override List<Film> GetAll()
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Films.ToList();
			}
		}

		public override void Remove(Film item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Films.Remove(item);
				db.SaveChanges();
			}
		}

		public override void Update(Film item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Films.Update(item);
				db.SaveChanges();
			}
		}
	}
}
