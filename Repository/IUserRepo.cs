using DTOsTask.Controllers;

namespace DTOsTask.Repository
{
    public interface IUserRepo
    {
        void AddUser(User user);
        User GetUSer(string email, string password);
    }
}