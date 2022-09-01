using AutoMapper;
using Booking_app.Services.Interfaces;
using BookingApp.Models;
using BookingApp.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_app.Controllers
{
    public class RoomController : Controller
    {

        private readonly IRoomService _service;
        private readonly IRezervationService _rezervationService;
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;

        public RoomController(IRoomService service, IMapper mapper, 
            IRezervationService rezervationService, IHotelService hotelService)
        {
            _service = service;
            _mapper = mapper;
            _rezervationService = rezervationService;
            _hotelService = hotelService;
        }
        // GET: RoomController
        public ActionResult Index()
        {
            _rezervationService.SetRoomStatus();
            var room = _service.FindAll();
            var model = _mapper.Map<List<RoomDTO>, List<RoomVM>>((List<RoomDTO>)room);
            return View(model);
        }

        // GET: RoomController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoomController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var room = _mapper.Map<RoomDTO>(model);

                var isSucces = _service.Create(room);
                if (!isSucces)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: RoomController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
