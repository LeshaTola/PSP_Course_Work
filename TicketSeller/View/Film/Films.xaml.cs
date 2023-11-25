using TicketSeller.ViewModel;

namespace TicketSeller.View;

public partial class Films : ContentPage
{
	public Films(FilmsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}