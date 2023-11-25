using TicketSeller.ViewModel;

namespace TicketSeller.View;

public partial class Admin : ContentPage
{
	public Admin(AdminPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}