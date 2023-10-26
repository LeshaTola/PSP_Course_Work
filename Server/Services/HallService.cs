using Server.DAO;
using TicketSellerLib.DTO;

namespace Server.Services
{
	public class HallService : Service<Hall>
	{
		private HallDAO dao = new HallDAO();
		public override void Add(Hall item)
		{
			dao.Add(item);
		}

		public override Hall Get(int id)
		{
			return dao.Get(id);
		}

		public override List<Hall> GetAll()
		{
			return dao.GetAll();
		}

		public override void Remove(Hall item)
		{
			dao.Remove(item);
		}

		public override void Update(Hall item)
		{
			dao.Update(item);
		}
	}
}
