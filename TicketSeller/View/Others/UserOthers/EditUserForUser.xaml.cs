using TicketSeller.ViewModel.Users;

namespace TicketSeller.View.Others.UserOthers;
public partial class EditUserForUser : ContentPage
{
	public EditUserForUser(AddUserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}