using TicketSeller.ViewModel.Films;

namespace TicketSeller.View;

public partial class Films : ContentPage
{
	public Films(FilmsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}