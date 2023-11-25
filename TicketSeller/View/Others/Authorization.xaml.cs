using TicketSeller.ViewModel;

namespace TicketSeller.View;

public partial class Authorization : ContentPage
{
	public Authorization(AuthorizationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}