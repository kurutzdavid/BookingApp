using AutoMapper;
using Booking_app.Services.Interfaces;
using BookingApp.Models;
using BookingApp.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelService _service;
        private readonly IMapper _mapper;
        private readonly IRezervationService _rezervationService;

        public HotelController(IHotelService service, 
            IMapper mapper,
            IRezervationService rezervationService)
        {
            _service = service;
            _mapper = mapper;
            _rezervationService = rezervationService;
        }
        // GET: HotelController
        public ActionResult Index()
        {
            _rezervationService.SetRoomStatus();
            var hotel = _service.FindAll();
            var model = _mapper.Map<List<HotelDTO>, List<HotelVM>>((List<HotelDTO>)hotel);
            return View(model);
        }

        // GET: HotelController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HotelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HotelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HotelVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var hotel = _mapper.Map<HotelDTO>(model);

                var isSucces = _service.Create(hotel);
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

        // GET: HotelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HotelController/Edit/5
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

        // GET: HotelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HotelController/Delete/5
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
