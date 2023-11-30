using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketSeller.Services;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;

namespace TicketSeller.ViewModel.Cinemas
{
	[QueryProperty("Cinema", "Cinema")]
	public partial class AddCinemaViewModel : BaseViewModel
	{
		[ObservableProperty] private Cinema cinema;

		private CinemaService service;

		public AddCinemaViewModel(CinemaService service)
		{
			this.service = service;
			Title = "Редактирование кинотеатра";
		}


		[RelayCommand]
		private async Task UpsertCinemaAsync()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				var response = await service.UpsertAsync(Cinema);

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
