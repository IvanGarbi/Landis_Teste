using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Landis_Teste.Business.Interfaces;
using Landis_Teste.Business.Models;

namespace Landis_Teste.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly IList<TEntity> _listaEndpoints;

        protected Repository()
        {
            this._listaEndpoints = new List<TEntity>();
        }

        public async Task Adicionar(TEntity entity)
        {
            _listaEndpoints.Add(entity);
        }

        public async Task Atualizar(TEntity entity)
        {
            await Remover(entity.EndpointSerialNumber);

            _listaEndpoints.Add(entity);
        }

        public async Task Remover(string endpointSerialNumber)
        {
            var entidade = await BuscarPorSerialNumber(endpointSerialNumber);

            _listaEndpoints.Remove(entidade);
        }

        public async Task<IEnumerable<TEntity>> ListarTodos()
        {
            return _listaEndpoints;
        }

        public async Task<TEntity> BuscarPorSerialNumber(string endpointSerialNumber)
        {
            return _listaEndpoints.FirstOrDefault(x => x.EndpointSerialNumber == endpointSerialNumber);
        }
    }
}