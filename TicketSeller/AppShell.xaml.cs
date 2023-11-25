using TicketSeller.View;

namespace TicketSeller
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(AddFilm), typeof(AddFilm));
			Routing.RegisterRoute(nameof(Films), typeof(Films));

			Routing.RegisterRoute(nameof(Registration), typeof(Registration));
			Routing.RegisterRoute(nameof(Admin), typeof(Admin));
		}
	}
}
