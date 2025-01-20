using System.ComponentModel.DataAnnotations;

namespace UtilityBill
{
    public class Address
    {
        public int AddressId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Street { get; set; }

        [Required]
        [MaxLength(50)]
        public string? City { get; set; }

        [Required]
        public int? PinCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string? State { get; set; }

    }
}
