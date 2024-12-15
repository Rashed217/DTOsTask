using DTOsTask.Controllers;

namespace DTOsTask.Repository
{
    public class UserRepo : IUserRepo
    {

        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUSer(string email, string password)
        {
            return _context.Users.Where(u => u.Email == email & u.Password == password).FirstOrDefault();
        }

    }
}
