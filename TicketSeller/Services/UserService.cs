using System.Diagnostics;
using System.Text.Json;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;
using TicketSellerLib.TCP;

namespace TicketSeller.Services
{
	public class UserService
	{
		public async Task<Response> LoginAsync(User user)
		{
			try
			{
				var data = JsonSerializer.Serialize(user);
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
	}
}
