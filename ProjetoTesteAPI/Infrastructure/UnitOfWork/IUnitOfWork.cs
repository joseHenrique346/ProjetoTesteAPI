using ProjetoTesteAPI.Infrastructure.Interfaces;
using ProjetoTesteAPI.Infrastructure.Repositories;
using System;

namespace ProjetoTesteAPI.Infrastructure
{

    public interface IUnitOfWork : IDisposable
    {
        BrandRepository BrandRepository { get; }
        ClientRepository ClientRepository { get; }
        OrderRepository OrderRepository { get; }
        ProductRepository ProductRepository { get; }
        void Commit();
    }
}