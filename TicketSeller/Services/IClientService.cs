using TicketSellerLib.TCP;

namespace TicketSeller.Services
{
	internal interface IClientService<T>
	{

		public Task<List<T>> GetAllAsync();

		public Task<Response> UpsertAsync(T item);

		public Task<Response> DeleteAsync(int id);
	}
}
