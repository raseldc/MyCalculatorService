using WebCalculator.Model;

namespace WebCalculator.services
{
    public interface IUserService
    {
       public void CreateUser(User user);
       public User? GetUserByUserName(String userName);
       public void GetUserById(User user);

        public bool checkLogin(String userName, String password);

    }
}
