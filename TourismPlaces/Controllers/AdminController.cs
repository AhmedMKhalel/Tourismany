using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using TourismPlaces.IRepository;
using TourismPlaces.ViewModels.Admin;

namespace TourismPlaces.Controllers
{
    //[Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly IRoleRepo _roleRepo;
        private readonly IPlaceApproveRepo _placeRepo;
        private readonly IToastNotification _toast;

        public AdminController(IRoleRepo roleRepo , IPlaceApproveRepo placeRepo,IToastNotification toast)
        {
            _roleRepo = roleRepo;
            _placeRepo = placeRepo;
            _toast = toast;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Role()
        {
            return View(await _roleRepo.GetRolesAsync());
        }




        [HttpGet]
        public async Task<IActionResult> AddUsersToRole(string roleId)
            
        {
            ViewBag.roleId = roleId;
           var role= await _roleRepo.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                _toast.AddErrorToastMessage("This role can not be found");
                return View("Role","Admin");
            }
            var users = await _roleRepo.GetUsersAsync();
            var model = new List<UserRoleViewModel>();
            foreach (var user in users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email=user.Email,
                    

                };
                if (await _roleRepo.IsUserInRole(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> AddUsersToRole(List<UserRoleViewModel> model, string roleId)

        {
           
            var role = await _roleRepo.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                _toast.AddErrorToastMessage("This role can not be found");
                return View("Role", "Admin");
            }
            for (int i = 0; i < model.Count; i++)
            {
                IdentityResult result = null;

                var userViewModel = new UserViewModel()
                {
                    Id = model[i].UserId,
                    Email = model[i].Email,
                    UserName = model[i].UserName,
                };
                var userInRole = await _roleRepo.IsUserInRole(userViewModel, role.Name);

                if (model[i].IsSelected &&!userInRole)
                {
                    result = await _roleRepo.AddUserToRoleAsync(userViewModel, role.Name);
                }
                else if (!model[i].IsSelected && userInRole)
                {
                   result = await _roleRepo.RemoveFromRoleAsync(userViewModel, role.Name);
                }
                else
                   continue;
                
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    _toast.AddSuccessToastMessage("Roles of users has updated suucessfully");
                    return RedirectToAction("Role", "Admin");

                }
            }

            return RedirectToAction("Role", "Admin");
        }



        [HttpGet]
        public async Task<IActionResult> RoleDetails(string roleId)
        {
            var roleVm = await _roleRepo.GetRoleByIdAsync(roleId);
            if (roleVm ==null)
            {
                TempData["error"] = "something went bad";
                return View("Error");
            }
          var users = await  _roleRepo.GetUsersByRoleAsync(roleVm.Name);
            var roleDetailsViewModel = new RoleDetailsViewModel()
            {
                RoleId = roleId,    
                RoleName = roleVm.Name,
                Users = users.ToList()
            };

            return View(roleDetailsViewModel);
        }



        [HttpGet]
        public async Task<IActionResult> ApprovePost()
        {
            return View(_placeRepo.GetApprovedPlaces());
        }
        
    }
}
