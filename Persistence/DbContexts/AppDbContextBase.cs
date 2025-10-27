using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.DbContexts
{
    public class AppDbContextBase<TContext>: DbContext where TContext: DbContext
    {
        public AppDbContextBase()
        {

        }
        public AppDbContextBase(DbContextOptions<TContext> options) : base(options){}
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Pedido> Pedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=NBUCLDSI-47;Database=BD_DevBantu;User ID=sa;Password=P3quen@123#;TrustServerCertificate=True");
        }
    }

    public class WriteDbContext : AppDbContextBase<WriteDbContext>
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options) { }
    }

    public class ReadDbContext : AppDbContextBase<ReadDbContext> 
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }

        public override int SaveChanges() =>
            throw new InvalidOperationException("Este contexto é somente leitura.");

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            throw new InvalidOperationException("Este contexto é somente leitura.");
    }
}
