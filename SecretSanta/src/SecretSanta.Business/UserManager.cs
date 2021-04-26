using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class UserManager : IUserManager
    {
        public User Create(User user)
        {
            DeleteMe.Users.Add(user);
            return user;
        }

        public User? GetItem(int id)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<User> List()
        {
            return DeleteMe.Users;
        }

        public bool Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public User Save(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}