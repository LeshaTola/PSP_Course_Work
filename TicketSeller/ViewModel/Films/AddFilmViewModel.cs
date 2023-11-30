using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketSeller.Services;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;

namespace TicketSeller.ViewModel.Films
{
	[QueryProperty("Film", "Film")]
	public partial class AddFilmViewModel : BaseViewModel
	{
		[ObservableProperty] private Film film;

		private FilmService service;

		public AddFilmViewModel(FilmService service)
		{
			Title = "Редактирование фильма";
			this.service = service;
		}

		[RelayCommand]
		private async Task UpsertFilmAsync()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				var response = await service.UpsertAsync(Film);

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
