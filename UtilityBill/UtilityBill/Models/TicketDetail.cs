using System.ComponentModel.DataAnnotations;

namespace UtilityBill
{
    public class TicketDetail
    {
        [Key]
        public int TicketDetailId { get; set; }

        [EnumDataType(typeof(TicketTypes))]
        public TicketTypes Type { get; set; }

        public string? Description { get; set; }

        public TicketStatus? status { get; set; }

        //navigation property
        public User? User { get; set; }
    }
}
