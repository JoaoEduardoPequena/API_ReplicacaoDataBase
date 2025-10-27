namespace Persistence.DbContexts
{
    public interface IAppDBContextRouter
    {
        public IQueryable<T> Query<T>() where T : class;
        public Task AddAsync<T>(T entity) where T : class;
        public Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;
        public Task<int> SaveChangesAsync();
    }
}
