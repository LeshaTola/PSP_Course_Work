using TicketSeller.ViewModel.Cinemas;

namespace TicketSeller.View;

public partial class AddCinema : ContentPage
{
	public AddCinema(AddCinemaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}