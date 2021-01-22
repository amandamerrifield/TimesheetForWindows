using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextValidationLibrary
{
    internal abstract class TextValidator
    {
        //This is the super type for all the text validator sub-types
        internal TextValidator() { }

        internal virtual string Validate(string text)
        {
            return String.Empty;
        }

    }
}
