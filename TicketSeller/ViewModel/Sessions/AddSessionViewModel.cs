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
		[ObservableProperty] private List<Film> films;
		[ObservableProperty] private List<Cinema> cinemas;
		[ObservableProperty] private List<Hall> halls;
		[ObservableProperty] private DateTime dummyDateTime = DateTime.Today;

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
			Title = "Редактирование";
			_ = LoadAtOneTime();

		}


		private async Task LoadAtOneTime()
		{
			await LoadCinemas();
			await LoadFilms();
		}

		private async Task LoadCinemas()
		{
			Cinemas = await cinemaService.GetAllAsync();
		}

		private async Task LoadHalls()
		{
			Halls = await hallService.GetAllAsync();
		}

		private async Task LoadFilms()
		{
			Films = await filmService.GetAllAsync();
		}

		[RelayCommand]
		private async Task UpsertAsync()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				Session.Date = DateOnly.FromDateTime(DummyDateTime);
				var response = await sessionService.UpsertAsync(Session);

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

		[RelayCommand]
		private async Task SelectedFilm()
		{
			await LoadHalls();
		}
	}
}
