using Newtonsoft.Json;
using System.Diagnostics;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;
using TicketSellerLib.TCP;

namespace TicketSeller.Services
{
	public class SessionService : IClientService<Session>
	{
		public async Task<List<Session>> GetAllAsync()
		{
			try
			{
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GetSessions, ""));
				var response = await Client.Client.Instance.GetResponseAsync();
				var sessions = JsonConvert.DeserializeObject<List<Session>>(response.Data);
				return sessions;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<Response> UpsertAsync(Session session)
		{
			try
			{
				string data = JsonConvert.SerializeObject(session);
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.UpsertSession, data));
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
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.DeleteSession, id.ToString()));
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
