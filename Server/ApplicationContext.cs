using Microsoft.EntityFrameworkCore;
using TicketSellerLib.DTO;

namespace TicketSeller.Model
{
	class ApplicationContext : DbContext
	{
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Hall> Halls { get; set; } = null!;
		public DbSet<Cinema> Cinema { get; set; } = null!;
		public DbSet<Film> Films { get; set; } = null!;
		public DbSet<Session> Sessions { get; set; } = null!;
		public DbSet<Ticket> Tickets { get; set; } = null!;

		public ApplicationContext()
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Host=localhost;Port=8888;Database=Users;Username=postgres;Password=admin");
		}
	}
}
