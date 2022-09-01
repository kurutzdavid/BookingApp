using Booking_app.Data;
using Booking_app.Repositories.Interfaces;

namespace Booking_app.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _db;

        public HotelRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Hotel entity)
        {
            _db.Hotels.Add(entity);
            return Save();
        }

        public bool Delete(Hotel entity)
        {
            _db.Hotels.Remove(entity);
            return Save();
        }

        public ICollection<Hotel> FindAll()
        {
            var Hotel = _db.Hotels.ToList();
            return Hotel;
        }

        public Hotel FindById(int id)
        {
            var Hotel = _db.Hotels.Find(id);
            return Hotel;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool Update(Hotel entity)
        {
            _db.Hotels.Update(entity);
            return Save();
        }
    }
}
