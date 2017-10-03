using ApiDemo.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer.Database;
using DataAccessLayer.Generic;
using System.Threading.Tasks;

namespace ApiDemo.Services
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await unitOfWork.Users.GetAsync(orderBy: x => x.OrderBy(y => y.Id));
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await unitOfWork.Users.GetSingleAsync(x => x.EmailAddress == email, y => y.OrderBy(z => z.Id));
        }

        public User GetUserById(int id)
        {
            return unitOfWork.Users.GetByID(id);
        }

        public async Task<IEnumerable<User>> GetUsersByNameAsync(string name)
        {
            return await unitOfWork.Users.GetAsync(x => x.Name == name, y => y.OrderBy(z => z.Id));
        }
 
    }
}