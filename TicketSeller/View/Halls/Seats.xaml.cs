using TicketSeller.ViewModel.Halls;

namespace TicketSeller.View;

public partial class Seats : ContentPage
{
	private SeatsViewModel viewModel;

	public Seats(SeatsViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		CreateGrid();
	}

	private void CreateGrid()
	{
		int rows = viewModel.Session.Hall.Rows;
		int columns = viewModel.Session.Hall.Columns;

		for (int i = 0; i < rows; i++)
		{
			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
		}

		for (int i = 0; i < columns; i++)
		{
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
		}

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				var button = new Button { Text = $"{j + 1}" };
				button.Command = viewModel.ChooseSeatsCommand;
				button.CommandParameter = i * columns + j;
				grid.Add(button, j, i);
			}
		}
	}
}