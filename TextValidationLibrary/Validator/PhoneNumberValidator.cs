using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextValidationLibrary.Validator
{
	internal class PhoneNumberValidator : TextValidator
	{
		internal PhoneNumberValidator() { }

		internal override string Validate(string text)
		{
			string retVal = "Please enter a valid phone number in the format (xxx) xxx-xxxx";
			int testint = 0;
			List<Char> phoneNbr = new List<Char>();

			if (text.Length == 14 && text.StartsWith ("(") && text.Substring (4,2) == ") " && text.Substring (9,1) == "-")
			{
				bool ok = int.TryParse(text.Substring(1, 3), out testint);
				if (ok)
				{
					ok = int.TryParse(text.Substring(6, 3), out testint);
					if (ok)
					{
						ok = int.TryParse(text.Substring(10, 4), out testint);
						if (ok)
						{
							return String.Empty;
						}
					}	
				}
			}
			return retVal;
		}
	}
}
