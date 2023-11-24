using Newtonsoft.Json;
using Server.Services;
using System.Net.Sockets;
using TicketSellerLib.DTO;
using TicketSellerLib.Enum;
using TicketSellerLib.TCP;

namespace Server
{
	public class ClientHandler
	{
		private Server server;
		private TcpClient client;

		private StreamReader reader;
		private StreamWriter writer;

		private UserService userService = new();
		private FilmService filmService = new();
		private HallService hallService = new();
		private SessionService sessionService = new();
		private TicketService ticketService = new();
		private CinemaService cinemaService = new();

		public ClientHandler(Server server, TcpClient client)
		{
			this.server = server;
			this.client = client;

			var stream = client.GetStream();
			reader = new StreamReader(stream);
			writer = new StreamWriter(stream);
		}

		public async Task ProcessAsync()
		{
			Console.WriteLine($"Клиент: {client.Client.RemoteEndPoint} подключился");
			try
			{
				while (true)
				{
					var request = await GetRequestAsync();
					switch (request.Type)
					{
						case RequestTypes.SignUp:
							SignUp(request.Message);
							break;
						case RequestTypes.Login:
							Login(request.Message);
							break;
						case RequestTypes.GetAllFilms:
							GetAllFilms();
							break;
						case RequestTypes.UpsertFilm:
							UpsertFilm(request.Message);
							break;
						case RequestTypes.DeleteFilm:
							DeleteFilm(request.Message);
							break;
						default:
							Console.WriteLine("Unknown request type");
							break;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				await Console.Out.WriteLineAsync($"Client {client.Client.RemoteEndPoint} is Disconnected");
				server.RemoveClient(this);
				Close();
			}
		}

		private async Task<Request> GetRequestAsync()
		{
			var message = await reader.ReadLineAsync();
			Console.WriteLine($"Клиент {client.Client.RemoteEndPoint} отправил: " + message);
			var request = JsonConvert.DeserializeObject<Request>(message);
			return request;
		}

		private async void SendResponseAsync(Response response)
		{
			string message = JsonConvert.SerializeObject(response);
			await writer.WriteLineAsync(message);
			writer.Flush();
		}

		private void Login(string requestMessage)
		{
			var requestUser = JsonConvert.DeserializeObject<User>(requestMessage);

			var users = userService.GetAll();
			var user = users.Find(u => u.Login.Equals(requestUser.Login, StringComparison.OrdinalIgnoreCase));
			Response response;
			if (user != null)
			{
				if (user.Password.Equals(requestUser.Password))
				{
					response = new Response(ResponseTypes.Ok, "Авторизация подтверждена", JsonConvert.SerializeObject(user));
				}
				else
				{
					response = new Response(ResponseTypes.NotOk, "Неверный пароль");
				}
			}
			else
			{
				response = new Response(ResponseTypes.NotOk, "Такого пользователя не существует");
			}
			SendResponseAsync(response);
		}

		private void SignUp(string requestMessage)
		{
			var requestUser = JsonConvert.DeserializeObject<User>(requestMessage);

			var users = userService.GetAll();
			var user = users.Find(u => u.Login.Equals(requestUser.Login, StringComparison.OrdinalIgnoreCase));
			Response response;
			if (user == null)
			{
				userService.Add(requestUser);
				response = new Response(ResponseTypes.Ok, "Успешная регистрация");
			}
			else
			{
				response = new Response(ResponseTypes.NotOk, "Такой пользователь уже существует");
			}
			SendResponseAsync(response);
		}

		private void GetAllFilms()
		{
			var films = filmService.GetAll();
			string data = JsonConvert.SerializeObject(films);
			Response response = new Response(ResponseTypes.Ok, "", data);
			SendResponseAsync(response);
		}

		private void UpsertFilm(string requestMessage)
		{
			var requestFilm = JsonConvert.DeserializeObject<Film>(requestMessage);

			filmService.Add(requestFilm);
			Response response = new Response(ResponseTypes.Ok, "Фильм успешно добавлен");
			SendResponseAsync(response);
		}

		private void DeleteFilm(string requestMessage)
		{
			var film = filmService.Get(int.Parse(requestMessage));

			filmService.Remove(film);
			Response response = new Response(ResponseTypes.Ok, "Фильм успешно добавлен");
			SendResponseAsync(response);
		}

		private void Close()
		{
			writer.Close();
			reader.Close();
			client.Close();
		}
	}
}
