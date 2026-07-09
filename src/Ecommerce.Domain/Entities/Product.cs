using System.Runtime.CompilerServices;
using System.Security;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Domain.Entities;

public class Product : Entity
{
    public String Name {get; private set;}
    public String? Description {get; private set;}
    public Money Price {get; private set;}
    public int StockQuantity {get; private set;}
    public  Guid CategoryId {get; private set;}
    public bool IsActive {get; private set;}
    

    protected Product(){}

    private Product(
        string name,
        string? description,
        Money price,
        int stockQuantity,
        Guid categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        CategoryId = categoryId;
        IsActive = true;
    }

    public static Product Create(
        String name,
        String? description,
        decimal price,
        int stockQuantity,
        Guid categoryId)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome do produto requerido.");
        
        if (stockQuantity < 0)
            throw new ArgumentException("O estoque inicial não pode ser negativo.");
        
        if (categoryId == Guid.Empty)
            throw new ArgumentException("O produto deve ter uma categoria.");
        
        return new Product(
            name,
            description,
            Money.Create(price),
            stockQuantity,
            categoryId
        );
    }
    
    public void UpdatePrice(decimal newPrice)
    {
        Price = Money.Create(newPrice);
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantidade adicionada tem que ser positiva.");
        
        StockQuantity += quantity;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("quantidade para remover tem que ser positiva.");
        
        if (quantity > StockQuantity)
            throw new InvalidOperationException($"Estoque insuficiente. Disponivel: {StockQuantity}, Requerido: {quantity}");
        
        StockQuantity -= quantity;
    }

    public bool HasSufficientStock(int quantity) => StockQuantity >= quantity;

    public void Deactivate() => IsActive = false;
}
