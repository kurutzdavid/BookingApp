using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models
{
    public class RezervationVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Rezervation Date")]
        public DateTime RezervationDate { get; set; }
        [Required]
        [Display(Name = "Day In")]
        public DateTime DayIn { get; set; }
        [Required]
        [Display(Name = "Day Out")]
        public DateTime DayOut { get; set; }
        [Required]
        [Display(Name = "Day Range")]
        public int DayRange { get; set; }
        public UserVM User { get; set; }
        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }
        public RoomVM? Room { get; set; }
        [Required]
        [Display(Name = "Room")]
        public int RoomID { get; set; }
        public double TotalPrice { get; set; }
    }

    public class UserRezervationVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Rezervation Date")]
        public DateTime RezervationDate { get; set; }
        [Required]
        [Display(Name = "Day In")]
        public DateTime DayIn { get; set; }
        [Required]
        [Display(Name = "Day Out")]
        public DateTime DayOut { get; set; }
        [Required]
        [Display(Name = "Day Range")]
        public int DayRange { get; set; }
        [Required]
        [Display(Name = "Room")]
        public int RoomID { get; set; }
        public double TotalPrice { get; set; }
    }

    public class RoomsRezervationVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Rezervation Date")]
        public DateTime RezervationDate { get; set; }
        [Required]
        [Display(Name = "Day In")]
        public DateTime DayIn { get; set; }
        [Required]
        [Display(Name = "Day Out")]
        public DateTime DayOut { get; set; }
        [Required]
        [Display(Name = "Day Range")]
        public int DayRange { get; set; }
        public double TotalPrice { get; set; }
    }

}
