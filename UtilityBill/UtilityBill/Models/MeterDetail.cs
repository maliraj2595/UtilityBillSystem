using System.ComponentModel.DataAnnotations;
namespace UtilityBill
{
    public class MeterDetail
    {
        [Key]
        public int MeterId { get; set; }
        public DateTime InstallationDate { get; set; }
        public string? Status { get; set; }

        // Navigation Property
        public List<BillDetail>? BillDetail { get; set; } = new List<BillDetail>();
    }
}
