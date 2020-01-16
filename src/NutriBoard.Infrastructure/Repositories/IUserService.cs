using System;
using System.Collections.Generic;
using System.Text;
using NutriBoard.Core.Entities;

namespace NutriBoard.Infrastructure.Repositories
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

        IEnumerable<User> GetAllUser();

        User GetById(int id);

        User GetByEmail(string email);

        User GetByToken(Guid token);

        User VerifyUser(int id, Guid token);

        User Create(User user, string password);
        void Update(User userParam, string password = null);
        void Delete(int id);
    }
}
