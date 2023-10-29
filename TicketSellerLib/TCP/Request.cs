﻿using TicketSellerLib.Enum;

namespace TicketSellerLib.TCP
{
	public class Request
	{
		public RequestTypes Type { get; set; }
		public string Message { get; set; }

		public Request() { }

		public Request(RequestTypes type, string message)
		{
			Type = type;
			Message = message;
		}
	}
}
