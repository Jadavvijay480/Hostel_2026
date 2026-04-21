using System;
using System.ComponentModel.DataAnnotations;

namespace Hostel_2026.Models
{
    public class Warden
    {
        [Key]
        public int Warden_id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        [Required]
        public int ExperienceYears { get; set; }

        [Required]
        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
