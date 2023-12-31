﻿using TicketSeller.View;
using TicketSeller.View.Others.UserOthers;

namespace TicketSeller
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
			Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));

			Routing.RegisterRoute(nameof(AboutMe), typeof(AboutMe));
			Routing.RegisterRoute(nameof(EditUserForUser), typeof(EditUserForUser));
			Routing.RegisterRoute(nameof(UserFilms), typeof(UserFilms));
			Routing.RegisterRoute(nameof(UserSessionsByFilm), typeof(UserSessionsByFilm));
			Routing.RegisterRoute(nameof(UserSessionHistory), typeof(UserSessionHistory));

			Routing.RegisterRoute(nameof(Registration), typeof(Registration));

			Routing.RegisterRoute(nameof(AddFilm), typeof(AddFilm));
			Routing.RegisterRoute(nameof(Films), typeof(Films));

			Routing.RegisterRoute(nameof(AddUser), typeof(AddUser));
			Routing.RegisterRoute(nameof(Users), typeof(Users));

			Routing.RegisterRoute(nameof(AddCinema), typeof(AddCinema));
			Routing.RegisterRoute(nameof(Cinemas), typeof(Cinemas));

			Routing.RegisterRoute(nameof(AddHall), typeof(AddHall));
			Routing.RegisterRoute(nameof(Halls), typeof(Halls));
			Routing.RegisterRoute(nameof(Seats), typeof(Seats));

			Routing.RegisterRoute(nameof(AddSession), typeof(AddSession));
			Routing.RegisterRoute(nameof(Sessions), typeof(Sessions));

			Routing.RegisterRoute(nameof(AddTicket), typeof(AddTicket));
			Routing.RegisterRoute(nameof(Tickets), typeof(Tickets));
		}
	}
}
