﻿using Microsoft.EntityFrameworkCore;
using TicketSeller.Model;
using TicketSellerLib.DTO;

namespace Server.DAO
{
	public class SessionDAO : DAO<Session>
	{
		public override void Upsert(Session item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				/*var session = Get(item.Id);
				if (session != null)
				{
					db.Sessions.Update(item);
				}
				else
				{
					db.Sessions.Add(item);
				}*/

				db.Sessions.Update(item);
				db.SaveChanges();
			}
		}

		public override Session Get(int id)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Sessions.FirstOrDefault(s => s.Id == id);
			}
		}

		public override List<Session> GetAll()
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				return db.Sessions.Include(s => s.Hall).ThenInclude(h => h.Cinema).Include(s => s.Film).ToList();
			}
		}

		public override void Remove(Session item)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				db.Sessions.Remove(item);
				db.SaveChanges();
			}
		}
	}
}
