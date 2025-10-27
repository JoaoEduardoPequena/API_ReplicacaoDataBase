using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.DbContexts
{
    public class AppDBContextRouter : IAppDBContextRouter
    {
        private readonly WriteDbContext _writeContext;
        private readonly ReadReplicaFactory _replicaFactory;
        private readonly ILogger<AppDBContextRouter> _logger;

        public AppDBContextRouter(WriteDbContext writeContext, ReadReplicaFactory replicaFactory, ILogger<AppDBContextRouter> logger)
        {
            _writeContext = writeContext;
            _replicaFactory = replicaFactory;
            _logger = logger;
        }

        //Consulta com balanceamento e fallback
        public IQueryable<T> Query<T>() where T : class
        {
            try
            {
                var readContext = _replicaFactory.CreateNextReplica();
                return readContext.Set<T>();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Falha ao conectar à réplica. Usando banco principal como fallback.");
                return _writeContext.Set<T>().AsNoTracking();
            }
        }

        //Escrita sempre no principal
        public async Task AddAsync<T>(T entity) where T : class
        {
            _writeContext.Set<T>().Add(entity);
            await _writeContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            _writeContext.Set<T>().AddRange(entities);
            await _writeContext.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync() => await _writeContext.SaveChangesAsync();
    }

}
