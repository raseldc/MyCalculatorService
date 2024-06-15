using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Service.Model;
using WebCalculator.Service.Validators;

namespace WebCalculator.Test
{
    public class PaymentLogicTest
    {
        [TestCase("1234567812345678", 2, 2024, "123", 1)]
        [TestCase("1234567812345678", 8, 2030, "456", 100)]
        [TestCase("1234567812345678", 12, 2032, "789", 10)]
        public void Card_should_be_valid(string cardNumber, int month, int year, string cvc, int countryId)
        {
            // arrange
            var cardRequest = new CardRequest(cardNumber, month, year, cvc, countryId);

            // act
            var isValid = cardRequest.Validate();

            // assert
            Assert.That(isValid, Is.EqualTo(true));
        }

        [TestCase("1234567812345", 2, 2024, "123", 1)]
        [TestCase("1234567812345678", 0, 2030, "456", 100)]
        [TestCase("1234567812345678", 10, 2052, "789", 10)]
        [TestCase("1234567812345678", 10, 2026, "7890", 10)]
        [TestCase("1234567812345678", 10, 2026, "789", 300)]
        public void Card_should_be_invalid(string cardNumber, int month, int year, string cvc, int countryId)
        {
            // arrange
            var cardRequest = new CardRequest(cardNumber, month, year, cvc, countryId);

            // act
            var isValid = true;
            try
            {
                isValid = cardRequest.Validate();
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            // assert
            Assert.That(isValid, Is.EqualTo(false));
        }
    }
}
