using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketSeller.Services;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;

namespace TicketSeller.ViewModel
{
	public partial class AddFilmViewModel : BaseViewModel
	{
		[ObservableProperty] private Film film;

		private FilmServices service;

		public AddFilmViewModel(FilmServices service)
		{
			Title = "Добавить";
			this.service = service;
		}

		[RelayCommand]
		private async Task AddFilmAsync()
		{
			try
			{
				IsBusy = true;
				var response = await service.AddFilmAsync(Film);

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
				IsBusy = true;
			}
		}
	}
}
