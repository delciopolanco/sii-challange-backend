using System.ComponentModel.DataAnnotations.Schema;

namespace POS_service_customers.Models
{
    [Table("contact")]
    public class Contact: Base
	{
		public string Detail { get; set; }
        public ContactType ContactType { get; set; }
        public bool? isPrincipal { get; set; }
    }
}

