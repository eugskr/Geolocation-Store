using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GeolocationStore.Domain.RepositoryModels;

namespace GeolocationStore.Application.Providers.Geolocations
{
    public interface IDbRepository<T> where T : BaseEntity
    {
        Task<T> Insert(T record);
        Task Delete(T record);
        Task<T> GetBy(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllRecords();
    }
}