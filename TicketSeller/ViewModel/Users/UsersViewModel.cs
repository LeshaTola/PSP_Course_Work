using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Users
{
	public partial class UsersViewModel : BaseViewModel
	{
		public ObservableCollection<User> Users { get; private set; } = new();

		private UserService service;

		public UsersViewModel(UserService service)
		{
			Title = "Пользователи";
			this.service = service;
			_ = LoadUsersAsync();
		}

		[RelayCommand]
		public async Task LoadUsersAsync()
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

		private async Task GoToAddUsersPageAsync(User user)
		{
			await Shell.Current.GoToAsync($"{nameof(AddUser)}", true, new Dictionary<string, object>
			{
				{"User", user}
			});
		}

		[RelayCommand]
		private async Task UpsertUserAsync(User user)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				user ??= new();
				await GoToAddUsersPageAsync(user);
				await LoadUsersAsync();
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
		private async Task DeleteUserAsync(int id)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				var response = await service.DeleteAsync(id);
				await LoadUsersAsync();
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
