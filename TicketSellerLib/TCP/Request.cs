using TicketSellerLib.Enum;

namespace TicketSellerLib.TCP
{
	public class Request
	{
		public RequestTypes RequestType { get; set; }
		public string RequestMessage { get; set; }

		public Request(RequestTypes requestType, string requestMessage)
		{
			RequestType = requestType;
			RequestMessage = requestMessage;
		}
	}
}
