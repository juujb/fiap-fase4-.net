namespace FIAP.IRRIGACAO.API.Repository
{
    public interface IGenericRepository<T>
    {
        List<T> GetAllPaged(int pageNumber, int pageSize);
        T? FindById(long id);
        void Create(T model);
        void Update(T model);
        T? DeleteAndReturn(long id);
    }
}
