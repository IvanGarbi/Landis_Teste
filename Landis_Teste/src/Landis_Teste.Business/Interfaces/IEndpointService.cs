using System.Threading.Tasks;
using Landis_Teste.Business.Models;

namespace Landis_Teste.Business.Interfaces
{
    public interface IEndpointService
    {
        Task Adicionar(Endpoint serie);

        Task Atualizar(Endpoint serie);

        Task Remover(string endpointSerialNumber);

        Task<Endpoint> BuscarPorSerialNumber(string endpointSerialNumber);
    }
}