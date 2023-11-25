using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSeller.ViewModel;
using TicketSeller.ViewModel.Films;
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
			builder.Services.AddSingleton<FilmServices>();

			builder.Services.AddSingleton<AdminViewModel>();
			builder.Services.AddSingleton<AuthorizationViewModel>();
			builder.Services.AddTransient<RegistrationViewModel>();
			builder.Services.AddSingleton<FilmsViewModel>();
			builder.Services.AddTransient<AddFilmViewModel>();
			builder.Services.AddSingleton<UsersViewModel>();
			builder.Services.AddTransient<AddUserViewModel>();

			builder.Services.AddSingleton<Admin>();
			builder.Services.AddSingleton<Authorization>();
			builder.Services.AddTransient<Registration>();
			builder.Services.AddSingleton<Films>();
			builder.Services.AddTransient<AddFilm>();
			builder.Services.AddSingleton<Users>();
			builder.Services.AddTransient<AddUser>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			Client.Client client = new("127.0.0.1", 14447);
			client.Connect();

			return builder.Build();
		}
	}
}
