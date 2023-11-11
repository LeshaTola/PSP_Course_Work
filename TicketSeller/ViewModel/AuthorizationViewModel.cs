using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;

namespace TicketSeller.ViewModel
{
	public partial class AuthorizationViewModel : BaseViewModel
	{

		[ObservableProperty] private User user = new();

		private UserService userService;

		public AuthorizationViewModel(UserService userService)
		{
			Title = "Authorization";
			this.userService = userService;
		}

		private async Task GoToRegistrationPage()
		{
			await Shell.Current.GoToAsync($"{nameof(Registration)}", true);
		}

		private async Task GoToFilmsPageAsync(User user)
		{
			if (user == null)
				return;

			await Shell.Current.GoToAsync($"{nameof(ShowFilms)}", true, new Dictionary<string, object>
			{
				{"user", User}
			});
		}

		[RelayCommand]
		private async Task RegisterAsync()
		{
			await GoToRegistrationPage();
		}

		[RelayCommand]
		private async Task LoginAsync()
		{
			if (IsBusy)
				return;

			try
			{
				IsBusy = true;

				var response = await userService.AuthorizeAsync(User);
				if (response != null)
				{
					if (response.Type == ResponseTypes.Ok)
					{
						User responseUser = JsonConvert.DeserializeObject<User>(response.Data);
						await GoToFilmsPageAsync(responseUser);
					}
					else
					{
						await Shell.Current.DisplayAlert("Error!", response.Message, "Ok");
					}
				}
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Error!", ex.Message, "Ok");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
