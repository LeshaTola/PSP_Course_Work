using TicketSeller.ViewModel.UserOthers;

namespace TicketSeller.View.Others.UserOthers;

public partial class UserPage : ContentPage
{
	public UserPage(UserPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}