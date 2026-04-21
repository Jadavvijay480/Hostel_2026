using System;

namespace Hostel_2026.Models
{
    public class AdminModel
    {
        public int Admin_id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime JoiningDate { get; set; }

        public int ExperienceYears { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}