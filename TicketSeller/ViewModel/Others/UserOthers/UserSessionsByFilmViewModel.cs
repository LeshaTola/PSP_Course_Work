using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Others.UserOthers
{
	[QueryProperty("Film", "Film")]
	public partial class UserSessionsByFilmViewModel : BaseViewModel
	{
		[ObservableProperty] private Film film;
		[ObservableProperty] private string searchString;
		[ObservableProperty]
		private List<string> orderTypesList
			= new() { "От А до Я (Кинотеатр)", "От Я до А(Кинотеатр)", "От А до Я (Фильм)", "От Я до А(Фильм)", "Цена (убывание)", "Цена (возрастание)" };
		[ObservableProperty] private int orderTypeId = (int)SessionsOrderType.NameFromAToZCinema;

		public ObservableCollection<Session> ValidSessions { get; private set; } = new();

		private List<Session> sessions;
		private SessionService service;

		public UserSessionsByFilmViewModel(SessionService service)
		{
			this.service = service;
			Title = "Сеансы на фильм";
		}

		[RelayCommand]
		private async Task LoadElementsAsync()
		{
			List<Session> loadedSessions = await service.GetAllAsync();

			if (loadedSessions.Count == 0)
				return;

			loadedSessions = loadedSessions.Where(session => session.FilmId == Film.Id).ToList();
			sessions = loadedSessions;
			ValidateElements();
		}

		[RelayCommand]
		private async Task ChooseSession(Session session)
		{
			try
			{
				IsBusy = true;
				await GoToChooseSeats(session);
			}
			catch (Exception ex) { await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо"); }
			finally { IsBusy = false; }
		}

		private async Task GoToChooseSeats(Session session)
		{
			await Shell.Current.GoToAsync(nameof(Seats), true, new Dictionary<string, object>
			{
				{"Session", session }
			});
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
