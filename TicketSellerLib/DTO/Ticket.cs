namespace TicketSellerLib.DTO
{
	public class Ticket
	{
		public int Id { get; set; }
		public int Row { get; set; }
		public int Column { get; set; }


		public int SessionId { get; set; }
		public Session Session { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }
	}
}
