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
			Films.Clear();

			List<Film> films = await service.GetActualFilmsAsync();
			if (films == null)
			{
				return;
			}

			foreach (Film film in films)
			{
				Films.Add(film);
			}
		}

		[RelayCommand]
		private async Task GoToAddFilmPageAsync()
		{
			await Shell.Current.GoToAsync($"{nameof(AddFilm)}", true);
		}
	}
}
