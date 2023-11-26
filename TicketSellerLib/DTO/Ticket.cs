namespace TicketSellerLib.DTO
{
	public class Ticket
	{
		public int Id { get; set; }
		public int Row { get; set; }
		public int Column { get; set; }

		public Session Session { get; set; }
		public User User { get; set; }
	}
}
