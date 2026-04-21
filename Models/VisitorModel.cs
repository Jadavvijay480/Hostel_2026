using System;
using System.ComponentModel.DataAnnotations;

namespace Hostel_Consume_2026.Models
{
    public class VisitorModel
    {
        public int Visitor_id { get; set; }

        [Required(ErrorMessage = "Visitor name is required")]
        [StringLength(100)]
        public string VisitorName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(15)]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Visit date is required")]
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }

        [Required(ErrorMessage = "In Time is required")]
        [DataType(DataType.Time)]
        public TimeSpan InTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? OutTime { get; set; }

        [Required(ErrorMessage = "Purpose is required")]
        public string Purpose { get; set; }

        [Required(ErrorMessage = "Student is required")]
        public int Student_id { get; set; }

        public int? Warden_id { get; set; }

        public bool Status { get; set; } = true;

        public DateTime CreatedAt { get; set; }
    }
}