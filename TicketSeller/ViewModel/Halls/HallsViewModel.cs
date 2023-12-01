using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Halls
{
	public partial class HallsViewModel : BaseViewModel, ICrudViewModel<Hall>
	{
		public ObservableCollection<Hall> ValidHalls { get; private set; } = new();

		[ObservableProperty] private string searchString;
		[ObservableProperty] private List<string> orderTypesList = new() { "От А до Я", "От Я до А" };
		[ObservableProperty] private int orderTypeId = (int)HallOrderType.NameFromAToZ;

		private List<Hall> halls;
		private HallService service;

		public HallsViewModel(HallService service)
		{
			Title = "Залы";
			this.service = service;
		}

		[RelayCommand]
		public async Task LoadElementsAsync()
		{
			try
			{
				List<Hall> loadedHalls = await service.GetAllAsync();
				if (loadedHalls.Count == 0)
					return;

				halls = loadedHalls;
				ValidateElements();
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
			}
		}

		public async Task GoToAddElementPageAsync(Hall element)
		{
			await Shell.Current.GoToAsync($"{nameof(AddHall)}", true, new Dictionary<string, object>
			{
				{"Hall", element}
			});
		}

		[RelayCommand]
		public async Task UpsertElementAsync(Hall element)
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
			List<Hall> searchedElements = SearchElements(halls);
			var searchedAndOrderedElements = OrderElements(searchedElements);

			ValidHalls.Clear();
			foreach (var element in searchedAndOrderedElements)
			{
				ValidHalls.Add(element);
			}
		}

		private List<Hall> SearchElements(List<Hall> unsearchedElements)
		{
			if (string.IsNullOrEmpty(SearchString))
			{
				return unsearchedElements;
			}

			List<Hall> searchedElements = new();
			searchedElements = unsearchedElements.Where(s => s.Name.Contains(SearchString)).ToList();
			return searchedElements;
		}

		private List<Hall> OrderElements(List<Hall> unorderedElements)
		{
			List<Hall> orderedElements = new();
			var orderType = (HallOrderType)OrderTypeId;
			switch (orderType)
			{
				case HallOrderType.NameFromAToZ:
					orderedElements = unorderedElements.OrderBy((h) => h.Name).ToList();
					break;
				case HallOrderType.NameFromZToA:
					orderedElements = unorderedElements.OrderByDescending((h) => h.Name).ToList();
					break;
			}
			return orderedElements;
		}
	}
}
