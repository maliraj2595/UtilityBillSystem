using System.ComponentModel.DataAnnotations;

namespace UtilityBill
{
    public class BillDetail
    {
        [Key]
        public int BillDetailId { get; set; }

        public int UnitsConsumed { get; set; }

        public decimal TotalBill { get; set; }

        public decimal Rate { get; set; }

        public DateTime BillDate { get; set; }

        public BillStatus BillStatus { get; set; }

        // Foreign key to associate a bill with a user
        public int UserId { get; set; }

        // Navigation property
        public User User { get; set; }

        //Navigation property
        public MeterDetail? MeterDetail { get; set; }
    }
}
