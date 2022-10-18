using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using pictureAPI.Data.Entities;

namespace pictureAPI.Data.Repository
{
    public interface IAlbumsRepository
    {
        Task<Album?> GetAsync(Guid portId, Guid albumId);
        Task<IReadOnlyList<Album>> GetManyAsync(Guid portId);
        Task CreateAsync(Album album);
        Task UpdateAsync(Album album);
        Task DeleteAsync(Album album);
    }
    public class AlbumsRepository : IAlbumsRepository
    {
        private readonly PictureAPIDbContext pictureAPIDbContext;

        public AlbumsRepository(PictureAPIDbContext pictureAPIDbContext)
        {
            this.pictureAPIDbContext = pictureAPIDbContext;
        }

        public async Task<Album?> GetAsync(Guid portId, Guid albumId)
        {
            return await pictureAPIDbContext.Albums.FirstOrDefaultAsync(o => o.Portfolio.Id == portId && o.Id == albumId);
        }

        public async Task<IReadOnlyList<Album>> GetManyAsync(Guid portId)
        {
            return await pictureAPIDbContext.Albums.Where(o => o.Portfolio.Id == portId).ToListAsync();
        }

        public async Task CreateAsync(Album album)
        {
            pictureAPIDbContext.Albums.Add(album);
            await pictureAPIDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Album album)
        {
            pictureAPIDbContext.Albums.Update(album);
            await pictureAPIDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Album album)
        {
            pictureAPIDbContext.Albums.Remove(album);
            await pictureAPIDbContext.SaveChangesAsync();
        }
    }
}
