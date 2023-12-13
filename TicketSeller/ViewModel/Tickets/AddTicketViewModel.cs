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

		private List<Session> sessions;
		private List<User> users;

		[ObservableProperty] private List<string> sessionNames;
		[ObservableProperty] private List<string> userNames;

		[ObservableProperty] private int sessionId;
		[ObservableProperty] private int userId;

		private SessionService sessionService;
		private UserService userService;

		private TicketService ticketService;

		public AddTicketViewModel(SessionService sessionService, TicketService ticketService, UserService userService)
		{
			this.ticketService = ticketService;
			this.sessionService = sessionService;
			this.userService = userService;

			Title = "Редактирование Билета";

			_ = LoadAllAsync();
		}
		private async Task LoadAllAsync()
		{
			await LoadSessionsAsync();
			await LoadUsersAsync();
		}

		private async Task LoadSessionsAsync()
		{
			sessions = await sessionService.GetAllAsync();
			SessionNames = sessions.Select(s => s.Film.Name + " " + s.Date + " " + s.Time).ToList();
		}

		private async Task LoadUsersAsync()
		{
			users = await userService.GetAllAsync();
			UserNames = users.Select(u => u.Login).ToList();
		}

		[RelayCommand]
		private async Task UpsertAsync()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				Ticket.Session = sessions[SessionId];
				Ticket.User = users[UserId];
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
