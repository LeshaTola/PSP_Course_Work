namespace TicketSellerLib.DTO
{
	public class Ticket
	{
		public int Id { get; set; }
		public Session Session { get; set; }
		public User User { get; set; }
	}
}
