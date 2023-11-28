using TicketSeller.ViewModel.UserOthers;

namespace TicketSeller.View.Others.UserOthers;

public partial class UserPanel : ContentPage
{
	public UserPanel(UserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}