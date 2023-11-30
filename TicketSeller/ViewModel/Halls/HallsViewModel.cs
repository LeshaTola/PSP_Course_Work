using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Halls
{
	public partial class HallsViewModel : BaseViewModel, ICrudViewModel<Hall>
	{
		public ObservableCollection<Hall> Halls { get; private set; } = new();

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
				List<Hall> halls = await service.GetAllAsync();
				if (halls.Count == 0)
					return;

				Halls.Clear();
				foreach (Hall hall in halls)
				{
					Halls.Add(hall);
				}
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
	}
}
