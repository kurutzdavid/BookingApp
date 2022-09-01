using Booking_app.Models;
using Booking_app.Services.Interfaces;
using BookingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRezervationService _rezervationService;

        public HomeController(ILogger<HomeController> logger,
            IRezervationService rezervationService)
        {
            _logger = logger;
            _rezervationService = rezervationService;
        }

        public async Task<IActionResult> Index()
        {
            _rezervationService.SetRoomStatus();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}