using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketSeller.Services;
using TicketSeller.View;
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

				if (!await CheckUserPropertiesAsync())
					return;

				var response = await userService.RegisterAsync(User);
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

		private async Task<bool> CheckUserPropertiesAsync()
		{
			if (!await CheckLoginAsync())
				return false;

			if (!await CheckPasswordAsync())
				return false;

			return true;
		}

		private async Task<bool> CheckLoginAsync()
		{
			int minLoginLength = 4;
			if (User.Login.Length < minLoginLength)
			{
				await Shell.Current.DisplayAlert("Ошибка!", $"Логин должен состоять из как минимум {minLoginLength} символов", "Хорошо");
				return false;
			}

			int maxLoginLength = 20;
			if (User.Login.Length > maxLoginLength)
			{
				await Shell.Current.DisplayAlert("Ошибка!", $"Логин слишком длинный! Он не должен быть больше {maxLoginLength} символов", "Хорошо");
				return false;
			}
			return true;
		}

		private async Task<bool> CheckPasswordAsync()
		{
			int minPasswordLength = 4;
			if (User.Password.Length < minPasswordLength)
			{
				await Shell.Current.DisplayAlert("Ошибка!", $"Пароль должен состоять из как минимум {minPasswordLength} символов", "Хорошо");
				return false;
			}

			int maxPasswordLength = 20;
			if (User.Password.Length > maxPasswordLength)
			{
				await Shell.Current.DisplayAlert("Ошибка!", $"Пароль слишком длинный! Он не должен быть больше {maxPasswordLength} символов", "Хорошо");
				return false;
			}

			if (!IsPasswordConfirmed())
			{
				await Shell.Current.DisplayAlert("Ошибка!", "Пароли не совпадают", "Хорошо");
				return false;
			}
			return true;
		}
	}
}
