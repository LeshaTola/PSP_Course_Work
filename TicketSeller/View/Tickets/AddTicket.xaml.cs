using TicketSeller.ViewModel.Tickets;

namespace TicketSeller.View;

public partial class AddCinema : ContentPage
{
	public AddCinema(AddTicketViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}