using DTOsTask.Controllers;

namespace DTOsTask.Service
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUser(string email, string password);
    }
}