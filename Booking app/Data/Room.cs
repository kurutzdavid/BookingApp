using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_app.Data
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public int RoomNr { get; set; }
        public double Price { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; }
        public int RoomTypeId { get; set; }

        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }

    }
}
