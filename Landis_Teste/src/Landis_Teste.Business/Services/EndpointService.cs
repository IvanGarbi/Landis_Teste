using System.Threading.Tasks;
using Landis_Teste.Business.Interfaces;
using Landis_Teste.Business.Models;
using Landis_Teste.Business.Validations;

namespace Landis_Teste.Business.Services
{
    public class EndpointService : BaseService, IEndpointService
    {
        private readonly IEndpointRepository _endpointRepository;
        public EndpointService(IEndpointRepository endpointRepository, INotificador notificador) : base(notificador)
        {
            _endpointRepository = endpointRepository;
        }


        public async Task Adicionar(Endpoint endpoint)
        {
            if (!ExecutarValidacao(new EndpointValidation(), endpoint))
            {
                return;
            }

            var entidadeDuplicada = _endpointRepository.BuscarPorSerialNumber(endpoint.EndpointSerialNumber).Result;

            if (entidadeDuplicada != null)
            {
                Notificar("Já existe um Endpoint com esse Serial Number!");
                return;
            }

            await _endpointRepository.Adicionar(endpoint);
        }

        public async Task Atualizar(Endpoint endpoint)
        {
            if (!ExecutarValidacao(new EndpointValidation(), endpoint))
            {
                return;
            }

            await _endpointRepository.Atualizar(endpoint);
        }

        public async Task Remover(string endpointSerialNumber)
        {
            var entidade = _endpointRepository.BuscarPorSerialNumber(endpointSerialNumber).Result;

            if (entidade == null)
            {
                Notificar("Não existe um Endpoint com esse Serial Number.");
                return;
            }

            await _endpointRepository.Remover(endpointSerialNumber);
        }

        public async Task<Endpoint> BuscarPorSerialNumber(string endpointSerialNumber)
        {
            var entidade = _endpointRepository.BuscarPorSerialNumber(endpointSerialNumber).Result;

            if (entidade == null)
            {
                Notificar("Não existe um Endpoint com esse Serial Number.");
                return entidade;
            }

            return entidade;
        }
    }
}