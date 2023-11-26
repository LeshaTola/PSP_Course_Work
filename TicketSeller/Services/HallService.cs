using Newtonsoft.Json;
using System.Diagnostics;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;
using TicketSellerLib.TCP;

namespace TicketSeller.Services
{
	public class HallService : IClientService<Hall>
	{
		public async Task<List<Hall>> GetAllAsync()
		{
			try
			{
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GetHalls, ""));
				var response = await Client.Client.Instance.GetResponseAsync();
				var halls = JsonConvert.DeserializeObject<List<Hall>>(response.Data);
				return halls;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<Response> UpsertAsync(Hall hall)
		{
			try
			{
				string data = JsonConvert.SerializeObject(hall);
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.UpsertHall, data));
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
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.DeleteHall, id.ToString()));
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
