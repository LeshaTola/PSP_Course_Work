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
			Title = "Обо мне";
		}

		private async Task GoToAddElementPageAsync(User user)
		{
			await Shell.Current.GoToAsync($"{nameof(EditUserForUser)}", true, new Dictionary<string, object>
			{
				{"User", user}
			});
		}

		[RelayCommand]
		private async Task UpsertElementAsync()
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

		[RelayCommand]
		private async Task LoadMyInfo()
		{
			try
			{
				Me = null;
				Me = Client.Client.Instance.CurrentUser;
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
			}
		}

	}
}
