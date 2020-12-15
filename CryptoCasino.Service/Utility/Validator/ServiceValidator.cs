using System;
using System.Text.RegularExpressions;
using WebCasino.Service.Exceptions;

namespace WebCasino.Service.Utility.Validator
{
	public static class ServiceValidator
	{
		public static void ObjectIsNotEqualNull(object entity)
		{
			if (entity == null)
			{
				throw new EntityNotFoundException("Entity not found");
			}
		}

		public static void ObjectIsEqualNull(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("Can't pass null reference object");
			}
		}

		public static void ValueNotEqualZero(int value)
		{
			if (value <= 0)
			{
				throw new ArgumentNullException("", "Value cannot be 0 or less then 0");
			}
		}

		public static void IsInputStringEmptyOrNull(string input)
		{
			if (input == null || input == string.Empty)
			{
				throw new ArgumentNullException("", "String cannot be null or empty");
			}
		}

		public static void ValueIsBetween(int value, int min, int max)
		{
			if (value < min || value > max)
			{
				throw new ArgumentOutOfRangeException("", "Value not in the given range");
			}
		}

		public static void ValueIsBetween(double value, double min, double max)
		{
			if (value < min || value > max)
			{
				throw new ArgumentOutOfRangeException("", "Value not in the given range");
			}
		}

		public static void CheckStringLength(string str, int minLength, int maxLength)
		{
			if (str.Length < minLength || str.Length > maxLength)
			{
				throw new ArgumentOutOfRangeException("", "String length not in the given values");
			}
		}

		public static void CheckCardExpirationDate(DateTime currentDate)
		{
			if (currentDate <= DateTime.Now)
			{
				throw new CardExpirationException("Card is expired!");
			}
		}

		public static void ValidateCardNumber(string cardNumber)
		{
			var rgx = new Regex("^[0-9]{16}$");

			if (!rgx.IsMatch(cardNumber))
			{
				throw new CardNumberException("Invalid card number");
			}
		}

        public static void DayIsInMonth(int day)
        {
            var currentMonthsTotalDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            if (day < 1 || day > currentMonthsTotalDays)
            {
                throw new NotValidDayInMonthException("Invalid day in month.");
            }

        }
    }
}