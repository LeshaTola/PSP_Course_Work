using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Cinemas
{
	public partial class CinemasViewModel : BaseViewModel
	{
		public ObservableCollection<Cinema> Cinemas { get; private set; } = new();

		private CinemaService service;

		public CinemasViewModel(CinemaService service)
		{
			Title = "Кинотеатры";
			this.service = service;
			_ = LoadCinemasAsync();
		}

		[RelayCommand]
		public async Task LoadCinemasAsync()
		{
			try
			{
				List<Cinema> cinemas = await service.GetAllAsync();
				if (cinemas.Count == 0)
					return;

				Cinemas.Clear();
				foreach (Cinema cinema in cinemas)
				{
					Cinemas.Add(cinema);
				}
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
			}
		}

		private async Task GoToAddCinemaPageAsync(Cinema cinema)
		{
			await Shell.Current.GoToAsync($"{nameof(AddCinema)}", true, new Dictionary<string, object>
			{
				{"Cinema", cinema}
			});
		}

		[RelayCommand]
		private async Task UpsertCinemaAsync(Cinema cinema)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				cinema ??= new();
				await GoToAddCinemaPageAsync(cinema);
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
		private async Task DeleteCinemaAsync(int id)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				var response = await service.DeleteAsync(id);
				await LoadCinemasAsync();
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
