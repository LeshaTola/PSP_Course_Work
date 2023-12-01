using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketSeller.View.Others.UserOthers;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.UserOthers
{
	public partial class AboutMeViewModel : BaseViewModel
	{
		[ObservableProperty] private User me;

		public AboutMeViewModel()
		{
			Me = Client.Client.Instance.CurrentUser;
			Title = "Обо мне";
		}

		public async Task GoToAddElementPageAsync(User user)
		{
			await Shell.Current.GoToAsync($"{nameof(EditUserForUser)}", true, new Dictionary<string, object>
			{
				{"User", user}
			});
		}

		[RelayCommand]
		public async Task UpsertElementAsync()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				await GoToAddElementPageAsync(Me);
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
