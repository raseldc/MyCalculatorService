namespace WebCalculator.Resources;

    public record CreateCardResource(
        string Name,
        string Number,
        string ExpiryYear,
        string ExpiryMonth,
        string Cvc,
        string Email
        );
