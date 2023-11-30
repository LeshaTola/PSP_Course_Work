using Microsoft.EntityFrameworkCore;
using TicketSellerLib.DTO;

namespace TicketSeller.Model
{
	class ApplicationContext : DbContext
	{
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Cinema> Cinemas { get; set; } = null!;
		public DbSet<Hall> Halls { get; set; } = null!;
		public DbSet<Film> Films { get; set; } = null!;
		public DbSet<Session> Sessions { get; set; } = null!;
		public DbSet<Ticket> Tickets { get; set; } = null!;


		public void Init()
		{
			Database.EnsureDeleted();
			Database.EnsureCreated();

			Users.Add(new User { Login = "", Password = "", IsAdmin = true, Email = "@gmail.com" });
			Users.Add(new User { Login = "admin", Password = "admin", IsAdmin = true, Email = "admin@gmail.com" });
			Users.Add(new User { Login = "user", Password = "user", IsAdmin = false, Email = "user@gmail.com" });

			var cinema = new Cinema { Name = "Зоотопия", Address = "Африка" };
			var cinema2 = new Cinema { Name = "Звезда", Address = "Ул Ленина" };

			var hall = new Hall { Columns = 12, Rows = 5, Cinema = cinema, Name = "Малый" };
			var hall2 = new Hall { Columns = 16, Rows = 8, Cinema = cinema, Name = "Большой" };
			var hall3 = new Hall { Columns = 16, Rows = 8, Cinema = cinema2, Name = "Большой" };

			var film = new Film { Name = "Z", Description = "Zzzz...", Cost = 8f, Duration = 120 };
			var film2 = new Film { Name = "Человек Паук", Description = "Описание...", Cost = 16f, Duration = 180 };
			var film3 = new Film { Name = "Интерстеллар", Description = "Описание...", Cost = 10f, Duration = 111 };

			Sessions.Add(new Session { Date = DateOnly.FromDateTime(DateTime.Now), Film = film, Hall = hall, Time = new TimeSpan(15, 20, 0) });
			Sessions.Add(new Session { Date = DateOnly.FromDateTime(DateTime.Now), Film = film2, Hall = hall, Time = new TimeSpan(10, 20, 0) });
			Sessions.Add(new Session { Date = DateOnly.FromDateTime(DateTime.Now), Film = film2, Hall = hall2, Time = new TimeSpan(22, 20, 0) });
			Sessions.Add(new Session { Date = DateOnly.FromDateTime(DateTime.Now), Film = film3, Hall = hall3, Time = new TimeSpan(11, 20, 0) });

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Host=localhost;Port=8888;Database=TicketSaleDB;Username=postgres;Password=postgres;IncludeErrorDetail=True;")
			.EnableDetailedErrors();
		}
	}
}
