using Server.Services;

namespace TicketSeller.Test
{
	public class Tests
	{
		[Test]
		public void UserServiceTestGetAll()
		{
			var userService = new UserService();
			var users = userService.GetAll();
			Assert.IsNotNull(users);
		}

		[Test]
		public void FilmServiceTestGetAll()
		{
			var filmService = new FilmService();
			var films = filmService.GetAll();
			Assert.IsNotNull(films);
		}

		[Test]
		public void HallServiceTestGetAll()
		{
			var hallService = new HallService();
			var halls = hallService.GetAll();
			Assert.IsNotNull(halls);
		}

		[Test]
		public void SessionsServiceTestGetAll()
		{
			var sessionService = new SessionService();
			var sessions = sessionService.GetAll();
			Assert.IsNotNull(sessions);
		}

		[Test]
		public void TicketsServiceTestGetAll()
		{
			var ticketService = new TicketService();
			var tickets = ticketService.GetAll();
			Assert.IsNotNull(tickets);
		}
	}
}