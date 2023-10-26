using Server.DAO;
using TicketSellerLib.DTO;

namespace Server.Services
{
	public class FilmService : Service<Film>
	{
		private FilmDAO dao = new FilmDAO();
		public override void Add(Film item)
		{
			dao.Add(item);
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

		public override void Update(Film item)
		{
			dao.Update(item);
		}
	}
}
