using Server.DAO;
using TicketSellerLib.DTO;

namespace Server.Services
{
	public class TicketService : Service<Ticket>
	{
		private TicketDAO dao = new TicketDAO();
		public override void Upsert(Ticket item)
		{
			dao.Upsert(item);
		}

		public override Ticket Get(int id)
		{
			return dao.Get(id);
		}

		public override List<Ticket> GetAll()
		{
			return dao.GetAll();
		}

		public override void Remove(Ticket item)
		{
			dao.Remove(item);
		}
	}
}
