using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Cinemas
{
	public partial class CinemasViewModel : BaseViewModel, ICrudViewModel<Cinema>
	{
		public ObservableCollection<Cinema> Cinemas { get; private set; } = new();

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
	}
}
