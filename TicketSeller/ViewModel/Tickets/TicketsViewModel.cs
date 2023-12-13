using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Tickets
{
	public partial class TicketsViewModel : BaseViewModel, ICrudViewModel<Ticket>
	{
		[ObservableProperty] private string searchString;
		[ObservableProperty]
		private List<string> orderTypesList
			= new() { "От А до Я (Кинотеатр)", "От Я до А(Кинотеатр)", "От А до Я (Фильм)", "От Я до А(Фильм)", "Цена (убывание)", "Цена (возрастание)" };
		[ObservableProperty] private int orderTypeId = (int)SessionsOrderType.NameFromAToZCinema;

		public ObservableCollection<Ticket> ValidTickets { get; private set; } = new();

		private List<Ticket> tickets = new();
		private TicketService service;

		public TicketsViewModel(TicketService service)
		{
			Title = "Билеты";
			this.service = service;
		}


		[RelayCommand]
		public async Task DeleteElementAsync(int id)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				var response = await service.DeleteAsync(id);
				await LoadElementsAsync();
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

		[RelayCommand]
		public async Task GoToAddElementPageAsync(Ticket element)
		{
			await Shell.Current.GoToAsync($"{nameof(AddTicket)}", true, new Dictionary<string, object>
			{
				{"Ticket", element}
			});
		}

		[RelayCommand]
		public async Task LoadElementsAsync()
		{
			List<Ticket> loadedTickets = await service.GetAllAsync();
			if (loadedTickets.Count == 0)
				return;

			tickets = loadedTickets;
			ValidateElements();
		}

		[RelayCommand]
		public async Task UpsertElementAsync(Ticket element)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				element ??= new();
				await GoToAddElementPageAsync(element);
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
