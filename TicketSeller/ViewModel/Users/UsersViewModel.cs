using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Users
{
	public partial class UsersViewModel : BaseViewModel, ICrudViewModel<User>
	{
		public ObservableCollection<User> Users { get; private set; } = new();

		private UserService service;

		public UsersViewModel(UserService service)
		{
			Title = "Пользователи";
			this.service = service;
			_ = LoadElementsAsync();
		}

		[RelayCommand]
		public async Task LoadElementsAsync()
		{
			try
			{
				List<User> users = await service.GetAllAsync();
				if (users.Count == 0)
					return;

				Users.Clear();
				foreach (User user in users)
				{
					Users.Add(user);
				}
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
			}
		}

		public async Task GoToAddElementPageAsync(User user)
		{
			await Shell.Current.GoToAsync($"{nameof(AddUser)}", true, new Dictionary<string, object>
			{
				{"User", user}
			});
		}

		[RelayCommand]
		public async Task UpsertElementAsync(User user)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				user ??= new();
				await GoToAddElementPageAsync(user);
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
	}
}
