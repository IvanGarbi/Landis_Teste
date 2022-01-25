using System.Collections.Generic;
using System.Threading.Tasks;
using Landis_Teste.Business.Models;

namespace Landis_Teste.Business.Interfaces
{
    public interface IRepository<TEntity>  where TEntity : Entity
    {
        Task Adicionar(TEntity entity);

        Task Atualizar(TEntity entity);

        Task Remover(string endpointSerialNumber);

        Task<IEnumerable<TEntity>> ListarTodos();

        Task<TEntity> BuscarPorSerialNumber(string endpointSerialNumber);
    }
}