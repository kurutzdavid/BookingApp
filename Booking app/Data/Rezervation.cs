using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_app.Data
{
    public class Rezervation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime RezervationDate { get; set; }
        [Required]
        public DateTime DayIn { get; set; }
        [Required]
        public DateTime DayOut { get; set; }
        [Required]
        public int DayRange { get; set; }
        public double TotalPrice { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string UserId { get; set; }
        
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }
    }
}
