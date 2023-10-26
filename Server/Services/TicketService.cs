using Server.DAO;
using TicketSellerLib.DTO;

namespace Server.Services
{
	public class TicketService : Service<Ticket>
	{
		private TicketDAO dao = new TicketDAO();
		public override void Add(Ticket item)
		{
			dao.Add(item);
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

		public override void Update(Ticket item)
		{
			dao.Update(item);
		}
	}
}
