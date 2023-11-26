using TicketSeller.ViewModel.Halls;

namespace TicketSeller.View;

public partial class Halls : ContentPage
{
	public Halls(HallsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}