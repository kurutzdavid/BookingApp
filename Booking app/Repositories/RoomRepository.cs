using Booking_app.Data;
using Booking_app.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Booking_app.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _db;

        public RoomRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Room entity)
        {
            _db.Rooms.Add(entity);
            return Save();
        }

        public bool Delete(Room entity)
        {
            _db.Rooms.Remove(entity);
            return Save();
        }

        public ICollection<Room> FindAll()
        {
            var Room = _db.Rooms.Include("Hotel").Include("RoomType").ToList();
            return Room;
        }

        public Room FindById(int id)
        {
            var Room = _db.Rooms.Find(id);
            return Room;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool Update(Room entity)
        {
            _db.Rooms.Update(entity);
            return Save();
        }
    }
}
