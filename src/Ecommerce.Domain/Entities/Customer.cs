using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using System.Net.Mail;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Domain.Entities;

public class Customer : Entity
{
    public string Name {get;private set;}
    public Email Email{get;private set;}
    public string PasswordHash {get; private set;}
    public DateTime CreatedAt {get; private set;}
    public bool IsActive {get; private set;}


    protected Customer(){}

    private Customer(string name, Email email, string passwordHash)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public static Customer Register (string name, string email, string passwordHash)
    {
        if(String.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome é obrigatorio.");
        if(name.Length < 3 )
            throw new ArgumentException("nome deve ter pelo menos 3 caracteres.");

        var emailValid = Email.Create(email);

        return new Customer(name, emailValid, passwordHash);
    }

    public void UpdateName(string newName)
    {
        if(string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Nome e obrigatorio");
        
        Name = newName;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("Cliente já está inativo");
        
        IsActive = false;
    }

}
