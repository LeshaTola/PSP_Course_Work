namespace TicketSeller
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			//MainPage = new Authorization(new AuthorizationViewModel(new Services.UserService()));
			MainPage = new AppShell();
		}
	}
}
