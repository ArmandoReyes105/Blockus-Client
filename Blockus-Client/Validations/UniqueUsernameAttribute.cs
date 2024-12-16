using Blockus_Client.BlockusService;
using log4net;
using System;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel;

namespace Blockus_Client.Validations
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(UniqueUsernameAttribute));

        public UniqueUsernameAttribute() { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            string username = value as string;

            var result = IsUniqueUsername(username);

            if (username != null && result == 0)
            {
                var usernameRegisteredMessage = Properties.Resources.Error_UsernameAlreadyRegistered;
                return new ValidationResult(usernameRegisteredMessage, new[] { validationContext.MemberName });
            }

            if (username != null && result == -1)
            {
                var databaseErrorMessage = Properties.Resources.Error_retrievingData;
                return new ValidationResult(databaseErrorMessage, new[] { validationContext.MemberName });
            }

            if (username != null && result == -2)
            {
                var serverErrorMessage = Properties.Resources.Error_serverConnection;
                return new ValidationResult(serverErrorMessage, new[] { validationContext.MemberName }); 
            }


            return ValidationResult.Success;
        }

        private int IsUniqueUsername(string username)
        {
            int result;
            
            try
            {
                using (var client = new AccountServiceClient())
                {
                    result = client.IsUsernameUnique(username);
                }
            }
            catch (CommunicationException ex)
            {
                result = -2;
                log.Error("Validate unique username: " + ex.Message);
            }
            catch (Exception ex)
            {
                result = -2;
                log.Error("Validate unique username: " + ex.Message);
            }

            return result; 
        }
    }
}
