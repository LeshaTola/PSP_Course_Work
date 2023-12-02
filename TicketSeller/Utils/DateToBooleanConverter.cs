using System.Globalization;

namespace TicketSeller.Utils
{
	internal class DateToBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is DateOnly date)
			{
				return date >= DateOnly.FromDateTime(DateTime.Now);
			}

			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
