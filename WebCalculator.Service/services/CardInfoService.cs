using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Model;
using WebCalculator.Service.Model;

namespace WebCalculator.Service.services
{
    public class CardInfoService : ICardInfoService
    {
        private readonly ApplicationDbContext _context;

        public CardInfoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreateCardInfo(CardInfo cardInfo)
        {
            if (cardInfo == null)
            {
                throw new ArgumentNullException(nameof(cardInfo));
            }

            try
            {
                _context.CardInfo.Add(cardInfo);
                _context.SaveChanges();
          
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }

        }

        public CardInfo GetUserById(User user)
        {
            throw new NotImplementedException();
        }
    }
}
