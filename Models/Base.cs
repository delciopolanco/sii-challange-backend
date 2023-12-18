using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_service_customers.Models
{
	public abstract class Base
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string CreationUser { get; set; }
        public string? LastModificationUser { get; set; }
        public bool Active { get; set; } = true;

        public Base()
        {
            if (Id == null)
            {
                CreationDate = DateTime.UtcNow;
                CreationUser = "DefaultUser";
            }
            else
            {
                LastModificationDate = DateTime.UtcNow;
                LastModificationUser = "DefaultUser";
            }
        }
    }
}

