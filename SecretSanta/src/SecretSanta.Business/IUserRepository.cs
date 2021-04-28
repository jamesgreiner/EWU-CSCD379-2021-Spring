using SecretSanta.Data;
using System.Collections.Generic;

namespace SecretSanta.Business
{
    public interface IUserRepository
    {
        ICollection<User> List();
        User? GetItem(int id);
        bool Remove(int id);
        User Create(User item);
        void Save(User item);
    }
}