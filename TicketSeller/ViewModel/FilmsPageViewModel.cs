using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel
{
	public partial class FilmsPageViewModel : BaseViewModel
	{
		public ObservableCollection<Film> Films { get; private set; } = new();

		private FilmServices service;

		public FilmsPageViewModel(FilmServices service)
		{
			Title = "Фильмы";
			this.service = service;
			UpdateFilms();
		}

		public async void UpdateFilms()
		{
			List<Film> films = await service.GetActualFilmsAsync();

			if (films.Count == 0)
				return;


			Films.Clear();
			foreach (Film film in films)
			{
				Films.Add(film);
			}
		}

		[RelayCommand]
		private async Task GoToAddFilmPageAsync(Film film)
		{
			await Shell.Current.GoToAsync($"{nameof(AddFilm)}", true, new Dictionary<string, object>
			{
				{"Film", film}
			});
			UpdateFilms();
		}

		[RelayCommand]
		private async Task DeleteFilmAsync(int id)
		{
			await service.DeleteFilmAsync(id);
			UpdateFilms();
		}
	}
}
