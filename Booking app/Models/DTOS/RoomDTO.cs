namespace BookingApp.Models.DTOS
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int RoomNr { get; set; }
        public double Price { get; set; }
        public RoomTypeDTO RoomType { get; set; }
        public int RoomTypeId { get; set; }
        public HotelDTO Hotel { get; set; }
        public int HotelId { get; set; }

    }
}
