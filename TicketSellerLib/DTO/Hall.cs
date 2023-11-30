namespace TicketSellerLib.DTO
{
	public class Hall
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int Rows { get; set; }
		public int Columns { get; set; }

		public int CinemaId { get; set; }
		public Cinema Cinema { get; set; }

	}
}
