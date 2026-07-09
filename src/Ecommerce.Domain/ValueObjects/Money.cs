namespace Ecommerce.Domain.ValueObjects;

public readonly struct Money
{
    public decimal Amount{get;}

    private Money(decimal amount)
    {
        Amount = amount;
    }
    public static Money Create(decimal amount)
    {
        if(amount < 0)
            throw new ArgumentException("Valor não pode ser negativo.");
        
        return new Money(Math.Round(amount,2,MidpointRounding.ToEven));
    }

    public static Money Zero => new(0);

    public Money Add(Money other) => Create(Amount + other.Amount);

    public Money MultiplyBy(int quantity)
    {
        if (quantity < 0)
        throw new ArgumentException("Quantidade não pode ser negativa");
        return Create(Amount * quantity);
    }

    public static implicit operator decimal(Money money) => money.Amount;

    public override string ToString() => Amount.ToString("C2");
}



