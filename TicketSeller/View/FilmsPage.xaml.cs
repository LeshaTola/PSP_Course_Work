using TicketSeller.ViewModel;

namespace TicketSeller.View;

public partial class FilmsPage : ContentPage
{
	public FilmsPage(FilmsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}