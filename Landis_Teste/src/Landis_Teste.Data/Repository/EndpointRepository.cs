using Landis_Teste.Business.Interfaces;
using Landis_Teste.Business.Models;

namespace Landis_Teste.Data.Repository
{
    public class EndpointRepository : Repository<Endpoint>, IEndpointRepository
    {
        public EndpointRepository()
        {
        }
    }
}