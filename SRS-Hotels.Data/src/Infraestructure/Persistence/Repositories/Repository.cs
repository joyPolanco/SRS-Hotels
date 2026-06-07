using Microsoft.EntityFrameworkCore;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.Infraestructure.Persistence.Contexts;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Repositories
{
    public class Repository<T>(HotelContext hotelContext) : IRepository<T> where T : class
    {
        public async Task<T> AddAndReturnAsync(T entity)
        {
            var result = await hotelContext.Set<T>().AddAsync(entity);
            await hotelContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddAsync(T entity)
        {
            await hotelContext.Set<T>().AddAsync(entity);
            await hotelContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await hotelContext.Set<T>().AddRangeAsync(entities);
            await hotelContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            hotelContext.Set<T>().Remove(entity);
            await hotelContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await hotelContext.Set<T>().FindAsync(id);

            if (entity is null)
                return;

            hotelContext.Set<T>().Remove(entity);
            await hotelContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsEnumerableAsync()
        {
            return await hotelContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllAsListAsync()
        {
            return await hotelContext.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return hotelContext.Set<T>().AsQueryable();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await hotelContext.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAndReturnAsync(T entity)
        {
            hotelContext.Set<T>().Update(entity);
            await hotelContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            hotelContext.Set<T>().Update(entity);
            await hotelContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            hotelContext.Set<T>().UpdateRange(entities);
            await hotelContext.SaveChangesAsync();
        }
    }
}