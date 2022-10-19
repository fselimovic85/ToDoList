using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TO_DO_LIST.Models;

namespace TO_DO_LIST.Service.UserServices
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string passwprd, int TimeZone, string Email);
        Task<ServiceResponse<string>> Login(string username, string passwprd);
        Task<bool> UserExists(string username);
    }
}
