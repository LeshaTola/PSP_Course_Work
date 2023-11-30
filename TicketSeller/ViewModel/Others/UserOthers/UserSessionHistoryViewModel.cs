using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Others.UserOthers
{
	public partial class UserSessionHistoryViewModel : BaseViewModel
	{
		public ObservableCollection<Ticket> Tickets { get; private set; } = new();

		private TicketService service;

		public UserSessionHistoryViewModel(TicketService service)
		{
			Title = "История сеансов";
			this.service = service;
		}

		[RelayCommand]
		private async Task LoadElementsAsync()
		{
			try
			{
				List<Ticket> tickets = await service.GetAllAsync();
				if (tickets.Count == 0)
					return;

				tickets = tickets.Where(t => t.UserId == Client.Client.Instance.CurrentUser.Id).ToList();

				Tickets.Clear();
				foreach (var ticket in tickets)
				{
					Tickets.Add(ticket);
				}
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
			}
		}

	}
}
