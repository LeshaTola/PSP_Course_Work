using Microsoft.EntityFrameworkCore;
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
				db.Cinema.Upsert(item).On(c => c.Id).Run();
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
	}
}
