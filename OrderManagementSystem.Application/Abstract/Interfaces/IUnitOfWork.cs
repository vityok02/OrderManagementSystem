namespace Application.Abstract.Interfaces;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken token);
}
