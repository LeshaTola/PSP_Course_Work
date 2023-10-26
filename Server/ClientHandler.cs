using System.Net.Sockets;
using System.Text.Json;
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

		public ClientHandler(Server server, TcpClient client)
		{
			this.server = server;
			this.client = client;

			var stream = client.GetStream();
			reader = new StreamReader(stream);
			writer = new StreamWriter(stream);
		}

		public async Task ProcessAsync(TcpClient client)
		{
			Console.WriteLine($"Клиент: {client.Client.RemoteEndPoint} подключился");
			try
			{
				while (true)
				{
					var request = GetRequest(client);
					switch (request.RequestType)
					{
						case RequestTypes.SignUp:

							break;
						case RequestTypes.Login:
							Login(request.RequestMessage);
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
				server.RemoveClient(this);
				Close();
			}
		}

		private Request GetRequest(TcpClient client)
		{
			string message = reader.ReadToEnd();
			Console.WriteLine($"Клиент {client.Client.RemoteEndPoint} отправил: " + message);
			var request = JsonSerializer.Deserialize<Request>(message);
			return request;
		}

		private void Login(string requestMessage)
		{
			var requestUser = JsonSerializer.Deserialize<User>(requestMessage);


		}

		private void Close()
		{
			writer.Close();
			reader.Close();
			client.Close();
		}
	}
}
