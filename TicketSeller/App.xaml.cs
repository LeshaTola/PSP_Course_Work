namespace TicketSeller
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			//MainPage = new FilmsPage(new ViewModel.FilmsPageViewModel(new Services.FilmServices()));
			//MainPage = new AdminPage();
			MainPage = new AppShell();
		}
	}
}
