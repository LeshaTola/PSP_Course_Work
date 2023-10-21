namespace TicketSellerLib.DTO
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Cost { get; set; }
        public float Duration { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        //public List<Session> Sessions { get; set; }

    }
}
