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

			var film = new Film
			{
				ImageURL = "https://upload.wikimedia.org/wikipedia/ru/2/2b/%D0%94%D0%B6%D1%83%D0%BC%D0%B0%D0%BD%D0%B4%D0%B6%D0%B8_%D0%97%D0%BE%D0%B2_%D0%B4%D0%B6%D1%83%D0%BD%D0%B3%D0%BB%D0%B5%D0%B9_2017.jpg",
				Name = "Джуманджи",
				Description = "Четверо подростков оказываются внутри игры «Джуманджи». Их ждет схватка с носорогами, черными мамбами, а на каждом шагу будет подстерегать бесконечная череда ловушек и головоломок. Чтобы пройти игру и остаться в живых, им придется перевоплотиться в персонажей игры: робкий и застенчивый Спенсер превращается в отважного и сильного исследователя, здоровяк Фридж — в коротышку, модница и красавица Беттани — в профессора, а неуклюжая и нескладная Марта становится бесстрашной и ловкой амазонкой. Друзьям придется привыкнуть к совершенно новым и таким непривычным для себя ролям и найти дорогу домой.",
				Cost = 8f,
				Duration = 120
			};
			var film2 = new Film
			{
				ImageURL = "https://upload.wikimedia.org/wikipedia/ru/5/5f/Spiderman_movie.jpg",
				Name = "Человек Паук",
				Description = "Питер Паркер – обыкновенный школьник. " +
				"Однажды он отправился с классом на экскурсию, где его кусает странный паук-мутант. " +
				"Через время парень почувствовал в себе нечеловеческую силу и ловкость в движении, " +
				"а главное – умение лазать по стенам и метать стальную паутину. Свои способности он направляет на защиту слабых. " +
				"Так Питер становится настоящим супергероем по имени Человек-паук, который помогает людям и борется с преступностью. " +
				"Но там, где есть супергерой, рано или поздно всегда объявляется и суперзлодей...",
				Cost = 16f,
				Duration = 180
			};
			var film3 = new Film
			{
				ImageURL = "https://upload.wikimedia.org/wikipedia/ru/c/c3/Interstellar_2014.jpg",
				Name = "Интерстеллар",
				Description = "Наше время на Земле подошло к концу, команда исследователей берет на себя самую важную миссию в истории человечества; путешествуя за пределами нашей галактики, чтобы узнать есть ли у человечества будущее среди звезд.",
				Cost = 10f,
				Duration = 111
			};

			Sessions.Add(new Session { Date = DateOnly.FromDateTime(new DateTime(2022, 10, 10)), Film = film, Hall = hall, Time = new TimeSpan(15, 20, 0) });
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
