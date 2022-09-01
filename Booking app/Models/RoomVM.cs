using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models
{
    public class RoomVM
    {
        public int Id { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public int RoomNr { get; set; }
        [Required]
        public double Price { get; set; }
        public RoomTypeVM? RoomType { get; set; }
        [Required]
        public int RoomTypeId { get; set; }
        public HotelVM? Hotel { get; set; }
        [Required]
        [Display(Name = "Hotel")]
        public int HotelId { get; set; }

    }

    public class RoomRezervationVM
    {
        public int Id { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public int RoomNr { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public RoomTypeVM RoomType { get; set; }
        [Required]
        [Display(Name = "Hotel")]
        public int HotelId { get; set; }
    }
}
