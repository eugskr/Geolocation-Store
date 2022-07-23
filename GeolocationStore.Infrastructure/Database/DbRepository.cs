using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GeolocationStore.Application.Providers.Geolocations;
using GeolocationStore.Domain.RepositoryModels;
using Microsoft.EntityFrameworkCore;

namespace GeolocationStore.Infrastructure.Database
{
    public class DbRepository<T>: IDbRepository<T> where T : BaseEntity
    {
        private GeolocationStoreContext Context { get; set; }

        public DbRepository(GeolocationStoreContext context)
        {
            Context = context;
        }

        public async Task<T> Insert(T record)
        {
            await Context.Set<T>().AddAsync(record);
            await Context.SaveChangesAsync();
            return record;
        }
        
        public async Task Delete(T record)
        {
            Context.Set<T>().Remove(record);
            await Context.SaveChangesAsync();
        }
        
        public async Task<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }
        
        public async Task<List<T>> GetAllRecords()
        {
            return await Context.Set<T>().ToListAsync();
        }
    }
}