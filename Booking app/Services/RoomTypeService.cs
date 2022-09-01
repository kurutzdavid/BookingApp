using AutoMapper;
using Booking_app.Data;
using Booking_app.Repositories.Interfaces;
using Booking_app.Services.Interfaces;
using BookingApp.Models.DTOS;

namespace Booking_app.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _repo;
        private readonly IMapper _mapper;

        public RoomTypeService(IRoomTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public bool Create(RoomTypeDTO entity)
        {
            var model = _repo.Create(_mapper.Map<RoomTypeDTO, RoomType>(entity));
            return model;
        }

        public bool Delete(RoomTypeDTO entity)
        {
            var roomType = _repo.FindById(entity.Id);
            if (roomType == null)
            {
                return false;
            }
            var model = _repo.Delete(roomType);
            return model;
        }

        public ICollection<RoomTypeDTO> FindAll()
        {
            var roomtype = _repo.FindAll().ToList();
            var model = _mapper.Map<List<RoomType>, List<RoomTypeDTO>>(roomtype);
            return model;
        }

        public RoomTypeDTO FindById(int id)
        {
            var roomtype = _repo.FindById(id);
            return _mapper.Map<RoomType, RoomTypeDTO>(roomtype);
        }

        public bool Update(RoomTypeDTO entity)
        {
            var model = _repo.Update(_mapper.Map<RoomTypeDTO, RoomType>(entity));
            return model;
        }

        public bool isExis(int id)
        {
            var exist = _repo.FindAll().Any(q => q.Id == id);
            return exist;
        }
    }
}