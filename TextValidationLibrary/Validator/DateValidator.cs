using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextValidationLibrary.Validator
{
    internal class DateValidator : TextValidator
    {
        internal enum DateStyle {Past, Future, DontCare}

        DateStyle _dateStyl;

        internal DateValidator(DateStyle dateStyl)
        {
            _dateStyl = dateStyl;
        }

        internal override string Validate(string text)
        {
            string retVal = String.Empty;

            if (text.Length != 10 || text.Substring(2, 1) != "/" || text.Substring(5, 1) != "/")
            {
                return "Please enter a date in mm/dd/yyyy format";
            }

            DateTime theDate;
            bool okay = DateTime.TryParseExact(text, "MM/dd/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out theDate);
            if(!okay)
            {
                return "Please enter a valid date";
            }

            if(_dateStyl == DateStyle.Past)
            {
                if (theDate > DateTime.Today) return "Please enter a date that is not in the future.";                
            }

            if(_dateStyl == DateStyle.Future)
            {
                if (theDate <= DateTime.Today) return "Please enter a date that is in the future.";
            }
            return retVal;
        }
    }
}
