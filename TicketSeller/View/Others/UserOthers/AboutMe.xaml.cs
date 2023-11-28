using TicketSeller.ViewModel.Users;

namespace TicketSeller.View.Others.UserOthers;

public partial class AboutMe : ContentPage
{
	public AboutMe(AddUserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}