using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace addCard_backend.Models
{
    [Table("CreditCard")]
    public class CreditCard : Base
	{
        [Required()]
        [MaxLength(40)]
        public string CreditcardHolderName { get; set; }

        [Required()]
        [MaxLength(100)]
        public string CreditcardNumber { get; set; }

        [Required()]
        [MaxLength(6)]
        public string CreditcardCVV { get; set; }

        [Required()]
        [MaxLength(8)]
        public string CreditcardExpireDate { get; set; }
    }
}

