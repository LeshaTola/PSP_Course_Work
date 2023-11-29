﻿namespace TicketSellerLib.DTO
{
	public class User
	{
		public int Id { get; set; }
		public string Login { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public bool IsAdmin { get; set; }
		public string Email { get; set; } = string.Empty;

	}
}
