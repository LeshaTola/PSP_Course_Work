using TicketSeller.ViewModel.Users;

namespace TicketSeller.View;

public partial class Users : ContentPage
{
	public Users(UsersViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}