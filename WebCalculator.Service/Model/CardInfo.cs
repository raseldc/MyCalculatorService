using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Model;

namespace WebCalculator.Service.Model
{
    public class CardInfo
    {
        public int Id { get; set; }
        public string LastFourDigit { get; set; }
        public string ExpiryYear { get; set; }
        public string ExpiryMonth { get; set; }
        public User User { get; set; }
    }
}
