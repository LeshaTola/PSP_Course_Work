using Server.DAO;
using TicketSellerLib.DTO;

namespace Server.Services
{
	public class FilmService : Service<Film>
	{
		private FilmDAO dao = new FilmDAO();
		public override void Upsert(Film item)
		{
			dao.Upsert(item);
		}

		public override Film Get(int id)
		{
			return dao.Get(id);
		}

		public override List<Film> GetAll()
		{
			return dao.GetAll();
		}

		public override void Remove(Film item)
		{
			dao.Remove(item);
		}
	}
}
