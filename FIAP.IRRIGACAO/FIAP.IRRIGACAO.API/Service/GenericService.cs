using AutoMapper;
using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.Repository;
using FIAP.IRRIGACAO.API.ViewModel;

namespace FIAP.IRRIGACAO.API.Service
{
    public class GenericService<TViewModel, TEntity, TRegisterViewModel> : IGenericService<TViewModel, TEntity, TRegisterViewModel>
    where TEntity : BaseEntity
    where TViewModel : BaseViewModel
    where TRegisterViewModel : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<TViewModel> GetAll()
        {
            var entities = _repository.GetAll();
            return _mapper.Map<IEnumerable<TViewModel>>(entities);
        }

        public IEnumerable<TViewModel> GetAllPaged(int pageNumber, int pageSize)
        {
            var entities = _repository.GetAllPaged(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<TViewModel>>(entities);
        }

        public TViewModel? FindById(long id)
        {
            var entity = _repository.FindById(id);
            return _mapper.Map<TViewModel>(entity);
        }

        public TViewModel Create(TRegisterViewModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            var result = _repository.Create(entity);
            return _mapper.Map<TViewModel>(result);
        }

        public void Update(long id, TRegisterViewModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            entity.Id = id;
            _repository.Update(entity);
        }

        public TViewModel? DeleteAndReturn(long id)
        {
            var entity = _repository.DeleteAndReturn(id);
            return _mapper.Map<TViewModel>(entity);
        }
    }
}
