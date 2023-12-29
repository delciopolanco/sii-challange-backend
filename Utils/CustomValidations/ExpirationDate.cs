using System.ComponentModel.DataAnnotations;
using addCard_backend.DTOs;
using addCard_backend.Models;

namespace addCard_backend.Utils.CustomValidations
{
    public class ExpirationDate : ValidationAttribute
    {


        protected override ValidationResult? IsValid(
         object? value, ValidationContext validationContext)
        {
            try
            {
                var creditcard = (CreditCardDTO)validationContext.ObjectInstance;

                if (DateTime.TryParseExact(creditcard.CreditcardExpireDate, "MMyy", null, System.Globalization.DateTimeStyles.None, out var expirationDate))
                {
                    var currentYear = DateTime.Now.Year % 100;
                    var currentMonth = DateTime.Now.Month;

                    var inputMonth = expirationDate.Month;
                    var inputYear = expirationDate.Year % 100;

                    if (!((inputYear >= currentYear && inputMonth >= currentMonth) || (inputYear > currentYear && inputYear <= currentYear + 5)))
                    {
                        return new ValidationResult("specify a valid ExpirationDate", new[] { nameof(creditcard.CreditcardExpireDate) });
                    }

                }
                else
                {
                    return new ValidationResult("specify a valid ExpirationDate", new[] { nameof(creditcard.CreditcardExpireDate) });
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult("There was an issue with the validation");
            }

            return ValidationResult.Success;
        }
    }
}

