using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Models;
using System.Linq;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public bool IsValidClient(string email, string password)
    {
        var client = _context.Clients.SingleOrDefault(c => c.Email == email);

        if (client == null)
        {
            return false; 
        }

        return VerifyPasswordHash(password, client.PasswordHash);
    }

    public void UpdateClient(Client client)
    {
        _context.Clients.Update(client); 
        _context.SaveChanges(); 
    }

    public Client? GetClientByEmail(string email)
    {
        return _context.Clients.SingleOrDefault(c => c.Email == email);
    }


    private bool VerifyPasswordHash(string password, string storedHash)
    {
        var hashBytes = Convert.FromBase64String(storedHash);
        return KeyDerivation.Pbkdf2(
            password: password,
            salt: hashBytes.Take(16).ToArray(), 
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32) 
            .SequenceEqual(hashBytes.Skip(16).Take(32)); 
    }

    public void RegisterClient(string name, string email, string password, string cpf, string phone)
    {
        var salt = new byte[16];
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32));

        var client = new Client
        {
            Name = name,
            Email = email,
            CPF = cpf,
            Phone = phone,
            PasswordHash = Convert.ToBase64String(salt.Concat(Convert.FromBase64String(passwordHash)).ToArray()),
            Role = "client"
        };


        _context.Clients.Add(client);
        _context.SaveChanges();
    }

}