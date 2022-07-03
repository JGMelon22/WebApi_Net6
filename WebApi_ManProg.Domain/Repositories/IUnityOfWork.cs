namespace WebApi_ManProg.Domain.Repositories;

public interface IUnityOfWork : IDisposable // Por boas práticas, colocamos um dispose 
{
    Task BeginTransaction();
    Task Commit();
    Task Rollback();
}