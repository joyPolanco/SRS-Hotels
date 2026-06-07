namespace SRS_Hotels.Data.src.BuildingBlocks.Abstractions
{
    public interface IRepository <T>
    {

        public Task<T> GetByIdAsync(Guid id);
        
        public Task<List<T>> GetAllAsListAsync();
        public Task<IEnumerable<T>> GetAllAsEnumerableAsync();

        public IQueryable<T> GetAllAsQueryable();
        public Task AddAsync(T entity);
        public Task AddRangeAsync(IEnumerable<T> entities);

        public Task<T> AddAndReturnAsync(T entity);

        public Task UpdateAsync(T entity);
        public Task UpdateRangeAsync(IEnumerable<T> entities);
        public Task<T> UpdateAndReturnAsync(T entity);
        public Task DeleteAsync(T entity);

        public Task DeleteByIdAsync(Guid id);
    }
}
