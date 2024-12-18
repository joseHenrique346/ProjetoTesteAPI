using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Infrastructure.Repositories;
using ProjetoTesteAPI.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    private BrandRepository _brandRepository;
    public BrandRepository BrandRepository => _brandRepository ??= new BrandRepository(_context);

    private ClientRepository _clientRepository;
    public ClientRepository ClientRepository => _clientRepository ??= new ClientRepository(_context);

    private OrderRepository _orderRepository;
    public OrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context);

    private ProductRepository _productRepository;
    public ProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

    public AppDbContext DbContext => _context;

    public void Commit()
    {
        _context.SaveChanges();
    }
    
    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}