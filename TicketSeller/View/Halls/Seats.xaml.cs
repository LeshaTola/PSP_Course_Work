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

		for (int i = 0; i < columns + 1; i++)
		{
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
		}

		for (int i = 0; i < rows; i++)
		{
			grid.Add(
				new Label
				{
					Text = $"Ðÿä {i + 1}",
					FontAttributes = FontAttributes.Bold,
					VerticalTextAlignment = TextAlignment.Center,
				}, 0, i);
			for (int j = 1; j < columns; j++)
			{
				var button = new Button { Text = $"{j}" };
				button.Command = viewModel.ChooseSeatsCommand;
				button.CommandParameter = i * columns + (j - 1);

				var ticket = viewModel.Tickets.FirstOrDefault(t => t.Row == i && t.Column == (j - 1));
				if (ticket != null)
				{
					button.IsEnabled = false;
				}
				grid.Add(button, j, i);
			}
		}
	}
}