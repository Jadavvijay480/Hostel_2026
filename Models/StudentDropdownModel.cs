namespace Hostel_2026.Models
{
    public class StudentDropdownModel
    {
        public int Student_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Optional: convenience property for display in dropdown
        public string DisplayName => $"{First_name} {Last_name} | {Phone} | {Email}";
    }
}