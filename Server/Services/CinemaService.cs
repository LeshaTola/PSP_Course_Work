using Server.DAO;
using TicketSellerLib.DTO;

namespace Server.Services
{
	public class CinemaService : Service<Cinema>
	{
		private CinemaDAO dao = new CinemaDAO();
		public override void Add(Cinema item)
		{
			dao.Add(item);
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

		public override void Update(Cinema item)
		{
			dao.Update(item);
		}
	}
}
