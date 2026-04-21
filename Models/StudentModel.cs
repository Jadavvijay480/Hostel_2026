using System;
using System.ComponentModel.DataAnnotations;

namespace Hostel_2026.Models
{
    public class StudentModel
    {
        public int Student_id { get; set; }

        [Required]
        public string First_name { get; set; }

        [Required]
        public string Last_name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        // Not database column
        public string FullName => First_name + " " + Last_name;
    }

    //public class StudentDropDownModel
    //{
    //    public int Student_id { get; set; }
    //    public string StudentName { get; set; }
    //}
}