using Microsoft.Extensions.DependencyInjection;
using App.Domain.Interfaces;
using App.Infrastructure.Data;

namespace App.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _context;
    private IServiceProvider _serviceProvider;
    public RepositoryManager(ApplicationDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }
    public async void Dispose() => await _context.DisposeAsync();
    public async Task Save() => await _context.SaveChangesAsync();
}
