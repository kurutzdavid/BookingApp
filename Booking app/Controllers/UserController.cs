using AutoMapper;
using BookingApp.Models;
using BookingApp.Models.DTOS;
using Booking_app.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Booking_app.Services.Interfaces;

namespace Booking_app.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRezervationService _rezervationService;
        private readonly UserManager<User> _userManager;
        


        public UserController( IMapper mapper, UserManager<User> userManager,
            IRezervationService rezervationService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _rezervationService = rezervationService;
        }
        // GET: UserController
        public ActionResult Index()
        {
            _rezervationService.SetRoomStatus();
            var users = _userManager.GetUsersInRoleAsync("User").Result;
            var model = _mapper.Map<List<UserVM>>(users);
            return View(model); 
        }

        public ActionResult ListAdmins()
        {
            var users = _userManager.GetUsersInRoleAsync("Administrator").Result;
            var model = _mapper.Map<List<AdminVM>>(users);
            return View(model);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserVM model)
        {
            try
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                _userManager.AddToRoleAsync(user, "User").Wait();   

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var model = _mapper.Map<UserVM>(user);
            return View(model);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Edit(string id, UserVM model)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(id);
                user.PhoneNumber = model.PhoneNumber;
                user.LastName = model.LastName;
                user.FirstName = model.FirstName;
                user.Address = model.Address;
               
                IdentityResult rezult = await _userManager.UpdateAsync(user);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var model = _mapper.Map<UserVM>(user);
            return View(model);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, UserVM model)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(id);
                
                IdentityResult rezult = await _userManager.DeleteAsync(user);

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
