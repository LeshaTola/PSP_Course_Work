using CommunityToolkit.Mvvm.Input;
using TicketSeller.View;

namespace TicketSeller.ViewModel
{
	public partial class AdminPageViewModel : BaseViewModel
	{
		public AdminPageViewModel()
		{
			Title = "Панель администратора";
		}

		[RelayCommand]
		private async Task GoToFilmsPageAsync()
		{
			await Shell.Current.GoToAsync($"{nameof(FilmsPage)}", true);
		}

	}
}
