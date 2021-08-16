﻿using iTechArt.SurveysSite.DomainModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace iTechArt.SurveysSite.Foundation
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<User> _userManager;


        public UserManagementService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task<User> GetUserByUsernameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName), "Username cannot be null");
            }

            var user = await _userManager.FindByNameAsync(userName);

            return user;
        }
    }
}