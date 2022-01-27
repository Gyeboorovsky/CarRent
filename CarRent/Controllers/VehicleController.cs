using System.Threading.Tasks;
using CarRent.DAL;
using CarRent.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var result = await _vehicleService.GetAll();
            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult AddVehicleView()
        {
            return View(new Vehicle());
        } 
        
        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> PostVehicle(Vehicle vehicle)
        {
            await _vehicleService.Add(vehicle);
            return RedirectToAction("Index");
        }
    }
}
