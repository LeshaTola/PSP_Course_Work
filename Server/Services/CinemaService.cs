using Server.DAO;
using TicketSellerLib.DTO;

namespace Server.Services
{
	public class CinemaService : Service<Cinema>
	{
		private CinemaDAO dao = new CinemaDAO();
		public override void Upsert(Cinema item)
		{
			dao.Upsert(item);
		}

		public override Cinema Get(int id)
		{
			return dao.Get(id);
		}

		public override List<Cinema> GetAll()
		{
			return dao.GetAll();
		}

		public override void Remove(Cinema item)
		{
			dao.Remove(item);
		}
	}
}
