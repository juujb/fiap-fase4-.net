using AutoMapper;
using FIAP.IRRIGACAO.API.Data.Repository;
using FIAP.IRRIGACAO.API.Models;
using FIAP.IRRIGACAO.API.ViewModels;
using X.PagedList;

namespace FIAP.IRRIGACAO.API.Services
{
    public class FaucetService : IFaucetService
    {
        private readonly IFaucetRepository _repository;
        private readonly IMapper _mapper;

        public FaucetService(
            IFaucetRepository repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public FaucetViewModel? FindById(long id)
        {
            var model = _repository.FindById(id);

            var faucet = _mapper.Map<FaucetViewModel>(model);

            return faucet;
        }

        public FaucetViewModel? Delete(long id)
        {
            var model = _repository.DeleteAndReturn(id);

            if (model == null) 
                return null;

            return _mapper.Map<FaucetViewModel>(model);
        }

        public void Update(FaucetViewModel model)
        {
            _repository.Update(_mapper.Map<FaucetModel>(model));
        }

        public void Create(FaucetViewModel model)
        {
            _repository.Create(_mapper.Map<FaucetModel>(model));
        }

        public IPagedList<FaucetViewModel> GetAllPaged(int? pageNumber)
        {
            int page = pageNumber ?? 1;
            int pageSize = 10;

            var faucetList = _repository.GetAllPaged(page, pageSize);

            var mappedItems = _mapper.Map<IEnumerable<FaucetViewModel>>(faucetList);
            return new StaticPagedList<FaucetViewModel>(mappedItems, faucetList.PageNumber, faucetList.PageSize, faucetList.TotalItemCount);
        }

    }
}
