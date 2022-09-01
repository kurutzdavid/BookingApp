using Booking_app.Data;
using Booking_app.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Booking_app.Repositories
{
    public class RezervationRepository : IRezervationRepository
    {
        private readonly ApplicationDbContext _db;

        public RezervationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Rezervation entity)
        {
            _db.Rezervations.Add(entity);
            return Save();
        }

        public bool Delete(Rezervation entity)
        {
            _db.Rezervations.Remove(entity);
            return Save();
        }

        public ICollection<Rezervation> FindAll()
        {
            var rezervation = _db.Rezervations.Include("Room").Include(q => q.User).ToList();
            return rezervation;
        }

        public Rezervation FindById(int id)
        {
            var rezervation = _db.Rezervations.Find(id);
            return rezervation;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool Update(Rezervation entity)
        {
            _db.Rezervations.Update(entity);
            return Save();
        }
    }
}
