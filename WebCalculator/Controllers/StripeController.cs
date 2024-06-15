
using Microsoft.AspNetCore.Mvc;
using MimeKit.Utils;
using System.Security.Cryptography;
using System.Text;
using WebCalculator.Model;
using WebCalculator.Resources;
using WebCalculator.Security;
using WebCalculator.Service.Model;
using WebCalculator.Service.services;
using WebCalculator.Service.Utils;
using WebCalculator.services;

namespace WebCalculator.Controllers
{
    [Route("stripe")]
    public class StripeController : ControllerBase
    {

        private readonly IStripeService _stripeService;
        private readonly IUserService _userService;
        private readonly ICardInfoService _cardInfoService;
        private readonly IEmailService _emailService;
        private readonly JwtAuthenticationManager _jwtAuthenticationManager;
        public StripeController(IStripeService stripeService, IUserService userService, ICardInfoService cardInfoService, IEmailService emailService,JwtAuthenticationManager jwtAuthenticationManager )
        {
            _emailService = emailService;
            _stripeService = stripeService;
            _userService = userService;
            _cardInfoService = cardInfoService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost("customer")]
        public async Task<ActionResult<CustomerResource>> CreateCustomer([FromBody] CreateCardResource resource,
            CancellationToken cancellationToken)
        {
            var dbUser = _userService.GetUserByUserName(resource.Email);
            if(dbUser!=null)    
            {
                return BadRequest("User already exists");
            }

            var response = await _stripeService.CreateCustomer(resource, cancellationToken);

            if (resource != null)
            {
                var createChargeResource = new CreateChargeResource(
                    Currency: "usd",
                    Amount: 1000,
                    CustomerId: response.CustomerId,
                    ReceiptEmail: response.Email,
                    Description:"");
                
               var result = await _stripeService.CreateCharge(createChargeResource, cancellationToken);
                if(result != null)
                {
                    createUserAndCardInfo(resource, response);
                    //_userService.CreateUser(user);
                }
            }
            var token = _jwtAuthenticationManager.GenerateToken(resource.Email);

            return Ok(new { Token = token });
        }
        private void createUserAndCardInfo(CreateCardResource resource, CustomerResource response)
        {
            var password = PasswordHashManagement.CreateRandomPassword();
            var hashString = PasswordHashManagement.HashPassword(password);

            var user = new User
            {
                Name = response.Name,
                Email = response.Email,
                Password = hashString,
                UserName = response.Email
            };
            var cardInfo = new CardInfo
            {

                LastFourDigit = resource.Number.Substring(resource.Number.Length - 4),
                ExpiryYear = resource.ExpiryYear,
                ExpiryMonth = resource.ExpiryMonth,
                User = user

            };
            _cardInfoService.CreateCardInfo(cardInfo);


            SendEmailForPassword(response.Email, password);
           

        }
        private void SendEmailForPassword(string email, string password)
        {
            _emailService.SendEmailForPassword(email, password);
        }
        [HttpPost("charge")]
        public async Task<ActionResult<ChargeResource>> CreateCharge([FromBody] CreateChargeResource resource, CancellationToken cancellationToken)
        {
            var response = await _stripeService.CreateCharge(resource, cancellationToken);
            return Ok(response);
        }
    }
}
