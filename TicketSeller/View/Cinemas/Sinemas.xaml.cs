using TicketSellerLib.DTO;

namespace TicketSeller.View;

public partial class Cinemas : ContentPage
{
	public Cinemas()
	{
		InitializeComponent();

		CinemasList.ItemsSource = new List<Cinema>
		{
			new Cinema
			{
				Id = 1,
				Name = "Name1",
				Address = "Address1"
			},
			new Cinema
			{
				Id = 2,
				Name = "Name2",
				Address = "Address2"
			}
		};
	}
}