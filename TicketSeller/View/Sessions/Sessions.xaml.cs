using TicketSeller.ViewModel.Sessions;

namespace TicketSeller.View;

public partial class Sessions : ContentPage
{
	public Sessions(SessionsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}