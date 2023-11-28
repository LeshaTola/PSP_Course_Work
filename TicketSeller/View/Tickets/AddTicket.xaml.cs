using TicketSeller.ViewModel.Tickets;

namespace TicketSeller.View;

public partial class AddTicket : ContentPage
{
	public AddTicket(AddTicketViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}