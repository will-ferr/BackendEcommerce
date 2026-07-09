using System.Runtime.CompilerServices;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Domain.Entities;

public class OrderItem : Entity
{
    public Guid ProductId {get; private set;}
    public string ProductNameSnapshot {get; private set;}
    public Money UnitPriceSnapshot {get; private set;}
    public int Quantity {get; private set;}


    protected OrderItem(){}

    internal OrderItem(
        Guid productId,
        string productNameSnapshot,
        Money unitPriceSnapshot,
        int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("quantidade deve ser maior que zero.");
        
        ProductId = productId;
        ProductNameSnapshot = productNameSnapshot;
        UnitPriceSnapshot = unitPriceSnapshot;
        Quantity = quantity;

    }

    internal void ChangeQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("A quantidade tem que ser maior que zero.");

            Quantity = newQuantity;

    }

    public Money CalculateSubtotal() => UnitPriceSnapshot.MultiplyBy(Quantity);


}