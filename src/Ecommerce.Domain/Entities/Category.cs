using System.Reflection.Metadata;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities;

public class Category : Entity
{
    public string Name {get; private set;}
    public bool IsActivate {get; private set;}

    protected Category() {}

    private Category(string name)
    {
        Name = name;
        IsActivate = true;
    }

    public static Category Create(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome da categoria requerido");
        
        if(name.Length < 2)
            throw new ArgumentException("Nome da categoria tem que ter mais de dois caracteres.");
        
        return new Category(name);
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("nome da categoria requerido.");
        
        Name = newName;
    }

    public void Deactivate() => IsActivate = false;
    public void Activate() => IsActivate = true;
}