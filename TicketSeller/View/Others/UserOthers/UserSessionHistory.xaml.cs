using TicketSeller.ViewModel.Others.UserOthers;

namespace TicketSeller.View.Others.UserOthers;

public partial class UserSessionHistory : ContentPage
{
	public UserSessionHistory(UserSessionHistoryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}