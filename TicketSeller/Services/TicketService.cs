using Newtonsoft.Json;
using System.Diagnostics;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;
using TicketSellerLib.TCP;

namespace TicketSeller.Services
{
	public class TicketService : IClientService<Ticket>
	{
		public async Task<List<Ticket>> GetAllAsync()
		{
			try
			{
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GetTickets, ""));
				var response = await Client.Client.Instance.GetResponseAsync();
				var tickets = JsonConvert.DeserializeObject<List<Ticket>>(response.Data);
				return tickets;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<Response> UpsertAsync(Ticket ticket)
		{
			try
			{
				string data = JsonConvert.SerializeObject(ticket);
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.UpsertTicket, data));
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
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.DeleteTicket, id.ToString()));
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
