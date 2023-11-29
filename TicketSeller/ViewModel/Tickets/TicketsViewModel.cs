using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TicketSeller.Services;
using TicketSeller.View;
using TicketSellerLib.DTO;

namespace TicketSeller.ViewModel.Tickets
{
	public partial class TicketsViewModel : BaseViewModel, ICrudViewModel<Ticket>
	{
		public ObservableCollection<Ticket> Tickets { get; private set; } = new();

		private TicketService service;

		public TicketsViewModel(TicketService service)
		{
			Title = "Билеты";
			this.service = service;
		}


		[RelayCommand]
		public async Task DeleteElementAsync(int id)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				var response = await service.DeleteAsync(id);
				await LoadElementsAsync();
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
			}
			finally
			{
				IsBusy = false;
			}
		}

		[RelayCommand]
		public async Task GoToAddElementPageAsync(Ticket element)
		{
			await Shell.Current.GoToAsync($"{nameof(AddTicket)}", true, new Dictionary<string, object>
			{
				{"Ticket", element}
			});
		}

		[RelayCommand]
		public async Task LoadElementsAsync()
		{
			List<Ticket> tickets = await service.GetAllAsync();

			if (tickets.Count == 0)
				return;


			Tickets.Clear();
			foreach (Ticket ticket in tickets)
			{
				Tickets.Add(ticket);
			}
		}

		[RelayCommand]
		public async Task UpsertElementAsync(Ticket element)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				element ??= new();
				await GoToAddElementPageAsync(element);
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "Хорошо");
			}
			finally
			{
				IsBusy = false;
			}
		}

	}
}
