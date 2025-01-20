using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UtilityBill.Models;

namespace UtilityBill
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(8)]
        public string? Password { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        public string? MobileNumber { get; set; }

        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }

        public bool IsDeleted { get; set; }

        //navigation properties
        public List<TicketDetail>? TicketDetail { get; set; } = new List<TicketDetail>();

        public Address? Address { get; set; }

        public ApplicationDetail? ApplicationDetail { get; set; }

        public List<BillDetail>? BillDetails { get; set; } = new List<BillDetail>();
    }

}
