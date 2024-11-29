namespace FIAP.IRRIGACAO.API.Repository
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllPaged(int pageNumber, int pageSize);
        T? FindById(long id);
        T? Create(T model);
        void Update(T model);
        T? DeleteAndReturn(long id);
    }
}
