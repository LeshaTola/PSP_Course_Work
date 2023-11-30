using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Others.UserOthers
{
	[QueryProperty("Film", "Film")]
	public partial class UserSessionsByFilmViewModel : BaseViewModel
	{
		[ObservableProperty] private Film film;
		public ObservableCollection<Session> Sessions { get; private set; } = new();

		private SessionService service;

		public UserSessionsByFilmViewModel(SessionService service)
		{
			this.service = service;
			Title = "Сеансы на фильм";
		}

		[RelayCommand]
		private async Task LoadElementsAsync()
		{
			List<Session> sessions = await service.GetAllAsync();

			if (sessions.Count == 0)
				return;

			sessions = sessions.Where(session => session.FilmId == Film.Id).ToList();

			Sessions.Clear();
			foreach (Session session in sessions)
			{
				Sessions.Add(session);
			}
		}

		[RelayCommand]
		private async Task ChooseSession(Session session)
		{
			try
			{
				IsBusy = true;
				await GoToChooseSeats(session);
			}
			catch (Exception ex) { await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо"); }
			finally { IsBusy = false; }
		}

		private async Task GoToChooseSeats(Session session)
		{
			await Shell.Current.GoToAsync(nameof(Seats), true, new Dictionary<string, object>
			{
				{"Session", session }
			});
		}
	}
}
