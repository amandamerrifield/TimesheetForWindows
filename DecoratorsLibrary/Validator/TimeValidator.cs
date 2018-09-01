using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorsLibrary.Validator
{
	internal class TimeValidator : TextValidator
	{
		internal TimeValidator() { }

		internal override string Validate(string text)
		{
			string errMsg = "Please enter a time in 12-hour HH:MM format";

			//Five chars are required for evaluationg
			if (text.Length == 4) text = "0" + text;
			if (text.Length != 5) return errMsg;

			//We have 5 chars so begin!
			char[] array = text.ToCharArray();
			bool failure = true;

			for (int ptr = 0; ptr < array.Length; ptr++)
			{
				if (ptr == 2)
				{
					if (array[ptr] != ':')
					{
						failure = true;
						break;
					}
				}
				else
				{
					if (!char.IsDigit(array[ptr]))
					{
						failure = true;
						break;
					}
				}
			}
			if (failure) return errMsg;
			return String.Empty;

		}
	}
}
