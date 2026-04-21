namespace Hostel_2026.Models
{
    public class RoomDropdownModel
    {
        public int Room_id { get; set; }
        public string Room_number { get; set; }
        public string Room_type { get; set; } // Single, Double, Dorm
        public int Capacity { get; set; }

        // Optional: convenience property for display in dropdown
        public string DisplayName => $"{Room_number} | {Room_type} | Capacity {Capacity}";
    }
}
