using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movimentos.Data.Repositories.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> GetEntityById(int id);
        Task<IEnumerable<T>> List();
        Task Update(T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<List<T>> BuscarPorCampo(Expression<Func<T, bool>> predicate);
    }
}
