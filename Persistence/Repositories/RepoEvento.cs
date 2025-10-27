using Domain.Entites;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories
{
    public class RepoEvento : IRepoEvento
    {
        private readonly IAppDBContextRouter _appDBContextRouter;
        public RepoEvento(IAppDBContextRouter appDBContextRouter )
        {
            _appDBContextRouter=appDBContextRouter;
        }
   
        public async Task<bool> CriarEvento(Evento dto)
        {   
            try
            {
                await _appDBContextRouter.AddAsync(dto);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Evento>> GetAllEventos()
        {
           return await _appDBContextRouter.Query<Evento>().ToListAsync();
        }
    }
}
