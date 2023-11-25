using TicketSellerLib.DTO;

namespace TicketSeller.View;

public partial class Users : ContentPage
{
	public Users()
	{
		InitializeComponent();

		lists.ItemsSource = new List<User>
		{
			new User()
			{
				Id = 1,
				Login = "Lesha",
				Password = "password",
				Email = "email",
			},
			new User()
			{
				Id = 2,
				Login = "Stepan",
				Password = "password2",
				Email = "email2",
			},
			new User()
			{
				Id = 3,
				Login = "Lisa",
				Password = "password3",
				Email = "email3",
			}
		};
	}
}