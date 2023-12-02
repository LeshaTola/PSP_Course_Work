using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketSeller.Services;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;

namespace TicketSeller.ViewModel.Halls
{
	[QueryProperty("Session", "Session")]
	public partial class SeatsViewModel : BaseViewModel
	{
		[ObservableProperty] private Session session;
		public List<Ticket> Tickets { get; private set; } = new();

		private TicketService service;

		public SeatsViewModel(TicketService service)
		{
			this.service = service;
			Title = "Места";
			LoadElements();
		}

		public async void LoadElements()
		{
			Tickets = await service.GetAllAsync();
			Tickets = Tickets.Where(t => t.Session.Id == Session.Id).ToList();
		}

		[RelayCommand]
		private async Task ChooseSeatsAsync(int seatId)
		{
			if (IsBusy)
			{
				return;
			}
			try
			{
				IsBusy = true;
				int row = seatId / Session.Hall.Columns;
				int column = seatId - Session.Hall.Columns * row;

				var ticket = new Ticket
				{
					Column = column,
					Row = row,
					Session = Session,
					User = Client.Client.Instance.CurrentUser
				};
				var response = await service.UpsertAsync(ticket);

				if (response.Type == ResponseTypes.Ok)
				{
					await Shell.Current.DisplayAlert("Внимание!", response.Message, "Хорошо");
					await Shell.Current.GoToAsync("../../..");
				}
				else
				{
					await Shell.Current.DisplayAlert("Ошибка!", response.Message, "Хорошо");
				}
			}
			catch (Exception ex)
			{ await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо"); }
			finally
			{ IsBusy = false; }
		}
	}
}
