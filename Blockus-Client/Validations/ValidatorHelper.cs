using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Blockus_Client.Validations
{
    public static class ValidatorHelper
    {

        public static List<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);

            return results; 
        }

    }
}
