using Microsoft.EntityFrameworkCore;
using pictureAPI.Data.Entities;

namespace pictureAPI.Data.Repository
{

    public interface IPicturesRepository
    {
        Task<Picture?> GetAsync(Guid albumId,Guid pictureId);
        Task<IReadOnlyList<Picture>> GetManyAsync(Guid albumId);
        Task CreateAsync(Picture picture);
        Task UpdateAsync(Picture picture);
        Task DeleteAsync(Picture picture);
    }

    public class PicturesRepository : IPicturesRepository
    {
        private readonly PictureAPIDbContext pictureAPIDbContext;

        public PicturesRepository(PictureAPIDbContext pictureAPIDbContext)
        {
            this.pictureAPIDbContext = pictureAPIDbContext;
        }

        public async Task<Picture?> GetAsync(Guid albumId, Guid pictureId)
        {
            return await pictureAPIDbContext.Pictures.FirstOrDefaultAsync(o => o.Album.Id == albumId && o.Id == pictureId);
        }

        public async Task<IReadOnlyList<Picture>> GetManyAsync(Guid albumId)
        {
            return await pictureAPIDbContext.Pictures.Where(o => o.Album.Id == albumId).ToListAsync();
        }

        public async Task CreateAsync(Picture picture)
        {
            pictureAPIDbContext.Pictures.Add(picture);
            await pictureAPIDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Picture picture)
        {
            pictureAPIDbContext.Pictures.Update(picture);
            await pictureAPIDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Picture picture)
        {
            pictureAPIDbContext.Pictures.Remove(picture);
            await pictureAPIDbContext.SaveChangesAsync();
        }

    }
}
