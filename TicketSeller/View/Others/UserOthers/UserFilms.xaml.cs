using TicketSeller.ViewModel.Others.UserOthers;

namespace TicketSeller.View.Others.UserOthers;

public partial class UserFilms : ContentPage
{
	public UserFilms(UserFilmsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}