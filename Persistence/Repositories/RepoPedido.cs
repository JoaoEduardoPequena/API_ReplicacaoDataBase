using Domain.Entites;
using Domain.Interfaces;
using Persistence.DbContexts;

namespace Persistence.Repositories
{
    public class RepoPedido: IRepoPedido
    {
        private readonly IAppDBContextRouter _appDBContextRouter;
        
        public RepoPedido(IAppDBContextRouter appDBContextRouter)
        {
           _appDBContextRouter = appDBContextRouter;
        }
        public async Task<bool> CriarPedido(Pedido pedido)
        {
            try
            {
                await _appDBContextRouter.AddAsync(pedido);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
