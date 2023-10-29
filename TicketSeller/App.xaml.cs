using TicketSeller.View;
using TicketSeller.ViewModel;

namespace TicketSeller
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new Authorization(new AuthorizationViewModel(new Services.UserService()));
			Client.Client client = new("127.0.0.1", 14447);
			client.Connect();
		}
	}
}
