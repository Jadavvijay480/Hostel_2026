using System.ComponentModel.DataAnnotations;

namespace Hostel_2026.Models
{
    public class RoomModel
    {
        public int Room_id { get; set; }

        [Required]
        public int Student_id { get; set; }

        [Required]
        public int Hostel_id { get; set; }

        [Required]
        [StringLength(10)]
        public string Room_number { get; set; }

        [Required]
        [StringLength(50)]
        public string Room_type { get; set; }   // Single, Double, Dorm

        [Required]
        public int Capacity { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Available";
    }
}