using TicketSeller.ViewModel.UserOthers;

namespace TicketSeller.View.Others.UserOthers;
public partial class EditUserForUser : ContentPage
{
	public EditUserForUser(EditUserForUserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}