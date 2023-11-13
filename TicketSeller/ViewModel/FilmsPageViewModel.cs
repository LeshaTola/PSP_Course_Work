﻿using System.Collections.ObjectModel;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel
{
	public partial class FilmsPageViewModel : BaseViewModel
	{
		public ObservableCollection<Film> Films { get; } = new()
		{
			new Film
			{
				Name = "SpiderMan",
				Description = "Питер Паркер – обыкновенный школьник. Однажды он отправился с классом на экскурсию, где его кусает странный паук-мутант. Через время парень почувствовал в себе нечеловеческую силу и ловкость в движении, а главное – умение лазать по стенам и метать стальную паутину. Свои способности он направляет на защиту слабых. Так Питер становится настоящим супергероем по имени Человек-паук, который помогает людям и борется с преступностью. Но там, где есть супергерой, рано или поздно всегда объявляется и суперзлодей",
				Duration = 1.30f,
				Cost = 300,
			},
			new Film(),
		};
	}
}
