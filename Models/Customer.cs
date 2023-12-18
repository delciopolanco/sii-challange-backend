using System.ComponentModel.DataAnnotations.Schema;

namespace POS_service_customers.Models
{
    [Table("customer")]
	public class Customer : Base
	{
        public string Name { get; set; }
        public string? LastName { get; set; }
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Contact>? Contacts { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public CustomerType CustomerType { get; set; } = CustomerType.Customer;

    }
}

