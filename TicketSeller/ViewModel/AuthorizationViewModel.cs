using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Diagnostics;
using TicketSeller.Services;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;

namespace TicketSeller.ViewModel
{
	partial class AuthorizationViewModel : BaseViewModel
	{

		[ObservableProperty] private User user = new();
		[ObservableProperty] private string errorString;

		private UserService userService;

		public AuthorizationViewModel(UserService userService)
		{
			Title = "Authorization";
			this.userService = userService;
		}

		[RelayCommand]
		private async Task LoginAsync()
		{
			if (IsBusy)
				return;

			try
			{
				IsBusy = true;

				var response = await userService.LoginAsync(User);
				if (response != null)
				{
					if (response.Type == ResponseTypes.Ok)
					{
						User responseUser = JsonConvert.DeserializeObject<User>(response.Data);
					}
					else
					{
						ErrorString = response.Message;
						//await Shell.Current.DisplayAlert("Error!", "message", "Ok");
					}
				}
			}
			catch (Exception ex)
			{
				//ErrorString = ex.Message;//TODO: Handle
				Debug.WriteLine(ex.Message);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
