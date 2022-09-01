using Booking_app.Data;
using BookingApp.Models.DTOS;

namespace Booking_app.Services.Interfaces
{
    public interface IRezervationService : IServiceBase<RezervationDTO>
    {
        public ICollection<RezervationDTO> FindByUser(string id);
        public bool UserCreate(RezervationDTO entity, string id);
        public bool CreateUserRezervation(RezervationDTO entity, int roomId,string userId);
        public ICollection<RoomDTO> ListRooms();
        public void SetRoomStatus();
        public static int ReturnedRoomID = 0;
    }
}
