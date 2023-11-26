namespace TicketSellerLib.DTO
{
	public class Session
	{
		public int Id { get; set; }
		public DateOnly Date { get; set; }
		public TimeSpan Time { get; set; }

		public Film Film { get; set; }
		public Hall Hall { get; set; }
		public List<Ticket> Tickets { get; set; }

	}
}
