﻿using System.Collections.Generic;
using iTechArt.SurveysSite.DomainModel;
using System.Threading.Tasks;

namespace iTechArt.SurveysSite.Foundation
{
    public interface IUserManagementService
    {
        Task<User> GetUserByUsernameAsync(string userName);

        Task<IReadOnlyCollection<User>> GetAllUsersAsync();

        Task DeleteUserAsync(int id);
    }
}