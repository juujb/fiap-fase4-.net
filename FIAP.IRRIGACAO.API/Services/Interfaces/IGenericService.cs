using X.PagedList;

namespace FIAP.IRRIGACAO.API.Services
{
    public interface IGenericService<T>
    {
        IPagedList<T> GetAllPaged(int? pageNumber);
        T? FindById(long id);
        void Create(T model);
        void Update(T model);
        T? Delete(long id);
    }
}
