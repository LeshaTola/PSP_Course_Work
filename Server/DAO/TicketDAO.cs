﻿using Microsoft.EntityFrameworkCore;
using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server.DAO
{
	internal class TicketDAO : DAO<Ticket>
	{
		public override void Upsert(Ticket item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				/*var ticket = Get(item.Id);
				if (ticket != null)
				{
					db.Tickets.Update(item);
				}
				else
				{
					db.Tickets.Add(item);
				}*/
				db.Tickets.Update(item);

				db.SaveChanges();
			}
		}

		public override Ticket Get(int id)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Tickets.FirstOrDefault(t => t.Id == id);
			}
		}

		public override List<Ticket> GetAll()
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Tickets.Include(t => t.User).Include(t => t.Session).ThenInclude(s => s.Hall).ThenInclude(h => h.Cinema).Include(t => t.Session).ThenInclude(s => s.Film).ToList();
			}
		}

		public override void Remove(Ticket item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Remove(item);
				db.SaveChanges();
			}
		}
	}
}
