using TicketSeller.View;

namespace TicketSeller
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(ShowFilms), typeof(ShowFilms));
			Routing.RegisterRoute(nameof(Registration), typeof(Registration));
		}
	}
}
