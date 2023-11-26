using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Sessions
{
	public partial class SessionsViewModel : BaseViewModel, ICrudViewModel<Session>
	{
		public ObservableCollection<Session> Sessions { get; private set; } = new();

		private SessionService service;

		public SessionsViewModel(SessionService service)
		{
			Title = "Сеансы";
			this.service = service;
			_ = LoadElementsAsync();
		}


		[RelayCommand]
		public async Task DeleteElementAsync(int id)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				var response = await service.DeleteAsync(id);
				await LoadElementsAsync();
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
		public async Task GoToAddElementPageAsync(Session element)
		{
			await Shell.Current.GoToAsync($"{nameof(AddSession)}", true, new Dictionary<string, object>
			{
				{"Session", element}
			});
		}

		[RelayCommand]
		public async Task LoadElementsAsync()
		{
			List<Session> sessions = await service.GetAllAsync();

			if (sessions.Count == 0)
				return;


			Sessions.Clear();
			foreach (Session session in sessions)
			{
				Sessions.Add(session);
			}
		}

		[RelayCommand]
		public async Task UpsertElementAsync(Session element)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				element ??= new();
				await GoToAddElementPageAsync(element);
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
