namespace TicketSellerLib.DTO
{
	public class Film
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public float Cost { get; set; }
		public float Duration { get; set; }
		public List<Session> Sessions { get; set; }

	}
}
