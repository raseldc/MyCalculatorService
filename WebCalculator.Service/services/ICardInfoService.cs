using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Model;
using WebCalculator.Service.Model;

namespace WebCalculator.Service.services
{
    public interface ICardInfoService
    {
        public CardInfo GetUserById(User user);
        public void CreateCardInfo(CardInfo cardInfo);
    }
}
