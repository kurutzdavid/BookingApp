using AutoMapper;
using Booking_app.Data;
using Booking_app.Repositories.Interfaces;
using Booking_app.Services.Interfaces;
using BookingApp.Models.DTOS;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace Booking_app.Services
{
    public class RezervationService : IRezervationService
    {
        private readonly IRezervationRepository _repo;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public RezervationService(IRezervationRepository repo, 
            IRoomRepository roomRepository, 
            IMapper mapper,
            UserManager<User> userManager)
        {
            _repo = repo;
            _mapper = mapper; 
            _roomRepository = roomRepository;
            _userManager = userManager; 
        }
        public bool Create(RezervationDTO entity)
        {
            entity.RezervationDate = DateTime.Now;
            var dayRange = entity.DayOut.Subtract(entity.DayIn);
            if (!DayOutVerification(entity.DayIn, entity.DayOut))
                return false;
            if (!FreeRooms(entity))
                return false;
            entity.DayRange = dayRange.Days;

            var room = _roomRepository.FindById(entity.RoomID);
            var totalPrice = room.Price * dayRange.Days;

            entity.TotalPrice = totalPrice;
            var model = _repo.Create(_mapper.Map<RezervationDTO, Rezervation>(entity));
            room.Status = true;
            _roomRepository.Update(room);
            return model;
        }

        public bool UserCreate(RezervationDTO entity,string id)
        {
            entity.RezervationDate = DateTime.Now;
            var dayRange = entity.DayOut.Subtract(entity.DayIn);
            if (!DayOutVerification(entity.DayIn, entity.DayOut))
                return false;
            if (!FreeRooms(entity))
                return false;
            entity.DayRange = dayRange.Days;

            var room = _roomRepository.FindById(entity.RoomID);
            var totalPrice = room.Price * dayRange.Days;
            entity.TotalPrice = totalPrice;
            entity.UserId = id;
            var model = _repo.Create(_mapper.Map<RezervationDTO, Rezervation>(entity));
            room.Status = true;
            _roomRepository.Update(room);
            return model;
        }

        public bool CreateUserRezervation(RezervationDTO entity, int roomId,string userId)
        {
            entity.RezervationDate = DateTime.Now;
            var room = _roomRepository.FindById(roomId);
            var dayRange = entity.DayOut.Subtract(entity.DayIn);
            if(!DayOutVerification(entity.DayIn,entity.DayOut))
                return false;
            entity.DayRange = dayRange.Days;
            var totalPrice = room.Price * dayRange.Days;
            entity.RoomID = roomId;
            entity.TotalPrice = totalPrice;
            entity.UserId = userId;
            entity.Id = 0;
            var model = _repo.Create(_mapper.Map<RezervationDTO, Rezervation>(entity));
            room.Status = true;
            _roomRepository.Update(room);
            return model;
        }

        public bool DayOutVerification(DateTime dayIn,DateTime dayOut)
        {
            var today = DateTime.Today;
            if (dayIn.CompareTo(today) <= 0)
            {
                return false;
            }
            if (dayOut.CompareTo(dayIn) <= 0)
            {
                return false;
            }
            return true;
        } 

        public void SetRoomStatus()
        {
            var date = DateTime.Now;
            var rooms = _roomRepository.FindAll().ToList();
            var rezervations = _repo.FindAll().ToList();

            foreach (var rez in rezervations)
            {
                foreach(var room in rooms)
                {
                    if(rez.DayOut.CompareTo(date) < 0)
                    {
                        room.Status = false;
                        _roomRepository.Update(room);
                        Delete(_mapper.Map<Rezervation,RezervationDTO>(rez));
                    }
                }
            }

        }

        public bool FreeRooms(RezervationDTO entity)
        {
            var room = _roomRepository.FindById(entity.RoomID);
            if(room.Status == true)
                return false;
            return true;
        }

        public bool EditRooms(RezervationDTO entity)
        {
            var rez = true;
            var room = _roomRepository.FindById(entity.RoomID);
            if ((room.Status == true) && (entity.RoomID == IRezervationService.ReturnedRoomID))
                rez = true;
            if (room.Status == false)
                rez = true;
            if ((room.Status == true) && (entity.RoomID != IRezervationService.ReturnedRoomID))
                rez = false;
            return rez;
        }

        public bool RoomCheck(RezervationDTO entity)
        {
            if (entity.RoomID == IRezervationService.ReturnedRoomID)
                return true;
            return false;
        }

        public bool Delete(RezervationDTO entity)
        {
            var room = _roomRepository.FindById(entity.RoomID);
            var roomType = _repo.FindById(entity.Id);
            if (roomType == null)
            {
                return false;
            }
            var model = _repo.Delete(roomType);
            room.Status = false;
            _roomRepository.Update(room);
            return model;
        }

        public ICollection<RezervationDTO> FindAll()
        {
            var rezervation = _repo.FindAll().ToList();
            var model = _mapper.Map<List<RezervationDTO>>(rezervation);
            return model;
        }

        public ICollection<RezervationDTO> FindByUser(string id)
        {
            var rezervation = _repo.FindAll().Where(q => q.UserId.Equals(id));
            var model = _mapper.Map<List<RezervationDTO>>(rezervation);
            return model;
        }

        public ICollection<RoomDTO> ListRooms()
        {
            var rooms = _roomRepository.FindAll().Where(q => q.Status == false);
            var model = _mapper.Map<List<RoomDTO>>(rooms);
            return model;
        }

        public RezervationDTO FindById(int id)
        {
            var rezervation = _repo.FindById(id);

            return _mapper.Map<Rezervation, RezervationDTO>(rezervation);
        
        }

        public bool isExis(int id)
        {

            var exist = _repo.FindAll().Any(q => q.Id == id);
            return exist;
        }

        public bool Update(RezervationDTO entity)
        {
            var room = _roomRepository.FindById(entity.RoomID);
            var roomOld = _roomRepository.FindById(IRezervationService.ReturnedRoomID);
            var dayRange = entity.DayOut.Subtract(entity.DayIn);
            entity.DayRange = dayRange.Days;
            if (!DayOutVerification(entity.DayIn, entity.DayOut))
                return false;
            if (!EditRooms(entity))
                return false;
            if (!RoomCheck(entity))
            {
                roomOld.Status = false;
                room.Status = true;
                _roomRepository.Update(roomOld);
                _roomRepository.Update(room);
            }
            var totalPrice = room.Price * dayRange.Days;
            entity.TotalPrice = totalPrice;
            var model = _repo.Update(_mapper.Map<RezervationDTO,Rezervation>(entity));
            return model;
        }
    }
}
