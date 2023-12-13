using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Sessions
{
	public partial class SessionsViewModel : BaseViewModel, ICrudViewModel<Session>
	{
		public ObservableCollection<Session> ValidSessions { get; private set; } = new();

		[ObservableProperty] private string searchString;
		[ObservableProperty]
		private List<string> orderTypesList
			= new() { "От А до Я (Кинотеатр)", "От Я до А(Кинотеатр)", "От А до Я (Фильм)", "От Я до А(Фильм)", "Цена (убывание)", "Цена (возрастание)" };
		[ObservableProperty] private int orderTypeId = (int)SessionsOrderType.NameFromAToZCinema;

		private List<Session> sessions;
		private SessionService service;

		public SessionsViewModel(SessionService service)
		{
			Title = "Сеансы";
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
		public async Task GoToAddElementPageAsync(Session element)
		{
			await Shell.Current.GoToAsync($"{nameof(AddSession)}", true, new Dictionary<string, object>
			{
				{"Session", element}
			});
		}

		[RelayCommand]
		public async Task LoadElementsAsync()
		{
			List<Session> loadedSessions = await service.GetAllAsync();

			sessions = loadedSessions;
			ValidateElements();
		}

		[RelayCommand]
		public async Task UpsertElementAsync(Session element)
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
			List<Session> searchedElements = SearchElements(sessions);
			var searchedAndOrderedElements = OrderElements(searchedElements);

			ValidSessions.Clear();
			foreach (var element in searchedAndOrderedElements)
			{
				ValidSessions.Add(element);
			}
		}

		private List<Session> SearchElements(List<Session> unsearchedElements)
		{
			if (string.IsNullOrEmpty(SearchString))
			{
				return unsearchedElements;
			}

			List<Session> searchedElements = new();
			searchedElements = unsearchedElements.Where(s => s.Hall.Cinema.Name.Contains(SearchString)).ToList();
			return searchedElements;
		}

		private List<Session> OrderElements(List<Session> unorderedElements)
		{
			List<Session> orderedElements = new();
			var orderType = (SessionsOrderType)OrderTypeId;
			switch (orderType)
			{
				case SessionsOrderType.NameFromAToZCinema:
					orderedElements = unorderedElements.OrderBy((s) => s.Hall.Cinema.Name).ToList();
					break;
				case SessionsOrderType.NameFromZToACinema:
					orderedElements = unorderedElements.OrderByDescending((s) => s.Hall.Cinema.Name).ToList();
					break;
				case SessionsOrderType.NameFromAToZFilms:
					orderedElements = unorderedElements.OrderBy((s) => s.Film.Name).ToList();
					break;
				case SessionsOrderType.NameFromZToAFilms:
					orderedElements = unorderedElements.OrderByDescending((s) => s.Film.Name).ToList();
					break;
				case SessionsOrderType.CostDown:
					orderedElements = unorderedElements.OrderBy((s) => s.Film.Cost).ToList();
					break;
				case SessionsOrderType.CostUp:
					orderedElements = unorderedElements.OrderByDescending((s) => s.Film.Cost).ToList();
					break;
			}
			return orderedElements;
		}
	}
}
