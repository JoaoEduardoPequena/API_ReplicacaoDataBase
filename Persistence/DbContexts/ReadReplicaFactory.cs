using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContexts
{
    public class ReadReplicaFactory
    {
        private readonly string[] _replicaConnections;
        private int _lastIndex = -1;

        public ReadReplicaFactory(string[] replicaConnections)
        {
            _replicaConnections = replicaConnections;
        }

        public ReadDbContext CreateNextReplica()
        {
            if (_replicaConnections.Length == 0)
                throw new InvalidOperationException("Nenhuma réplica configurada.");

            // Round-robin simples
            _lastIndex = (_lastIndex + 1) % _replicaConnections.Length;
            var connectionString = _replicaConnections[_lastIndex];

            var options = new DbContextOptionsBuilder<ReadDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new ReadDbContext(options);
        }
    }

}
