using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hostel_2026.Models
{
    public class FeesModel
    {
        public int Fee_id { get; set; }

        [Required]
        public int Student_id { get; set; }

        [Required]
        [Range(1, 999999)]
        public decimal Amount { get; set; }

        [Required]
        public string Fee_month { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Due_date { get; set; }

        public string Status { get; set; } = "Pending";


    }
}