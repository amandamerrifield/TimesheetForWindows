using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DecoratorsLibrary.Validator;
using static DecoratorsLibrary.Validator.DateValidator;
using static DecoratorsLibrary.Validator.DigitsValidator;

namespace DecoratorsLibrary
{
    public class ControlTextValidator
    {
        public enum ValidationStyle
        {
            PhoneNumber, PhoneNumWithExtension,
            DateFuture, DatePast, DateAny,
            DigitsOnly, DigitsNotZero, Money, MoneyNotZero, Percentage, PercentageNotZero,
            EmailAddr,
            NoWhiteSpace, NoPunctuation, NoWhiteSpaceAndNoPunct,
            NoOriginalValue, NoValidation,  StateAbbreviation, Time, ZipPlus4, SSN
        }

        // TextValidator needs these internal variables
        protected Control _attachedTextControl;
        protected string _nomenclature;
        protected bool _required;
        protected ValidationStyle _validationStyle;
        protected string _originalText;

        //Constructor
        public ControlTextValidator(Control attachedTextCtrl, string nomenclature, bool required, ValidationStyle validationStyle)
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

            switch (_validationStyle)
            {
                case ValidationStyle.DateAny:
                    validator = new DateValidator(DateStyle.DontCare);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
                    break;
                case ValidationStyle.DateFuture:
                    validator = new DateValidator(DateStyle.Future);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
                    break;
                case ValidationStyle.DatePast:
                    validator = new DateValidator(DateStyle.Past);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
                    break;
                case ValidationStyle.DigitsOnly:
                    validator = new DigitsValidator(DigitStyle.DigitsOnly);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
                    break;
                case ValidationStyle.DigitsNotZero:
                    validator = new DigitsValidator(DigitStyle.DigitsNotZero);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
                    break;
                case ValidationStyle.Money:
                    validator = new DigitsValidator(DigitStyle.Money);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
                    break;
                case ValidationStyle.MoneyNotZero:
                    validator = new DigitsValidator(DigitStyle.MoneyNotZero);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
                    break;
                case ValidationStyle.Percentage:
                    validator = new DigitsValidator(DigitStyle.Percentage);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
                    break;
                case ValidationStyle.PercentageNotZero:
                    validator = new DigitsValidator(DigitStyle.PercentageNotZero);
                    returnMsg = validator.Validate(_attachedTextControl.Text);
                    break;
            }

            return returnMsg;
        }


    }
}
