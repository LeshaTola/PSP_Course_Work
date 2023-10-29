using TicketSeller.ViewModel;

namespace TicketSeller.View;

public partial class Authorization : ContentPage
{
	internal Authorization(AuthorizationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}