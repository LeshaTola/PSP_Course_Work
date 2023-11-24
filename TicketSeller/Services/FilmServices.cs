using Newtonsoft.Json;
using System.Diagnostics;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;
using TicketSellerLib.TCP;

namespace TicketSeller.Services
{
	public class FilmServices
	{
		public async Task<List<Film>> GetActualFilmsAsync()
		{
			try
			{
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.GetAllFilms, ""));
				var response = await Client.Client.Instance.GetResponseAsync();
				var films = JsonConvert.DeserializeObject<List<Film>>(response.Data);
				return films;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<Response> UpsertFilmAsync(Film film)
		{
			try
			{
				string data = JsonConvert.SerializeObject(film);
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.UpsertFilm, data));
				var response = await Client.Client.Instance.GetResponseAsync();
				return response;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<Response> DeleteFilmAsync(int id)
		{
			try
			{
				await Client.Client.Instance.SendRequestAsync(new Request(RequestTypes.DeleteFilm, id.ToString()));
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
