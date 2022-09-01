using AutoMapper;
using Booking_app.Data;

using Booking_app.Repositories.Interfaces;
using Booking_app.Services.Interfaces;
using BookingApp.Models.DTOS;

namespace Booking_app.Services
{
    public class RoomService : IRoomService

    {
        private readonly IRoomRepository _repo;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository repo, 
            IMapper mapper,
            IHotelRepository hotelRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _hotelRepository = hotelRepository;
        } 

        public bool Create(RoomDTO entity)
        {
            var room = _repo.Create(_mapper.Map<Room>(entity));
            return room;
        }

        public bool Delete(RoomDTO entity)
        {
            var room = _repo.FindById(entity.Id);
            if (room == null)
            {
                return false;
            }
            var model = _repo.Delete(room);
            return model;
        }

        public ICollection<RoomDTO> FindAll()
        {
            var room = _repo.FindAll().ToList();
            var model = _mapper.Map<List<RoomDTO>>(room);
            return model;
        }

        public RoomDTO FindById(int id)
        {
            var room = _repo.FindById(id);

            return _mapper.Map<Room,RoomDTO>(room);
        }

        public bool isExis(int id)
        {
            var exist = _repo.FindAll().Any(q => q.Id == id);
            return exist;
        }

        public bool Update(RoomDTO entity)
        {
            var model = _repo.Update(_mapper.Map<Room>(entity));
            return model;
        }
    }
}
