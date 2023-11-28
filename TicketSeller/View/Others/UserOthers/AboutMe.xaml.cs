using TicketSeller.ViewModel.UserOthers;

namespace TicketSeller.View.Others.UserOthers;

public partial class AboutMe : ContentPage
{
	public AboutMe(AboutMeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}