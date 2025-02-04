﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using iTechArt.SurveysSite.DomainModel;
using iTechArt.SurveysSite.Foundation;
using iTechArt.SurveysSite.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace iTechArt.SurveysSite.WebApp.Controllers
{
    [Authorize(Roles = RoleNames.AdminRole)]
    public class UsersController : Controller
    {
        private readonly IUserManagementService _userManagementService;


        public UsersController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManagementService.GetAllUsersAsync();
            var usersViewModel = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                RoleNames = user.UserRoles.Select(ur => ur.Role.Name).ToList(),
                DateOfRegistration = user.RegistrationDate
            }).ToList();

            return View(usersViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManagementService.GetUserByIdAsync(id);
            await _userManagementService.DeleteUserAsync(user);

            return RedirectToAction("Index");
        }
    }
}
