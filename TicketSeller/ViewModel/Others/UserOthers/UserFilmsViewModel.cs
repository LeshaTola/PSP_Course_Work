using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View.Others.UserOthers;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Others.UserOthers
{
	public partial class UserFilmsViewModel : BaseViewModel
	{
		public ObservableCollection<Film> Films { get; private set; } = new();

		private FilmService service;
		public UserFilmsViewModel(FilmService service)
		{
			this.service = service;
			Title = "Фильмы";
		}

		[RelayCommand]
		private async Task LoadElementsAsync()
		{
			List<Film> films = await service.GetAllAsync();

			if (films.Count == 0)
				return;


			Films.Clear();
			foreach (Film film in films)
			{
				Films.Add(film);
			}
		}

		[RelayCommand]
		private async Task ChooseFilm(Film film)
		{
			try
			{
				IsBusy = true;
				await GoToChooseSession(film);
			}
			catch (Exception ex) { await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо"); }
			finally { IsBusy = false; }
		}

		private async Task GoToChooseSession(Film film)
		{
			await Shell.Current.GoToAsync(nameof(UserSessionsByFilm), true, new Dictionary<string, object>()
			{
				{"Film", film}
			});
		}
	}
}
