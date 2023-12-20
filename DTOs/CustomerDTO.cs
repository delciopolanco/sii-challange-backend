using System.ComponentModel.DataAnnotations;
using POS_service_address.DTOs;
using POS_service_contact.DTOs;
using POS_service_customers.Models;
using POS_service_customers.Utils.CustomValidations;

namespace POS_service_customers.DTOs
{
	public class CustomerDTO
	{
        [Required]
        public string Name { get; set; }
        [IsNameRequired]
        public string? LastName { get; set; } = string.Empty;
        public List<AddressDTO>? Addresses { get; set; }
        public List<ContactDTO>? Contacts { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public CustomerType CustomerType { get; set; } = CustomerType.Customer;
    }

    public class CustomerIdDTO: CustomerDTO
    {
        [Required]
        public int Id { get; set; }

        public bool Active { get; set; } = true;
    }
}

