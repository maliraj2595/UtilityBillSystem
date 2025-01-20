using System.ComponentModel.DataAnnotations;

namespace UtilityBill
{ 
    public class ApplicationDetail
    {
        [Key]
        public int ApplicationDetailId { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public String? ApplicationStatus { get; set; }

        [Required]
        public string? ConnectionType { get; set; }

        [Required]
        public string? RequiredLoad { get; set; }


        //Navigation Property
        public MeterDetail? MeterDetail { get; set; }
    }
}
