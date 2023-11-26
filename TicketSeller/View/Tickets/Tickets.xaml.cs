using TicketSeller.ViewModel.Tickets;

namespace TicketSeller.View;

public partial class Tickets : ContentPage
{
	public Tickets(TicketsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}