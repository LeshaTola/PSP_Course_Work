using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSeller.View.Others.UserOthers;
using TicketSeller.ViewModel;
using TicketSeller.ViewModel.Cinemas;
using TicketSeller.ViewModel.Films;
using TicketSeller.ViewModel.Halls;
using TicketSeller.ViewModel.Others.UserOthers;
using TicketSeller.ViewModel.Sessions;
using TicketSeller.ViewModel.Tickets;
using TicketSeller.ViewModel.UserOthers;
using TicketSeller.ViewModel.Users;

namespace TicketSeller
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				}).UseMauiCommunityToolkit();

			builder.Services.AddSingleton<UserService>();
			builder.Services.AddSingleton<FilmService>();
			builder.Services.AddSingleton<CinemaService>();
			builder.Services.AddSingleton<HallService>();
			builder.Services.AddSingleton<SessionService>();
			builder.Services.AddSingleton<TicketService>();

			//#############################################################################################

			builder.Services.AddSingleton<AdminViewModel>();
			builder.Services.AddSingleton<UserViewModel>();

			builder.Services.AddTransient<AboutMeViewModel>();
			builder.Services.AddTransient<UserFilmsViewModel>();
			builder.Services.AddTransient<UserSessionsByFilmViewModel>();

			builder.Services.AddSingleton<AuthorizationViewModel>();
			builder.Services.AddTransient<RegistrationViewModel>();

			builder.Services.AddSingleton<FilmsViewModel>();
			builder.Services.AddTransient<AddFilmViewModel>();

			builder.Services.AddSingleton<UsersViewModel>();
			builder.Services.AddTransient<AddUserViewModel>();

			builder.Services.AddSingleton<CinemasViewModel>();
			builder.Services.AddTransient<AddCinemaViewModel>();

			builder.Services.AddSingleton<HallsViewModel>();
			builder.Services.AddTransient<AddHallViewModel>();

			builder.Services.AddSingleton<SessionsViewModel>();
			builder.Services.AddTransient<AddSessionViewModel>();

			builder.Services.AddSingleton<TicketsViewModel>();
			builder.Services.AddTransient<AddTicketViewModel>();

			//#############################################################################################
			builder.Services.AddSingleton<Admin>();
			builder.Services.AddSingleton<UserPanel>();

			builder.Services.AddTransient<AboutMe>();
			builder.Services.AddTransient<EditUserForUser>();
			builder.Services.AddTransient<UserFilms>();
			builder.Services.AddTransient<UserSessionsByFilm>();

			builder.Services.AddSingleton<Authorization>();
			builder.Services.AddTransient<Registration>();

			builder.Services.AddSingleton<Films>();
			builder.Services.AddTransient<AddFilm>();

			builder.Services.AddSingleton<Users>();
			builder.Services.AddTransient<AddUser>();

			builder.Services.AddSingleton<Cinemas>();
			builder.Services.AddTransient<AddCinema>();

			builder.Services.AddSingleton<Halls>();
			builder.Services.AddTransient<AddHall>();
			builder.Services.AddTransient<Seats>();

			builder.Services.AddSingleton<Sessions>();
			builder.Services.AddTransient<AddSession>();

			builder.Services.AddSingleton<Tickets>();
			builder.Services.AddTransient<AddTicket>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			Client.Client client = new("127.0.0.1", 14447);
			client.Connect();

			return builder.Build();
		}
	}
}
