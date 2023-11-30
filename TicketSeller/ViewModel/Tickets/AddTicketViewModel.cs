using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketSeller.Services;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;

namespace TicketSeller.ViewModel.Tickets
{
	[QueryProperty("Ticket", "Ticket")]
	public partial class AddTicketViewModel : BaseViewModel
	{
		[ObservableProperty] private Ticket ticket;

		[ObservableProperty] private List<Session> sessions;


		private SessionService sessionService;
		private TicketService ticketService;

		public AddTicketViewModel(SessionService sessionService, TicketService ticketService)
		{
			this.ticketService = ticketService;
			this.sessionService = sessionService;
			Title = "Редактирование Билета";
			_ = LoadSessions();
		}

		private async Task LoadSessions()
		{
			Sessions = await sessionService.GetAllAsync();
		}

		[RelayCommand]
		private async Task UpsertAsync()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				var response = await ticketService.UpsertAsync(Ticket);

				if (response.Type == ResponseTypes.Ok)
				{
					await Shell.Current.DisplayAlert("Внимание!", response.Message, "Хорошо");
					await Shell.Current.Navigation.PopAsync(true);
				}
				else
				{
					await Shell.Current.DisplayAlert("Ошибка!", response.Message, "Хорошо");
				}

			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
