using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextValidationLibrary.Validator
{
	internal class SSNValidator : TextValidator
	{
		internal SSNValidator()	{	}
		internal override string Validate(string text)
		{
			string errMsg = "Please enter social security number in the format xxx-xx-xxxx";
			
			if (text.Length == 11)
			{
				char[] array = text.ToCharArray();
				bool ok = true;
				for(int ptr = 0; ptr < array.Length; ptr++)
				{
					if(ptr == 3 || ptr == 6)
					{
						if(array[ptr] != '-')
						{
							ok = false;
							break;
						}
					}
					else
					{
						if (! char.IsDigit(array[ptr]))
						{
							ok = false;
							break;
						}
					}
				}
				if (!ok) return errMsg;
				return String.Empty;
			}

			return errMsg;
		}
	}
}
