using System.ComponentModel.DataAnnotations;
using POS_service_customers.Models;

namespace POS_service_contact.DTOs
{
	public class ContactDTO
    {

        [Required]
        public string Detail { get; set; }
        [Required]
        public ContactType ContactType { get; set; }
        public bool? isPrincipal { get; set; }
    }

    public class ContactIdDTO: ContactDTO
    {
        [Required]
        public string Id { get; set; }

        public bool Active { get; set; } = true;
    }
}

