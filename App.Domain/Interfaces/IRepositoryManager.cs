namespace App.Domain.Interfaces;

public interface IRepositoryManager : IDisposable
{
    Task Save();
}
