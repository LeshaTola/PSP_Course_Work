using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Films
{
	public partial class FilmsViewModel : BaseViewModel
	{
		public ObservableCollection<Film> Films { get; private set; } = new();

		private FilmService service;

		public FilmsViewModel(FilmService service)
		{
			Title = "Фильмы";
			this.service = service;
			LoadFilms();
		}

		public async void LoadFilms()
		{
			List<Film> films = await service.GetAllAsync();

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
			film = film ??= new();
			await Shell.Current.GoToAsync($"{nameof(AddFilm)}", true, new Dictionary<string, object>
			{
				{"Film", film}
			});
			LoadFilms();
		}

		[RelayCommand]
		private async Task DeleteFilmAsync(int id)
		{
			await service.DeleteAsync(id);
			LoadFilms();
		}
	}
}
