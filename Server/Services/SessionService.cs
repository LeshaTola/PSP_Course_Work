using Server.DAO;
using TicketSellerLib.DTO;

namespace Server.Services
{
	public class SessionService : Service<Session>
	{
		private SessionDAO dao = new SessionDAO();
		public override void Upsert(Session item)
		{
			dao.Upsert(item);
		}

		public override Session Get(int id)
		{
			return dao.Get(id);
		}

		public override List<Session> GetAll()
		{
			return dao.GetAll();
		}

		public override void Remove(Session item)
		{
			dao.Remove(item);
		}
	}
}
