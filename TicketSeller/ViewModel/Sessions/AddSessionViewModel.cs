using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketSeller.Services;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;

namespace TicketSeller.ViewModel.Sessions
{
	[QueryProperty("Session", "Session")]
	public partial class AddSessionViewModel : BaseViewModel
	{
		[ObservableProperty] private Session session;
		[ObservableProperty] private List<string> filmNames;
		[ObservableProperty] private List<string> cinemaNames;
		[ObservableProperty] private List<string> hallNames;

		[ObservableProperty] private int filmId = -1;
		[ObservableProperty] private int cinemaId = -1;
		[ObservableProperty] private int hallId = -1;
		[ObservableProperty] private DateTime dummyDateTime = DateTime.Today;

		private List<Film> films;
		private List<Cinema> cinemas;
		private List<Hall> halls;

		private HallService hallService;
		private CinemaService cinemaService;
		private FilmService filmService;
		private SessionService sessionService;

		public AddSessionViewModel(HallService hallService, FilmService filmService, SessionService sessionService, CinemaService cinemaService)
		{
			this.hallService = hallService;
			this.filmService = filmService;
			this.sessionService = sessionService;
			this.cinemaService = cinemaService;
			Title = "Редактирование сеанса";
			_ = LoadAtOneTime();

		}


		private async Task LoadAtOneTime()
		{
			await LoadCinemas();
			await LoadFilms();
		}

		private async Task LoadCinemas()
		{
			cinemas = await cinemaService.GetAllAsync();
			CinemaNames = cinemas.Select(cinema => cinema.Name).ToList();
		}

		private async Task LoadHalls(Cinema cinema)
		{
			halls = await hallService.GetAllAsync();
			halls = halls.Where(hall => hall.Cinema.Id == cinema.Id).ToList();
			HallNames = halls.Select(hall => hall.Name).ToList();
		}

		private async Task LoadFilms()
		{
			films = await filmService.GetAllAsync();
			FilmNames = films.Select(film => film.Name).ToList();
		}

		[RelayCommand]
		private async Task UpsertAsync()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				Session.Date = DateOnly.FromDateTime(DummyDateTime);
				Session.Film = films[FilmId];
				Session.Hall = halls[HallId];
				var response = await sessionService.UpsertAsync(Session);

				if (response.Type == ResponseTypes.Ok)
				{
					await Shell.Current.DisplayAlert("Внимание!", response.Message, "Хорошо");
					await Shell.Current.Navigation.PopAsync(true);
				}
				else { await Shell.Current.DisplayAlert("Ошибка!", response.Message, "Хорошо"); }
			}
			catch (Exception ex) { await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо"); }
			finally { IsBusy = false; }
		}

		[RelayCommand]
		private async Task SelectedCinema()
		{
			await LoadHalls(cinemas[CinemaId]);
		}
	}
}
