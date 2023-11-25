using TicketSeller.ViewModel.Users;

namespace TicketSeller.View;
public partial class AddUser : ContentPage
{
	public AddUser(AddUserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}