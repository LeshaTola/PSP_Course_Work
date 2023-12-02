using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketSeller.Services;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;

namespace TicketSeller.ViewModel
{
	public partial class RegistrationViewModel : BaseViewModel
	{
		[ObservableProperty] private User user = new();
		[ObservableProperty] private string confirmPassword = "";

		private UserService userService;

		public RegistrationViewModel(UserService userService)
		{
			Title = "Регистрация";
			this.userService = userService;
		}

		[RelayCommand]
		private async Task RegisterAsync()
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				IsBusy = true;

				if (!await userService.CheckUserPropertiesAsync(User))
					return;

				if (!IsPasswordConfirmed())
				{
					await Shell.Current.DisplayAlert("Ошибка!", "Пароли не совпадают", "Хорошо");
					return;
				}

				var response = await userService.UpsertAsync(User);
				if (response.Type == ResponseTypes.Ok)
				{
					await GoToFilmsPageAsync(User);
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

		private async Task GoToFilmsPageAsync(User user)
		{
			if (user == null)
				return;

			await Shell.Current.GoToAsync($"{nameof(Films)}", true, new Dictionary<string, object>
			{
				{"user", User}
			});
		}

		private bool IsPasswordConfirmed()
		{
			return User.Password.Equals(ConfirmPassword);
		}
	}
}
