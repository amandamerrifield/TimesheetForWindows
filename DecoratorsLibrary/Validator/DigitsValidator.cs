using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorsLibrary.Validator
{
    internal class DigitsValidator : TextValidator
    {
        DigitStyle _digitStyl;
        decimal _numbr;

        internal enum DigitStyle {DigitsOnly, DigitsNotZero, Money, MoneyNotZero, Percentage, PercentageNotZero}

        internal DigitsValidator(DigitStyle ds)
        {
            _digitStyl = ds;
        }

        internal override string Validate(string text)
        {
            string retVal = String.Empty;

            if (_digitStyl == DigitStyle.DigitsOnly)
            {
                if (IsOnlyDigits(text)) return String.Empty;
                return "Please enter only digits";
            }
            if(_digitStyl == DigitStyle.DigitsNotZero)
            {
                if (IsOnlyDigits(text) && Convert.ToInt32(text) > 0) return String.Empty;
                return "Please enter only digits whose value is greater than zero";
            }
            if(_digitStyl == DigitStyle.Money || _digitStyl == DigitStyle.MoneyNotZero)
            {
                retVal = "Please enter a valid";

                if (Decimal.TryParse(text, out _numbr))
                {
                    if (_digitStyl == DigitStyle.Money)
                    {
                        if (text.Length > 3 && text.Substring(text.Length - 3, 1) == ".") return String.Empty;
                    }
                    else
                    {
                       if (text.Length > 3 && text.Substring(text.Length - 3, 1) == "." && text != "0.00") return String.Empty;
                        retVal = "Please enter a non-zero";
                    }
                }
                return retVal + " dollar amount including decimal point and two trailing digits";
            }
            if(_digitStyl == DigitStyle.Percentage || _digitStyl == DigitStyle.PercentageNotZero)
            {
                retVal = "Please enter a valid";

                if (Decimal.TryParse(text, out _numbr))
                {
                    if (_digitStyl == DigitStyle.Percentage)
                    {
                        if (text.Length > 4 && text.Substring(text.Length - 4, 1) == ".") return String.Empty;
                    }
                    else
                    {
                        if (text.Length > 4 && text.Substring(text.Length - 4, 1) == "." && text != "0.000") return String.Empty;
                        retVal = "Please enter a non-zero";
                    }
                }
                return retVal + " percentage including decimal point and three trailing digits";
            }
            return retVal;
        }

        private bool IsOnlyDigits(string txt)
        {
            foreach (char khar in txt.ToCharArray())
            {
                if (!Char.IsDigit(khar))
                {
                    return false;
                }
            }
            return true;
        }



    }
}
