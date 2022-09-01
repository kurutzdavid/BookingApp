using AutoMapper;
using Booking_app.Services.Interfaces;
using BookingApp.Models;
using BookingApp.Models.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeService _service;
        private readonly IRezervationService _rezervationService;
        private readonly IMapper _mapper;

        public RoomTypeController(IRoomTypeService service, IMapper mapper, IRezervationService rezervationService)
        {
            _service = service;
            _mapper = mapper;
            _rezervationService = rezervationService;
        }
        // GET: RoomTypeController
        public ActionResult Index()
        {
            _rezervationService.SetRoomStatus();
            var roomType = _service.FindAll();
            var model = _mapper.Map<List<RoomTypeDTO>, List<RoomTypeVM>>((List<RoomTypeDTO>)roomType);
            return View(model);
        }

        // GET: RoomTypeController/Details/5
        public ActionResult Details(int id)
        {
            if(!_service.isExis(id))
            {
                return NotFound();
            }
            var roomtype = _service.FindById(id);
            var model = _mapper.Map<RoomTypeVM>(roomtype);
            return View(model);
        }

        // GET: RoomTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var roomType = _mapper.Map<RoomTypeDTO>(model);

                var isSucces = _service.Create(roomType);
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

        // GET: RoomTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_service.isExis(id)){
                return NotFound();
            }
            var roomType = _service.FindById(id);
            var model = _mapper.Map<RoomTypeVM>(roomType);
            return View(model);
        }

        // POST: RoomTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,RoomTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var roomType = _mapper.Map<RoomTypeDTO>(model);
                var isSucces = _service.Update(roomType);
                if (!isSucces)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                ModelState.AddModelError("", "The hotel does not exist!");
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: RoomTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!_service.isExis(id))
            {
                return NotFound();
            }
            var roomType = _service.FindById(id);
            var model = _mapper.Map<RoomTypeVM>(roomType);
            return View(model);
        }

        // POST: RoomTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RoomTypeVM model)
        {
            try
            {     
                if(model == null)
                {
                    return NotFound();
                }
                var isSucces = _service.Delete(_mapper.Map<RoomTypeDTO>(model));
                if (!isSucces)
                {
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
    }
}
