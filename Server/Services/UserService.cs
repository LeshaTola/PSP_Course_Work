using Server.DAO;
using TicketSellerLib.DTO;

namespace Server.Services
{
	public class UserService : Service<User>
	{
		private UserDAO dao = new UserDAO();
		public override void Add(User item)
		{
			dao.Add(item);
		}

		public override User Get(int id)
		{
			return dao.Get(id);
		}

		public override List<User> GetAll()
		{
			return dao.GetAll();
		}

		public override void Remove(User item)
		{
			dao.Remove(item);
		}

		public override void Update(User item)
		{
			dao.Update(item);
		}
	}
}
