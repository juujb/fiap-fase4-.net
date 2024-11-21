using X.PagedList;

namespace FIAP.IRRIGACAO.API.Data.Repository
{
    public interface IGenericRepository<T>
    {
        IPagedList<T> GetAllPaged(int pageNumber, int pageSize);
        T? FindById(long id);
        void Create(T model);
        void Update(T model);
        T? DeleteAndReturn(long id);
    }
}
