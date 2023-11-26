using TicketSeller.ViewModel.Halls;

namespace TicketSeller.View;

public partial class AddHall : ContentPage
{
	public AddHall(AddHallViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}