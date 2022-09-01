namespace BookingApp.Models.DTOS
{
    public class RezervationDTO
    {
        public int Id { get; set; }
        public DateTime RezervationDate { get; set; }
        public DateTime DayIn { get; set; }
        public DateTime DayOut { get; set; }
        public int DayRange { get; set; }
        public UserDTO User { get; set; }
        public string UserId { get; set; }
        public double TotalPrice { get; set; }
        public RoomDTO Room { get; set; }
        public int RoomID { get; set; }
    }
}
