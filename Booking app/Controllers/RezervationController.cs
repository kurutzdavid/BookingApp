using AutoMapper;
using Booking_app.Data;
using Booking_app.Services.Interfaces;
using BookingApp.Models;
using BookingApp.Models.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
    public class RezervationController : Controller
    {
        private readonly IRezervationService _service;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IRoomService _roomService;

        public RezervationController(IRezervationService service, 
            IMapper mapper,
            UserManager<User> userManager,
            IRoomService roomService)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
            _roomService = roomService;
        }
        // GET: RezervationController
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            _service.SetRoomStatus();
            var rezervation = _service.FindAll();
            var model = _mapper.Map<List<RezervationDTO>, List<RezervationVM>>((List<RezervationDTO>)rezervation);
            return View(model);
        }
        [Authorize(Roles = "User")]
        public ActionResult UserIndex()
        {
            _service.SetRoomStatus();
            var user = _userManager.GetUserAsync(User).Result;
            var userId = user.Id;
            var rezervation = _service.FindByUser(userId);
            var model = _mapper.Map<List<RezervationDTO>, List<RezervationVM>>((List<RezervationDTO>)rezervation);
            return View(model);
        }

        public ActionResult RoomsList()
        {
            _service.SetRoomStatus();
            var rooms = _service.ListRooms();
            var model = _mapper.Map<List<RoomDTO>, List<RoomVM>>((List<RoomDTO>)rooms);
            return View(model);
        }

        // GET: RezervationController/Details/5
        [Authorize(Roles = "Administrator,User")]
        public ActionResult Details(int id)
        {
            if (!_service.isExis(id))
            {
                return NotFound();
            }
            var roomtype = _service.FindById(id);
            var model = _mapper.Map<RezervationVM>(roomtype);
            return View(model);
        }

        // GET: RezervationController/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RezervationController/Create
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RezervationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var rezervation = _mapper.Map<RezervationDTO>(model);

                var isSucces = _service.Create(rezervation);
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

        [Authorize(Roles = "User")]
        public ActionResult UserCreate()
        {
            return View();
        }

        // POST: RezervationController/Create
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate(UserRezervationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var rezervation = _mapper.Map<RezervationDTO>(model);
                var user = _userManager.GetUserAsync(User).Result;
                var userId = user.Id;
                var isSucces = _service.UserCreate(rezervation,userId);
                if (!isSucces)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(UserIndex));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: RezervationController/Edit/5
        [Authorize(Roles = "Administrator,User")]
        public ActionResult Edit(int id)
        {
            if (!_service.isExis(id))
            {
                return NotFound();
            }
            var rezervation = _service.FindById(id);
            IRezervationService.ReturnedRoomID = rezervation.RoomID;
            var model = _mapper.Map<RezervationVM>(rezervation);
            return View(model);
        }

        // POST: RezervationController/Edit/5
        [Authorize(Roles = "Administrator,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RezervationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var rezervation = _mapper.Map<RezervationDTO>(model);
                var isSucces = _service.Update(rezervation);
                if (!isSucces)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }
                var ret = RedirectToAction();

                if (User.IsInRole("Administrator"))
                {
                    ret = RedirectToAction(nameof(Index));
                }
                if (User.IsInRole("User"))
                {
                    ret = RedirectToAction(nameof(UserIndex));
                }

                return ret;
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        public ActionResult CreateRezervation()
        {
            return View();
        }

        // POST: RezervationController/Edit/5
        [Authorize(Roles = "Administrator,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRezervation(int id, RoomsRezervationVM model)
        {
            var rooms = _roomService.FindById(id);
            var roomID = rooms.Id;
            if (!_roomService.isExis(id))
            {
                return NotFound();
            }
            var user = _userManager.GetUserAsync(User).Result;
            var userId = user.Id;
            var rezervation = _mapper.Map<RezervationDTO>(model);

            var isSucces = _service.CreateUserRezervation(rezervation,roomID,userId);
            if (!isSucces)
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
            return RedirectToAction(nameof(UserIndex));
        }

        // GET: RezervationController/Delete/5
        [Authorize(Roles = "Administrator,User")]
        public ActionResult Delete(int id)
        {
            if (!_service.isExis(id))
            {
                return NotFound();
            }
            var roomType = _service.FindById(id);
            var model = _mapper.Map<RezervationVM>(roomType);
            return View(model);
        }

        // POST: RezervationController/Delete/5
        [Authorize(Roles = "Administrator,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RezervationVM model)
        {
            try
            {
                if (model == null)
                {
                    return NotFound();
                }
                var isSucces = _service.Delete(_mapper.Map<RezervationDTO>(model));
                if (!isSucces)
                {
                    return View(model);
                }
                var ret = RedirectToAction();

                    if (User.IsInRole("Administrator"))
                    {
                        ret = RedirectToAction(nameof(Index));
                    }
                    if (User.IsInRole("User"))
                    {
                        ret = RedirectToAction(nameof(UserIndex));
                    }
                
                return ret;
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }
    }
}
