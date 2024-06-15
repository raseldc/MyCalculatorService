using WebCalculator.Resources;

namespace WebCalculator.services
{
    public interface IStripeService
    {
        Task<CustomerResource> CreateCustomer(CreateCardResource resource, CancellationToken cancellationToken);
        Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken);

    }
}
