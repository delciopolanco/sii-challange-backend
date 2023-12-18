using System.ComponentModel.DataAnnotations.Schema;

namespace POS_service_customers.Models
{
    [Table("address")]
    public class Address : Base
	{
		public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }
}

