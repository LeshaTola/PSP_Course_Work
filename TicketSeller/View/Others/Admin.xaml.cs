using TicketSeller.ViewModel;

namespace TicketSeller.View;

public partial class Admin : ContentPage
{
	public Admin(AdminViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}