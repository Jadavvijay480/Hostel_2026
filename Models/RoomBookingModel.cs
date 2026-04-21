using System;
using System.ComponentModel.DataAnnotations;

namespace Hostel_Consume_2026.Models
{
    public class RoomBookingModel
    {
        [Key]
        public int Booking_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Guest_Name { get; set; }

        [Required]
        [StringLength(15)]
        public string Mobile_No { get; set; }

        [Required]
        [StringLength(20)]
        public string Room_Number { get; set; }

        [Required]
        [StringLength(20)]
        public string Address { get; set; }

        [Required]
        public DateTime Booking_Date { get; set; }

        public string Booking_Status { get; set; } = "Booked";

        public DateTime CreatedAt { get; set; } 
    }
}