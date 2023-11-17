using TicketSeller.View;

namespace TicketSeller
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(AddFilm), typeof(AddFilm));
			Routing.RegisterRoute(nameof(FilmsPage), typeof(FilmsPage));
			Routing.RegisterRoute(nameof(Registration), typeof(Registration));
		}
	}
}
