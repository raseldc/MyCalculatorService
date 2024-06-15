namespace WebCalculator.services
{
    public interface IEmailService
    {
        public void SendEmailForPassword(string email,string password);

    }
}
