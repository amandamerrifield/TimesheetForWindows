using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextValidationLibrary.Validator
{
	internal class ZipCodeValidator : TextValidator
	{
		internal ZipCodeValidator(){}

		internal override string Validate(string text)
		{
			string errMsg = "Please enter zip code as 5 digits or 5 digits, -, 4 digits";

			if (!(text.Length == 5 || text.Length == 10))
			{
				return errMsg;
			}

			if (text.Length == 5)
			{
				char[] array = text.ToCharArray();
				bool ok = true;
				foreach (char x in array)
				{
					if (! char.IsDigit(x))
					{
						ok = false;
						break;
					}
				}
				if (ok)
				{
					return String.Empty;
				}
				return errMsg;
			}

			if (text.Length == 10)
			{
				bool ok = true;

				string[] zipparts = text.Split('-');
				if (zipparts.Length != 2)
				{
					return errMsg;
				}

				char[] array = zipparts[0].ToCharArray();
				foreach (char x in array)
				{
					if (!char.IsDigit(x))
					{
						ok = false;
						break;
					}
				}
				if (! ok)
				{
					return errMsg;
				}

				array = zipparts[1].ToCharArray();
				foreach (char x in array)
				{
					if (!char.IsDigit(x))
					{
						ok = false;
						break;
					}
				}
				if (!ok)
				{
					return errMsg;
				}
			}

			return String.Empty;
		}
	}
}
