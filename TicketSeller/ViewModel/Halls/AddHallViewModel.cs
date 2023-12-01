using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketSeller.Services;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;

namespace TicketSeller.ViewModel.Halls
{
	[QueryProperty("Hall", "Hall")]
	public partial class AddHallViewModel : BaseViewModel
	{
		[ObservableProperty] private Hall hall;
		[ObservableProperty] private List<string> cinemaNames;
		[ObservableProperty] private int cinemaId = -1;

		private List<Cinema> cinemas;

		private HallService hallService;
		private CinemaService cinemaService;

		public AddHallViewModel(HallService hallService, CinemaService cinemaService)
		{
			this.hallService = hallService;
			this.cinemaService = cinemaService;
			Title = "Редактирование зала";
			_ = LoadCinemas();
		}

		private async Task LoadCinemas()
		{
			cinemas = await cinemaService.GetAllAsync();
			CinemaNames = cinemas.Select(c => c.Name).ToList();
		}

		[RelayCommand]
		private async Task UpsertAsync()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				Hall.Cinema = cinemas[CinemaId];
				var response = await hallService.UpsertAsync(Hall);

				if (response.Type == ResponseTypes.Ok)
				{
					await Shell.Current.DisplayAlert("Внимание!", response.Message, "Хорошо");
					await Shell.Current.Navigation.PopAsync(true);
				}
				else
				{
					await Shell.Current.DisplayAlert("Ошибка!", response.Message, "Хорошо");
				}

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
