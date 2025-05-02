using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Trucktive.Core.Specifications;

namespace Trucktive.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T?> GetByIdWithSpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(); // for Count all
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<int> GetCountAsync(ISpecification<T> spec);
        IQueryable<T> GetAllAsQueryable(ISpecification<T>? spec);
        IQueryable<T> GetAllAsQueryable();


        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task AddRange(IEnumerable<T> entities);
    }
}
