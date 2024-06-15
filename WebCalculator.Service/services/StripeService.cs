using Stripe;
using WebCalculator.Resources;

namespace WebCalculator.services
{
    public class StripeService : IStripeService
    {

        private readonly TokenService _tokenService;
        private readonly CustomerService _customerService;
        private readonly ChargeService _chargeService;
        private readonly CardService _cardService;


        public StripeService(
            TokenService tokenService,
            CustomerService customerService,
            ChargeService chargeService)
        {
            _tokenService = tokenService;
            _customerService = customerService;
            _chargeService = chargeService;
        }

        public async Task<CustomerResource> CreateCustomer(CreateCardResource resource, CancellationToken cancellationToken)
        {
           
            var tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Name = resource.Name,
                    Number = resource.Number,
                    ExpYear = resource.ExpiryYear,
                    ExpMonth = resource.ExpiryMonth,
                    Cvc = resource.Cvc
                }
            };
            var token = await _tokenService.CreateAsync(tokenOptions, null, cancellationToken);

            var customerOptions = new CustomerCreateOptions
            {
                Email = resource.Email,
                Name = resource.Name,
                Source = token.Id
            };
            var customer = await _customerService.CreateAsync(customerOptions, null, cancellationToken);

            return new CustomerResource(customer.Id, customer.Email, customer.Name);


        }

        public async Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken)
        {
            var chargeOptions = new ChargeCreateOptions
            {
                Currency = resource.Currency,
                Amount = resource.Amount,
                ReceiptEmail = resource.ReceiptEmail,
                Customer = resource.CustomerId,
                Description = resource.Description
            };

            var charge = await _chargeService.CreateAsync(chargeOptions, null, cancellationToken);

            return new ChargeResource(charge.Id, charge.Currency, charge.Amount, charge.CustomerId, charge.ReceiptEmail,
                charge.Description);

        }
    }
}

