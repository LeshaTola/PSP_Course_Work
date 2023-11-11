using TicketSeller.ViewModel;

namespace TicketSeller.View
{
	public partial class MainPage : ContentPage
	{
		public MainPage(MainPageViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = viewModel;
		}
	}
}
