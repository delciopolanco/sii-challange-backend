using System.ComponentModel.DataAnnotations;
using POS_service_customers.DTOs;
using POS_service_customers.Models;

namespace POS_service_customers.Utils.CustomValidations
{
    public class IsNameRequired : ValidationAttribute
    {

        public string GetErrorMessage() => $"LastName is required";

        protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
        {
            var customer = (CustomerDTO)validationContext.ObjectInstance;

            if ((value is null || value is "") && customer.CustomerType == CustomerType.Provider)
            {
                return ValidationResult.Success;
            }

            if ((value is null || value is "") && customer.CustomerType == CustomerType.Customer)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}

