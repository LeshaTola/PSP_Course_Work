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
				});

			builder.Services.AddSingleton<UserService>();

			builder.Services.AddSingleton<AuthorizationViewModel>();
			builder.Services.AddSingleton<RegistrationViewModel>();
			builder.Services.AddSingleton<MainPageViewModel>();

			builder.Services.AddSingleton<Authorization>();
			builder.Services.AddSingleton<Registration>();
			builder.Services.AddSingleton<MainPage>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			Client.Client client = new("127.0.0.1", 14447);
			client.Connect();

			return builder.Build();
		}
	}
}
