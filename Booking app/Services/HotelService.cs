using AutoMapper;
using Booking_app.Data;
using Booking_app.Repositories.Interfaces;
using Booking_app.Services.Interfaces;
using BookingApp.Models.DTOS;

namespace Booking_app.Services
{
    public class HotelService : IHotelService

    {
        private readonly IHotelRepository _repo;
        private readonly IMapper _mapper;


        public HotelService(IHotelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public bool Create(HotelDTO entity)
        {
            var model = _repo.Create(_mapper.Map<HotelDTO, Hotel>(entity));
            return model;
        }

        public bool Delete(HotelDTO entity)
        {
            _repo.Delete(_mapper.Map<HotelDTO, Hotel>(entity));
            return Save();
        }

        public ICollection<HotelDTO> FindAll()
        {
            var hotel = _repo.FindAll().ToList();
            var model = _mapper.Map<List<HotelDTO>>(hotel);
            return model;
        }

        public HotelDTO FindById(int id)
        {
            var hotel = _repo.FindById(id);

            return _mapper.Map<Hotel, HotelDTO>(hotel);
        }

        public bool isExis(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var save = _repo.Save();
            return save != false;
        }

        public bool Update(HotelDTO entity)
        {
            _repo.Update(_mapper.Map<HotelDTO, Hotel>(entity));
            return Save();
        }
    }
}