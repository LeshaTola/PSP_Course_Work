using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Others.UserOthers
{
	public partial class UserSessionHistoryViewModel : BaseViewModel
	{
		[ObservableProperty] private string searchString;
		[ObservableProperty]
		private List<string> orderTypesList
			= new() { "От А до Я (Кинотеатр)", "От Я до А(Кинотеатр)", "От А до Я (Фильм)", "От Я до А(Фильм)", "Цена (убывание)", "Цена (возрастание)" };
		[ObservableProperty] private int orderTypeId = (int)SessionsOrderType.NameFromAToZCinema;

		public ObservableCollection<Ticket> ValidTickets { get; private set; } = new();

		private List<Ticket> tickets = new();
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
				List<Ticket> loadedTickets = await service.GetAllAsync();
				if (loadedTickets.Count == 0)
					return;

				loadedTickets = loadedTickets.Where(t => t.UserId == Client.Client.Instance.CurrentUser.Id).ToList();
				tickets = loadedTickets;
				ValidateElements();
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
			}
		}

		[RelayCommand]
		private void ValidateElements()
		{
			List<Ticket> searchedElements = SearchElements(tickets);
			var searchedAndOrderedElements = OrderElements(searchedElements);

			ValidTickets.Clear();
			foreach (var element in searchedAndOrderedElements)
			{
				ValidTickets.Add(element);
			}
		}

		private List<Ticket> SearchElements(List<Ticket> unsearchedElements)
		{
			if (string.IsNullOrEmpty(SearchString))
			{
				return unsearchedElements;
			}

			List<Ticket> searchedElements = new();
			searchedElements = unsearchedElements.Where(t => t.Session.Film.Name.Contains(SearchString)).ToList();
			return searchedElements;
		}

		private List<Ticket> OrderElements(List<Ticket> unorderedElements)
		{
			List<Ticket> orderedElements = new();
			var orderType = (SessionsOrderType)OrderTypeId;
			switch (orderType)
			{
				case SessionsOrderType.NameFromAToZCinema:
					orderedElements = unorderedElements.OrderBy((t) => t.Session.Hall.Cinema.Name).ToList();
					break;
				case SessionsOrderType.NameFromZToACinema:
					orderedElements = unorderedElements.OrderByDescending((t) => t.Session.Hall.Cinema.Name).ToList();
					break;
				case SessionsOrderType.NameFromAToZFilms:
					orderedElements = unorderedElements.OrderBy((t) => t.Session.Film.Name).ToList();
					break;
				case SessionsOrderType.NameFromZToAFilms:
					orderedElements = unorderedElements.OrderByDescending((t) => t.Session.Film.Name).ToList();
					break;
				case SessionsOrderType.CostDown:
					orderedElements = unorderedElements.OrderBy((t) => t.Session.Film.Cost).ToList();
					break;
				case SessionsOrderType.CostUp:
					orderedElements = unorderedElements.OrderByDescending((t) => t.Session.Film.Cost).ToList();
					break;
			}
			return orderedElements;
		}
	}
}
