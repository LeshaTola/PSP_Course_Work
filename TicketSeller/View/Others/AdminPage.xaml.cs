using TicketSeller.ViewModel;

namespace TicketSeller.View;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}