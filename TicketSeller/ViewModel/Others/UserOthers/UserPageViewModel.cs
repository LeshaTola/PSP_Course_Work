using CommunityToolkit.Mvvm.Input;
using TicketSeller.View.Others.UserOthers;

namespace TicketSeller.ViewModel.UserOthers
{
	public partial class UserPageViewModel : BaseViewModel
	{
		public UserPageViewModel()
		{
			Title = "Меню пользователя";
		}


		[RelayCommand]
		private async Task GoToAboutMePageAsync()
		{
			if (IsBusy) return;
			try
			{
				IsBusy = true;
				await Shell.Current.GoToAsync($"{nameof(AboutMe)}", true);

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
		private async Task GoToUserFilmsPageAsync()
		{
			if (IsBusy) return;
			try
			{
				IsBusy = true;
				await Shell.Current.GoToAsync($"{nameof(UserFilms)}", true);

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
		private async Task GoToUserHistoryPageAsync()
		{
			if (IsBusy) return;
			try
			{
				IsBusy = true;
				await Shell.Current.GoToAsync($"{nameof(UserSessionHistory)}", true);

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
