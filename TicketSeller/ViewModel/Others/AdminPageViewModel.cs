using CommunityToolkit.Mvvm.Input;

namespace TicketSeller.ViewModel
{
	public partial class AdminViewModel : BaseViewModel
	{
		public AdminViewModel()
		{
			Title = "Панель администратора";
		}

		[RelayCommand]
		private async Task GoToFilmsPageAsync()
		{
			if (IsBusy) return;
			try
			{
				IsBusy = true;
				await Shell.Current.GoToAsync($"{nameof(Films)}", true);

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
		private async Task GoToUsersPageAsync()
		{
			if (IsBusy) return;
			try
			{
				IsBusy = true;
				await Shell.Current.GoToAsync($"{nameof(Users)}", true);

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
