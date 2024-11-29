using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.ViewModel;

namespace FIAP.IRRIGACAO.API.Service
{
    public interface IGenericService<TViewModel, TEntity, TRegisterViewModel>
    where TEntity : BaseEntity
    where TViewModel : class
    {
        IEnumerable<TViewModel> GetAll();
        IEnumerable<TViewModel> GetAllPaged(int pageNumber, int pageSize);
        TViewModel? FindById(long id);
        TViewModel Create(TRegisterViewModel model);
        void Update(long id, TRegisterViewModel model);
        TViewModel? DeleteAndReturn(long id);
    }
}
