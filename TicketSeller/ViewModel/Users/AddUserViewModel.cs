using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketSeller.Services;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;

namespace TicketSeller.ViewModel.Users
{
	[QueryProperty("User", "User")]
	public partial class AddUserViewModel : BaseViewModel
	{
		[ObservableProperty] private User user;

		private UserService service;

		public AddUserViewModel(UserService service)
		{
			this.service = service;
			Title = "Редактирование пользователя";
		}


		[RelayCommand]
		private async Task UpsertUserAsync()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				if (!await service.CheckUserPropertiesAsync(User)) return;

				var response = await service.UpsertAsync(User);

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
