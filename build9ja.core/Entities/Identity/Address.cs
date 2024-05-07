using System.ComponentModel.DataAnnotations;

namespace build9ja.core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}