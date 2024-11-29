using AutoMapper;
using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.Repository;
using FIAP.IRRIGACAO.API.ViewModel;

namespace FIAP.IRRIGACAO.API.Service
{
    public class FaucetService : GenericService<FaucetViewModel, FaucetModel, FaucetRegisterViewModel>, IFaucetService
    {
        public FaucetService(
            IFaucetRepository repository,
            IMapper mapper) : base(repository, mapper)
        {

        }

    }

}
