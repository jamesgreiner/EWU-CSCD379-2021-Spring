using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public interface IUserManager
    {
        ICollection<User> List();
        User? GetItem(int id);
        bool Remove(int id);
        User Create(User user);
        void Save(User user);
    }
}