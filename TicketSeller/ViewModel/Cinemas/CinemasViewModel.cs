using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Cinemas
{
	public enum CinemaOrderType
	{
		NameFromAToZ,
		NameFromZToA,
	}

	public partial class CinemasViewModel : BaseViewModel, ICrudViewModel<Cinema>
	{
		public ObservableCollection<Cinema> ValidCinemas { get; private set; } = new();

		[ObservableProperty] private string searchString;
		[ObservableProperty] private List<string> orderTypesList = new() { "От А до Я", "От Я до А" };
		[ObservableProperty] private int orderTypeId = (int)CinemaOrderType.NameFromAToZ;

		private List<Cinema> cinemas;
		private CinemaService service;

		public CinemasViewModel(CinemaService service)
		{
			Title = "Кинотеатры";
			this.service = service;
		}

		[RelayCommand]
		public async Task LoadElementsAsync()
		{
			try
			{
				List<Cinema> loadedCinemas = await service.GetAllAsync();

				cinemas = loadedCinemas;
				ValidateElements();
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
			}
		}

		public async Task GoToAddElementPageAsync(Cinema cinema)
		{
			await Shell.Current.GoToAsync($"{nameof(AddCinema)}", true, new Dictionary<string, object>
			{
				{"Cinema", cinema}
			});
		}

		[RelayCommand]
		public async Task UpsertElementAsync(Cinema cinema)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				cinema ??= new();
				await GoToAddElementPageAsync(cinema);
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
		private void ValidateElements()
		{
			List<Cinema> searchedElements = SearchElements(cinemas);
			var searchedAndOrderedElements = OrderElements(searchedElements);

			ValidCinemas.Clear();
			foreach (var element in searchedAndOrderedElements)
			{
				ValidCinemas.Add(element);
			}
		}

		private List<Cinema> SearchElements(List<Cinema> unsearchedFilms)
		{
			if (string.IsNullOrEmpty(SearchString))
			{
				return unsearchedFilms;
			}

			List<Cinema> searchedFilms = new();
			searchedFilms = unsearchedFilms.Where(c => c.Name.Contains(SearchString)).ToList();
			return searchedFilms;
		}

		private List<Cinema> OrderElements(List<Cinema> unorderedFilms)
		{
			List<Cinema> orderedFilms = new();
			var orderType = (CinemaOrderType)OrderTypeId;
			switch (orderType)
			{
				case CinemaOrderType.NameFromAToZ:
					orderedFilms = unorderedFilms.OrderBy((f) => f.Name).ToList();
					break;
				case CinemaOrderType.NameFromZToA:
					orderedFilms = unorderedFilms.OrderByDescending((f) => f.Name).ToList();
					break;
			}
			return orderedFilms;
		}
	}
}
