using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Films
{

	public enum FilmOrderType
	{
		NameFromAToZ,
		NameFromZToA,
		CostDown,
		CostUp,
	}

	public partial class FilmsViewModel : BaseViewModel, ICrudViewModel<Film>
	{
		public ObservableCollection<Film> ValidFilms { get; private set; } = new();

		[ObservableProperty] private string searchString;
		[ObservableProperty] private List<string> orderTypesList = new() { "От А до Я", "От Я до А", "Цена (убывание)", "Цена (возрастание)" };
		[ObservableProperty] private int orderTypeId = (int)FilmOrderType.NameFromAToZ;
		private List<Film> films;

		private FilmService service;

		public FilmsViewModel(FilmService service)
		{
			Title = "Фильмы";
			this.service = service;
		}

		[RelayCommand]
		public async Task LoadElementsAsync()
		{
			List<Film> loadedFilms = await service.GetAllAsync();

			if (loadedFilms.Count == 0)
				return;

			films = loadedFilms;
			ValidateElements();
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

		[RelayCommand]
		private void ValidateElements()
		{
			List<Film> searchedFilms = SearchElements(films);
			var searchedAndOrderedFilms = OrderElements(searchedFilms);

			ValidFilms.Clear();
			foreach (Film film in searchedAndOrderedFilms)
			{
				ValidFilms.Add(film);
			}
		}

		private List<Film> SearchElements(List<Film> unsearchedFilms)
		{
			if (string.IsNullOrEmpty(SearchString))
			{
				return unsearchedFilms;
			}

			List<Film> searchedFilms = new();
			searchedFilms = unsearchedFilms.Where(f => f.Name.Contains(SearchString)).ToList();
			return searchedFilms;
		}

		private List<Film> OrderElements(List<Film> unorderedFilms)
		{
			List<Film> orderedFilms = new();
			var orderType = (FilmOrderType)OrderTypeId;
			switch (orderType)
			{
				case FilmOrderType.NameFromAToZ:
					orderedFilms = unorderedFilms.OrderBy((f) => f.Name).ToList();
					break;
				case FilmOrderType.NameFromZToA:
					orderedFilms = unorderedFilms.OrderByDescending((f) => f.Name).ToList();
					break;
				case FilmOrderType.CostUp:
					orderedFilms = unorderedFilms.OrderBy((f) => f.Cost).ToList();
					break;
				case FilmOrderType.CostDown:
					orderedFilms = unorderedFilms.OrderByDescending((f) => f.Cost).ToList();
					break;
			}
			return orderedFilms;
		}

	}
}
