using TicketSeller.ViewModel;

namespace TicketSeller.View;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}