using Booking_app.Data;
using Booking_app.Repositories.Interfaces;

namespace Booking_app.Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private  readonly ApplicationDbContext _db;

        public RoomTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(RoomType entity)
        {
            _db.RoomTypes.Add(entity);
            return Save();
        }

        public bool Delete(RoomType entity)
        {
            _db.RoomTypes.Remove(entity);
            return Save();
        }

        public ICollection<RoomType> FindAll()
        {
            var roomTypes = _db.RoomTypes.ToList();
            return roomTypes;
        }

        public RoomType FindById(int id)
        {
            var roomtypes = _db.RoomTypes.Find(id);
            return roomtypes;
        }

        public bool Save()
        {

            return _db.SaveChanges() > 0;
        }

        public bool Update(RoomType entity)
        {
            var roomType = _db.RoomTypes.Update(entity);
            return Save();
        }

        
    }
}
