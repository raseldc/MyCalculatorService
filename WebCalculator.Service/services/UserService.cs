using WebCalculator.Model;
using WebCalculator.Service.Utils;

namespace WebCalculator.services
{

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                Console.WriteLine($"User {user.UserName} created successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public void GetUserById(User user)
        {
            throw new NotImplementedException();
        }

        public User? GetUserByUserName(String userName)
        {
            return _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
        }
        public bool checkLogin(String userName, String password)
        {
            User? user = GetUserByUserName(userName);
            if (user == null)
            {
                return false;
            }

           return PasswordHashManagement.VerifyPassword( password, user.Password);
        }
    }
}
