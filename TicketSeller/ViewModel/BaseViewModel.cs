using CommunityToolkit.Mvvm.ComponentModel;

namespace TicketSeller.ViewModel
{
	public partial class BaseViewModel : ObservableObject
	{
		[ObservableProperty] private bool isBusy;
		[ObservableProperty] string title;
	}
}
