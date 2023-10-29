using Microsoft.Extensions.Logging;
using System.Net;
using TicketSeller.Services;
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
			builder.Services.AddSingleton<Authorization>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
