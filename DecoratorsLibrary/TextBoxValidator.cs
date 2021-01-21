using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DecoratorsLibrary.Validator;
using static DecoratorsLibrary.Validator.DateValidator;
using static DecoratorsLibrary.Validator.DigitsValidator;
using static DecoratorsLibrary.Validator.PureTextValidator;

namespace DecoratorsLibrary
{
    public class TextBoxValidator
    {
        public enum ValidationStyle
        {
            PhoneNumber,
            DateFuture, DatePast, DateAny,
            DigitsOnly, DigitsNotZero, Money, MoneyNotZero, Percentage, PercentageNotZero,
            EmailAddr, NoWhiteSpace, NoPunctuation, NoWhiteSpaceAndNoPunct, StateAbbreviation,
			NoOriginalValue, NoValidation,  Time, ZipPlus4, SSN
        }

        // TextValidator needs these internal variables
        protected Control _attachedTextControl;
        protected string _nomenclature;
        protected bool _required;
        protected ValidationStyle _validationStyle;
        protected string _originalText;

        //Constructor
        public TextBoxValidator(Control attachedTextCtrl, string nomenclature, bool required, ValidationStyle validationStyle)
        {
            _attachedTextControl = attachedTextCtrl;
            _originalText = _attachedTextControl.Text;
            _nomenclature = nomenclature;
            _required = required;
            _validationStyle = validationStyle;
        }

        public void RestoreOriginalValue()
        {
            _attachedTextControl.Text = _originalText;
        }

        public Control AttachedControl()
        {
            return _attachedTextControl;
        }

        public string ValidationMsg()
        {
            // If we have no text to validate...
            if (String.IsNullOrEmpty(_attachedTextControl.Text.Trim()))
            {
                if (_required) return "Please enter a value into " + _nomenclature;
                return String.Empty;
            }
            // If we got here then there is text to validate
            if (_attachedTextControl.Text == _originalText)
            {
                // Assume the original text is in the proper validation style
                if (_validationStyle == ValidationStyle.NoOriginalValue) return "Please enter a different value into " + _nomenclature;
                return String.Empty;
            }
            // Okay, we have something to validate and its not the original value...
            string returnMsg = String.Empty;
            TextValidator validator;
			string errSuffix = " into " + _nomenclature;

			switch (_validationStyle)
            {
				case ValidationStyle.NoValidation:
					returnMsg = String.Empty;
					break;
                case ValidationStyle.DateAny:
                    validator = new DateValidator(DateStyle.DontCare);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
                    break;
                case ValidationStyle.DateFuture:
                    validator = new DateValidator(DateStyle.Future);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
                case ValidationStyle.DatePast:
                    validator = new DateValidator(DateStyle.Past);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
                case ValidationStyle.DigitsOnly:
                    validator = new DigitsValidator(DigitStyle.DigitsOnly);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
                case ValidationStyle.DigitsNotZero:
                    validator = new DigitsValidator(DigitStyle.DigitsNotZero);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
                case ValidationStyle.Money:
                    validator = new DigitsValidator(DigitStyle.Money);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
                case ValidationStyle.MoneyNotZero:
                    validator = new DigitsValidator(DigitStyle.MoneyNotZero);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
                case ValidationStyle.Percentage:
                    validator = new DigitsValidator(DigitStyle.Percentage);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
                case ValidationStyle.PercentageNotZero:
                    validator = new DigitsValidator(DigitStyle.PercentageNotZero);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
				case ValidationStyle.PhoneNumber:
					validator = new PhoneNumberValidator();
					returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
				case ValidationStyle.EmailAddr:
					validator = new PureTextValidator(PureTextStyle.EmailAddress);
					returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
				case ValidationStyle.StateAbbreviation:
					validator = new PureTextValidator(PureTextStyle.StateAbbreviation);
					returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
				case ValidationStyle.NoWhiteSpace:
					validator = new PureTextValidator(PureTextStyle.NoWhiteSpace);
					returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
				case ValidationStyle.NoPunctuation:
					validator = new PureTextValidator(PureTextStyle.NoPunctuation);
					returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
				case ValidationStyle.NoWhiteSpaceAndNoPunct:
					validator = new PureTextValidator(PureTextStyle.NoWhiteSpaceAndNoPunct);
					returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
				case ValidationStyle.SSN:
					validator = new SSNValidator();
					returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
				case ValidationStyle.Time:
					validator = new TimeValidator();
					returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
				case ValidationStyle.ZipPlus4:
					validator = new ZipCodeValidator();
					returnMsg = validator.Validate(_attachedTextControl.Text);
					if (returnMsg.Length > 0) returnMsg = returnMsg + errSuffix;
					break;
			}

            return returnMsg;
        }


    }
}
