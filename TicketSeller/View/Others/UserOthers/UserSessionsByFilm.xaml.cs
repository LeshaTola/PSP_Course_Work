using TicketSeller.ViewModel.Others.UserOthers;

namespace TicketSeller.View.Others.UserOthers;

public partial class UserSessionsByFilm : ContentPage
{
	public UserSessionsByFilm(UserSessionsByFilmViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}