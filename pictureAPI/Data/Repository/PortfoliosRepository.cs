using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using pictureAPI.Data.Entities;

namespace pictureAPI.Data.Repository
{
    public interface IPortfoliosRepository
    {
        Task<Portfolio?> GetAsync(Guid portId);
        Task<IEnumerable<Portfolio>> GetManyAsync();
        Task CreateAsync(Portfolio portfolio);
        Task UpdateAsync(Portfolio portfolio);
        Task DeleteAsync(Portfolio portfolio);
    }
    public class PortfoliosRepository : IPortfoliosRepository
    {
        private readonly PictureAPIDbContext pictureAPIDbContext;
        private readonly IAuthorizationService authorizationService;

        public PortfoliosRepository(PictureAPIDbContext pictureAPIDbContext, IAuthorizationService authorizationService)
        {
            this.pictureAPIDbContext = pictureAPIDbContext;
            this.authorizationService = authorizationService;
        }

        public async Task<Portfolio?> GetAsync(Guid portId)
        {
            return await pictureAPIDbContext.Portfolios.FirstOrDefaultAsync(o => o.Id == portId);
        }

        public async Task<IEnumerable<Portfolio>> GetManyAsync()
        {
            return await pictureAPIDbContext.Portfolios.ToListAsync();
        }

        public async Task CreateAsync(Portfolio portfolio)
        {
            pictureAPIDbContext.Portfolios.Add(portfolio);
            await pictureAPIDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Portfolio portfolio)
        {
            pictureAPIDbContext.Portfolios.Update(portfolio);
            await pictureAPIDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Portfolio portfolio)
        {
            pictureAPIDbContext.Portfolios.Remove(portfolio);
            await pictureAPIDbContext.SaveChangesAsync();
        }
    }
}
