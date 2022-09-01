using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models
{
    public class HotelVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
