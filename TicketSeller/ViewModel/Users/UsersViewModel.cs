using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Users
{
	public partial class UsersViewModel : BaseViewModel, ICrudViewModel<User>
	{
		public ObservableCollection<User> ValidUsers { get; private set; } = new();

		[ObservableProperty] private string searchString;
		[ObservableProperty] private List<string> orderTypesList = new() { "От А до Я", "От Я до А" };
		[ObservableProperty] private int orderTypeId = (int)UserOrderType.NameFromAToZ;

		private List<User> users;
		private UserService service;

		public UsersViewModel(UserService service)
		{
			Title = "Пользователи";
			this.service = service;
		}

		[RelayCommand]
		public async Task LoadElementsAsync()
		{
			try
			{
				List<User> loadedUsers = await service.GetAllAsync();
				if (loadedUsers.Count == 0)
					return;
				users = loadedUsers;
				ValidateElements();
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

		[RelayCommand]
		private void ValidateElements()
		{
			List<User> searchedElements = SearchElements(users);
			var searchedAndOrderedElements = OrderElements(searchedElements);

			ValidUsers.Clear();
			foreach (var element in searchedAndOrderedElements)
			{
				ValidUsers.Add(element);
			}
		}

		private List<User> SearchElements(List<User> unsearchedElements)
		{
			if (string.IsNullOrEmpty(SearchString))
			{
				return unsearchedElements;
			}

			List<User> searchedElements = new();
			searchedElements = unsearchedElements.Where(c => c.Login.Contains(SearchString)).ToList();
			return searchedElements;
		}

		private List<User> OrderElements(List<User> unorderedElements)
		{
			List<User> orderedElements = new();
			var orderType = (UserOrderType)OrderTypeId;
			switch (orderType)
			{
				case UserOrderType.NameFromAToZ:
					orderedElements = unorderedElements.OrderBy((u) => u.Login).ToList();
					break;
				case UserOrderType.NameFromZToA:
					orderedElements = unorderedElements.OrderByDescending((f) => f.Login).ToList();
					break;
			}
			return orderedElements;
		}
	}
}