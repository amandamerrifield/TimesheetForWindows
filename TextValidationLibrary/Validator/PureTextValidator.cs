using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextValidationLibrary.Validator
{
	internal class PureTextValidator : TextValidator
	{
		private PureTextStyle _style;

		internal enum PureTextStyle
		{
			EmailAddress, StateAbbreviation, NoWhiteSpace, NoPunctuation, NoWhiteSpaceAndNoPunct
		}
		internal PureTextValidator(PureTextStyle style)
		{
			_style = style;
		}

		internal override string Validate(string text)
		{
			string retVal = String.Empty;

			if (_style == PureTextStyle.EmailAddress)
			{
				List<Char> emailDelimiters = new List<Char>();
				emailDelimiters.Add('@');
				emailDelimiters.Add('.');

				if (! text.Contains (" ") )
				{
					char[] arry = text.ToCharArray();
					int atLoc = 0;
					int dotLoc = 0;

					for (int counter = 0; counter < text.Length; counter++)
					{
						if (arry[counter] == '@') atLoc = counter;
						if (arry[counter] == '.') dotLoc = counter;
					}
					// No dot, No @
					if (atLoc == 0 || dotLoc == 0)
					{
						return "Please enter a valid e-mail address";
					}
					// We have a dot and an @
					if (dotLoc >= atLoc + 2 && dotLoc < text.Length -1) return string.Empty;

				}

				return "Please enter a valid e-mail address";
			}

			if(_style == PureTextStyle.StateAbbreviation)
			{
				if (text.Length == 2)
				{
					string[] stateAbbreviations =
						{"AL", "AK", "AZ", "AR", "CA","CO", "CT", "DE","FL","GA",
						"HI","ID","IL","IN","IA","KS", "KY","LA","ME","MD","MA",
						"MI", "MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY",
						"NC","ND","OH","OK","OR","PA","RI","SC","SD","TN","TX",
						"UT","VT","VA","WA","WV","WI","WY","AS","DC","FM","GU",
						"MH","MP","PW","PR","VI"};

					if (stateAbbreviations.Contains(text))
					{
						return String.Empty;
					}
				}
				return "Please enter a two character state abbreviation in upper case";
			}

			if(_style == PureTextStyle.NoWhiteSpace)
			{
				if (text.Contains(" ") || text.Contains("\t"))
				{
					return "Please enter contiguous text ";
				}

				return String.Empty;
			}

			if (_style == PureTextStyle.NoPunctuation)
			{
				char[] array = text.ToCharArray();
				bool haspunct = false;
				foreach (char x in array)
				{
					if (char.IsPunctuation(x))
					{
						haspunct = true;
						break;
					}
				}
				if (haspunct)
				{
					return "Please enter pure text with no punctuation";
				}
				return String.Empty;
			}

			if(_style == PureTextStyle.NoWhiteSpaceAndNoPunct)
			{
				if (text.Contains(" ") || text.Contains("\t"))
				{
					return "Please enter contiguous text ";
				}

				char[] array = text.ToCharArray();
				bool haspunct = false;
				foreach (char x in array)
				{
					if (char.IsPunctuation(x))
					{
						haspunct = true;
						break;
					}
				}
				if (haspunct)
				{
					return "Please enter pure text with no punctuation";
				}

				return String.Empty;
			}


			return retVal;
		}
	}
}
