using TicketSeller.ViewModel.Cinemas;

namespace TicketSeller.View;
public partial class AddUser : ContentPage
{
	public AddUser(AddCinemaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}