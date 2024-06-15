using Moq;
using NUnit.Framework;
using Stripe;
using Stripe.Issuing;
using System;
using System.Threading;
using WebCalculator.Resources;
using WebCalculator.Service;
using WebCalculator.Service.Model;
using WebCalculator.Service.Model;
using WebCalculator.Service.Validators;
using WebCalculator.services;

namespace WebCalculator.Test
{
    public class StripServiceTest
    {
        private IStripeService _mockStripService;


        private Stripe.TokenService _tokenService;
        private CustomerService _customerService;
        private ChargeService _chargeService;
        
        [SetUp]
        public void Setup()
        {
            // Initialize your mock object
            _tokenService = new Stripe.TokenService();
            
            _customerService = new CustomerService();
            _chargeService = new ChargeService();
            _mockStripService = new StripeService(_tokenService, _customerService, _chargeService);



        }
        [Test]
        public async Task CreateCustomer_ShouldReturnTrue_WhenCardIsValidAsync()
        {
            StripeConfiguration.ApiKey = "sk_test_4eC39HqLyjWDarjtT1zdp7dc";

            // Arrange            
            var _validCardRequest = new CreateCardResource("Shamiul", "4242424242424242","2027","07","123","shamiulislam045@gmail.com");
                CancellationToken cancellationToken = new CancellationToken(false);

            // Act
            var result = await _mockStripService.CreateCustomer(_validCardRequest, cancellationToken);

            // Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Email == _validCardRequest.Email);
            Assert.IsTrue(result.Name == _validCardRequest.Name);
            
        }
    }
}
