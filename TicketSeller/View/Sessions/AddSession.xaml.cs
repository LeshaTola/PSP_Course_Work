using TicketSeller.ViewModel.Sessions;

namespace TicketSeller.View;

public partial class AddSession : ContentPage
{
	public AddSession(AddSessionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}