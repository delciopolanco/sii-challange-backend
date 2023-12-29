using System.ComponentModel.DataAnnotations;
using addCard_backend.Utils.CustomValidations;

namespace addCard_backend.DTOs
{
	public class CreditCardDTO
	{
        [Required(ErrorMessage = "required")]
        [MaxLength(20, ErrorMessage = "charactersMax")]
        [RegularExpression("^[A-Za-záéíóúÁÉÍÓÚñÑüÜ\\s]+$", ErrorMessage = "CreditcardHolderName only allows strings values without special characters")]
        public string CreditcardHolderName { get; set; }

        [Required(ErrorMessage = "required")]
        [MinLength(16, ErrorMessage = "CreditcardNumber should have 16 digits at least")]
        public string CreditcardNumber { get; set; }

        [Required(ErrorMessage = "required")]
        [MinLength(3, ErrorMessage = "CreditcardCVV should have 3 digits at least")]
        public string CreditcardCVV { get; set; }

        [Required(ErrorMessage = "required")]
        [RegularExpression("^(0[1-9]|1[0-2])(22|[2-9][0-9]|(1[0-9][0-9])|([2-9][0-9][0-9]))$", ErrorMessage = "CreditcardExpireDate is not a valid date")]
        [ExpirationDate]
        public string CreditcardExpireDate { get; set; }
    }

    public class CreditCardIdDTO: CreditCardDTO
    {
        [Required(ErrorMessage = "required")]
        public int Id { get; set; }
    }
}

