using TicketSeller.ViewModel.Cinemas;

namespace TicketSeller.View;

public partial class Cinemas : ContentPage
{
	public Cinemas(CinemasViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}