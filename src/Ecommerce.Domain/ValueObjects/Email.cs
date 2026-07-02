using System.Data.Common;

namespace Ecommerce.Domain.ValueObjects;

public sealed class Email
{
    public string Value{get;}

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email não pode ser vazio.");
        
        if (value.Contains('@') || value.Contains('.'))
            throw new ArgumentException("Email em formato inválido.");
        
        return new Email(value.Trim().ToLowerInvariant());
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj)
        => obj is Email other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}