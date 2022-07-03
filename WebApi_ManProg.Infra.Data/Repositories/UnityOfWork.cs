using Microsoft.EntityFrameworkCore.Storage;
using WebApi_ManProg.Domain.Repositories;
using WebApi_ManProg.Infra.Data.DataContext;

namespace WebApi_ManProg.Infra.Data.Repositories;

public class UnityOfWork : IUnityOfWork
{
    // Conexão com o banco...
    private readonly ApplicationDbContext _dbContext;

    // E a transação
    /// <summary>
    ///     Não estamos usando o "readonly"
    ///     pois precisamos manipular fora do construtor
    /// </summary>
    private IDbContextTransaction _dbContextTransaction;

    public UnityOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Dispose()
    {
        _dbContextTransaction?.Dispose(); // Se ficou algum resquício, mata a transação mesm oassim
    }

    public async Task BeginTransaction()
    {
        _dbContextTransaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        await _dbContext.Database.CommitTransactionAsync();
    }

    public async Task Rollback()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }
}