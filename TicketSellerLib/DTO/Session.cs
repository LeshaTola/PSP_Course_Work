namespace TicketSellerLib.DTO
{
	public class Session
	{
		public int Id { get; set; }
		public DateOnly Date { get; set; }
		public TimeSpan Time { get; set; }

		public int FilmId { get; set; }
		public Film Film { get; set; }

		public int HallId { get; set; }
		public Hall Hall { get; set; }
	}
}
