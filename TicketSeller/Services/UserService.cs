using Newtonsoft.Json;
using System.Diagnostics;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;
using TicketSellerLib.TCP;

namespace TicketSeller.Services
{
	public enum UserOrderType
	{
		NameFromAToZ,
		NameFromZToA,
	}

	public class UserService : IClientService<User>
	{
		public async Task<Response> AuthorizeAsync(User user)
		{
			try
			{
				var data = JsonConvert.SerializeObject(user);
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.Login, data));
				var response = await Client.Client.Instance.GetResponseAsync();
				return response;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<Response> UpsertAsync(User user)
		{
			try
			{
				var data = JsonConvert.SerializeObject(user);
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.UpsertUser, data));
				var response = await Client.Client.Instance.GetResponseAsync();
				return response;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<Response> DeleteAsync(int id)
		{
			try
			{
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.DeleteUser, id.ToString()));
				var response = await Client.Client.Instance.GetResponseAsync();
				return response;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<List<User>> GetAllAsync()
		{
			try
			{
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GetUsers, ""));
				var response = await Client.Client.Instance.GetResponseAsync();
				var users = JsonConvert.DeserializeObject<List<User>>(response.Data);
				return users;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<bool> CheckUserPropertiesAsync(User user)
		{
			if (!await CheckLoginAsync(user))
				return false;

			if (!await CheckPasswordAsync(user))
				return false;

			if (!await CheckEmailAsync(user))
				return false;

			return true;
		}

		private async Task<bool> CheckLoginAsync(User user)
		{
			int minLoginLength = 4;
			if (user.Login.Length < minLoginLength)
			{
				await Shell.Current.DisplayAlert("Ошибка!", $"Логин должен состоять из как минимум {minLoginLength} символов", "Хорошо");
				return false;
			}

			int maxLoginLength = 20;
			if (user.Login.Length > maxLoginLength)
			{
				await Shell.Current.DisplayAlert("Ошибка!", $"Логин слишком длинный! Он не должен быть больше {maxLoginLength} символов", "Хорошо");
				return false;
			}
			return true;
		}

		private async Task<bool> CheckPasswordAsync(User user)
		{
			int minPasswordLength = 4;
			if (user.Password.Length < minPasswordLength)
			{
				await Shell.Current.DisplayAlert("Ошибка!", $"Пароль должен состоять из как минимум {minPasswordLength} символов", "Хорошо");
				return false;
			}

			int maxPasswordLength = 20;
			if (user.Password.Length > maxPasswordLength)
			{
				await Shell.Current.DisplayAlert("Ошибка!", $"Пароль слишком длинный! Он не должен быть больше {maxPasswordLength} символов", "Хорошо");
				return false;
			}
			return true;
		}

		private async Task<bool> CheckEmailAsync(User user)
		{
			if (!user.Email.Contains('@'))
			{
				await Shell.Current.DisplayAlert("Ошибка!", $"Почта не подходить. Пример example@gmail.com", "Хорошо");
				return false;
			}
			return true;
		}
	}
}
