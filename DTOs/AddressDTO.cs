using System.ComponentModel.DataAnnotations;


namespace POS_service_address.DTOs
{
	public class AddressDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
    }

    public class AddressIdDTO : AddressDTO
    {
        [Required]
        public string Id { get; set; }

        public bool Active { get; set; } = true;
    }
}

