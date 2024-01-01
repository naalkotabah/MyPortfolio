using Core.Intrtfaces;

public interface IUnitOfWork<T> where T : class
{
    IGenericRepository<T> Entity { get; }
    void Save();
}
