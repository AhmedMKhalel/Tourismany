using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TourismPlaces.IRepository;

namespace TourismPlaces.Controllers
{
    public class UserController : Controller
    {
        private readonly IPlaceRepo _placeRepo;


        public UserController(IPlaceRepo placeRepo)
        {
            _placeRepo = placeRepo;

        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var places = _placeRepo.GetAllPlacesForUser()
                .Where(p => p.UserId == userId)
                .ToList();

            return View(places);
        }








    }
}
