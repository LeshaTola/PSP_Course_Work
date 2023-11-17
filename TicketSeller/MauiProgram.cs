using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSeller.ViewModel;

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

			builder.Services.AddSingleton<AuthorizationViewModel>();
			builder.Services.AddSingleton<RegistrationViewModel>();
			builder.Services.AddTransient<AddFilmViewModel>();
			builder.Services.AddSingleton<FilmsPageViewModel>();

			builder.Services.AddSingleton<Authorization>();
			builder.Services.AddSingleton<Registration>();
			builder.Services.AddTransient<AddFilm>();
			builder.Services.AddSingleton<FilmsPage>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			Client.Client client = new("127.0.0.1", 14447);
			client.Connect();

			return builder.Build();
		}
	}
}
