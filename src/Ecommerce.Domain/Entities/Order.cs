using Ecommerce.Domain.Common;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Domain.Entities;

public enum OrderStatus
{
    Pending,
    Paid,
    Shipped,
    Delivered,
    Cancelled
}

public class Order : Entity
{
    private readonly List<OrderItem> _items = new();

    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    protected Order() { }

    private Order(Guid customerId)
    {
        CustomerId = customerId;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public static Order Start(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("Order must be linked to a customer.");

        return new Order(customerId);
    }

    public void AddItem(Guid productId, string productName, decimal unitPrice, int quantity)
    {
        EnsureCanBeModified();

        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem is not null)
        {
            existingItem.ChangeQuantity(existingItem.Quantity + quantity);
            return;
        }

        var newItem = new OrderItem(
            productId,
            productName,
            Money.Create(unitPrice),
            quantity
        );

        _items.Add(newItem);
    }

    public void RemoveItem(Guid productId)
    {
        EnsureCanBeModified();

        var item = _items.FirstOrDefault(i => i.ProductId == productId)
            ?? throw new InvalidOperationException("Item not found in this order.");

        _items.Remove(item);
    }

    public Money CalculateTotal()
    {
        var total = Money.Zero;

        foreach (var item in _items)
            total = total.Add(item.CalculateSubtotal());

        return total;
    }

    public void ConfirmPayment()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Only pending orders can have payment confirmed.");

        if (_items.Count == 0)
            throw new InvalidOperationException("Cannot pay for an order with no items.");

        Status = OrderStatus.Paid;
    }

    public void Cancel()
    {
        if (Status is OrderStatus.Delivered or OrderStatus.Cancelled)
            throw new InvalidOperationException($"Cannot cancel an order with status {Status}.");

        Status = OrderStatus.Cancelled;
    }

    private void EnsureCanBeModified()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException(
                $"Cannot modify items of an order with status {Status}."
            );
    }
}