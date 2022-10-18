using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using pictureAPI.Data.Dtos.Album;
using pictureAPI.Data.Dtos.Portfolio;
using pictureAPI.Data.Entities;
using pictureAPI.Data.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace pictureAPI.Controllers
{
    [ApiController]
    [Route("api/Portfolios/{portfolioId}/albums")]
    public class AlbumController : ControllerBase
    {
        private readonly IPortfoliosRepository portfoliosRepository;
        private readonly IAlbumsRepository albumsRepository;

        public AlbumController(IPortfoliosRepository portfoliosRepository, IAlbumsRepository albumsRepository)
        {
            this.portfoliosRepository = portfoliosRepository;
            this.albumsRepository = albumsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<AlbumDto>> GetAllAsync(Guid portfolioId)
        {
            var albums = await albumsRepository.GetManyAsync(portfolioId);
            return albums.Select(x => new AlbumDto(x.Id, x.Name, x.Description, x.CreationDate));
        }

        [HttpGet]
        [Route("{albumId}")]
        public async Task<ActionResult<AlbumDto>> GetAsync(Guid portfolioId, Guid albumId)
        {
            var album = await albumsRepository.GetAsync(portfolioId, albumId);

            if (album == null)
                return NotFound();

            return new AlbumDto(album.Id, album.Name, album.Description, album.CreationDate);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<AlbumDto>> Create(Guid portfolioId, CreateAlbumDto createAlbumDto)
        {
            var portfolio = await portfoliosRepository.GetAsync(portfolioId);
            if (portfolio == null) return NotFound($"Couldn't find a portfolio with id of {portfolioId}");

            var album = new Album {
                Name = createAlbumDto.Name,
                Description = createAlbumDto.Description,
                CreationDate = DateTime.UtcNow,
            };
            album.Portfolio = portfolio;

            await albumsRepository.CreateAsync(album);

            return Created("", new AlbumDto(album.Id, album.Name, album.Description, album.CreationDate));
        }

        [HttpPut]
        [Route("update/{albumId}")]
        public async Task<ActionResult<AlbumDto>> Update(Guid portfolioId, Guid albumId, UpdateAlbumDto updateAlbumDto)
        {
            var portfolio = await portfoliosRepository.GetAsync(portfolioId);
            if (portfolio == null) return NotFound($"Couldn't find a portfolio with id of {portfolioId}");

            var album = await albumsRepository.GetAsync(portfolioId, albumId);
            if (album == null)
                return NotFound();

            album.Name = updateAlbumDto.Name;
            album.Description = updateAlbumDto.Description;
            await albumsRepository.UpdateAsync(album);

            return Ok(new AlbumDto(album.Id, album.Name, album.Description, album.CreationDate));
        }

        [HttpDelete]
        [Route("delete/{albumId}")]
        public async Task<ActionResult> Remove(Guid portfolioId, Guid albumId)
        {
            var album = await albumsRepository.GetAsync(portfolioId, albumId);

            if (album == null)
                return NotFound();

            await albumsRepository.DeleteAsync(album);

            return NoContent();
        }
    }
}
