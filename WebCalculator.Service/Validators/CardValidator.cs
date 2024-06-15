using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Service.Model;

namespace WebCalculator.Service.Validators
{
    public static  class CardValidator
    {
        public static bool Validate(this CardRequest cardRequest)
        {
            if (!cardRequest.ValidateCVC())
            {
                throw new Exception("Invalid cvc");
            }

            if (cardRequest.CardNumber.Length != 16)
            {
                throw new Exception("Invalid card number");
            }

            if (cardRequest.ExpireMonth < 1 || cardRequest.ExpireMonth > 12)
            {
                throw new Exception("Invalid Expire Month.");
            }

            if (cardRequest.ExpireYear < 2024 || cardRequest.ExpireYear > 2034) // say valid year till 2034
            {
                throw new Exception("Invalid Expire Year.");
            }

            if (cardRequest.CountryId < 1 || cardRequest.CountryId > 200) // say there are 200 country in our db
            {
                throw new Exception("Invalid country selected.");
            }
            return true;
        }

        public static bool ValidateCVC(this CardRequest cardRequest)
        {
            if (cardRequest.CVC.Length != 3)
            {
                return false;
            }

            return true;
        }

    }
}
