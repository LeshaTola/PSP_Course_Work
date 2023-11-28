using TicketSeller.ViewModel.Films;

namespace TicketSeller.View;

public partial class AddFilm : ContentPage
{
	public AddFilm(AddFilmViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}