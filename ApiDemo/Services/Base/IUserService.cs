using DataAccessLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApiDemo.Services.Base
{
    public interface IUserService 
    {

        Task<IEnumerable<User>> GetAllUsersAsync();

        User GetUserById(int id);

        Task<User> GetUserByEmail(string email);

        Task<IEnumerable<User>> GetUsersByNameAsync(string name);
         
    }
}