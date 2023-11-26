using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Films
{
	public partial class FilmsViewModel : BaseViewModel, ICrudViewModel<Film>
	{
		public ObservableCollection<Film> Films { get; private set; } = new();

		private FilmService service;

		public FilmsViewModel(FilmService service)
		{
			Title = "Фильмы";
			this.service = service;
			_ = LoadElementsAsync();
		}

		[RelayCommand]
		public async Task LoadElementsAsync()
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
		public async Task UpsertElementAsync(Film element)
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

		public async Task GoToAddElementPageAsync(Film film)
		{
			await Shell.Current.GoToAsync($"{nameof(AddFilm)}", true, new Dictionary<string, object>
			{
				{"Film", film}
			});
		}

		[RelayCommand]
		public async Task DeleteElementAsync(int id)
		{
			await service.DeleteAsync(id);
			await LoadElementsAsync();
		}
	}
}
