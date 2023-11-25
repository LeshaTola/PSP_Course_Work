using TicketSeller.ViewModel;

namespace TicketSeller.View;

public partial class Registration : ContentPage
{
	public Registration(RegistrationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}