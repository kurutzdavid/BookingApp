using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models
{
    public class RoomTypeVM
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
