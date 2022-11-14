using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using pictureAPI.Auth.Model;
using pictureAPI.Data.Dtos.Album;
using pictureAPI.Data.Dtos.Portfolio;
using pictureAPI.Data.Entities;
using pictureAPI.Data.Repository;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace pictureAPI.Controllers
{
    [ApiController]
    [Route("api/Portfolios/{portfolioId}/albums")]
    public class AlbumController : ControllerBase
    {
        private readonly IPortfoliosRepository portfoliosRepository;
        private readonly IAlbumsRepository albumsRepository;
        private readonly IAuthorizationService authorizationService;

        public AlbumController(IPortfoliosRepository portfoliosRepository, IAlbumsRepository albumsRepository, IAuthorizationService authorizationService)
        {
            this.portfoliosRepository = portfoliosRepository;
            this.albumsRepository = albumsRepository;
            this.authorizationService = authorizationService;
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
        [Authorize(Roles = UserRoles.AppUser)]
        public async Task<ActionResult<AlbumDto>> Create(Guid portfolioId, CreateAlbumDto createAlbumDto)
        {
            var portfolio = await portfoliosRepository.GetAsync(portfolioId);
            if (portfolio == null) return NotFound($"Couldn't find a portfolio with id of {portfolioId}");

            var album = new Album {
                Name = createAlbumDto.Name,
                Description = createAlbumDto.Description,
                CreationDate = DateTime.UtcNow,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };
            album.Portfolio = portfolio;

            await albumsRepository.CreateAsync(album);

            return Created("", new AlbumDto(album.Id, album.Name, album.Description, album.CreationDate));
        }

        [HttpPut]
        [Route("{albumId}")]
        [Authorize(Roles = UserRoles.AppUser)]
        public async Task<ActionResult<AlbumDto>> Update(Guid portfolioId, Guid albumId, UpdateAlbumDto updateAlbumDto)
        {
            var portfolio = await portfoliosRepository.GetAsync(portfolioId);
            if (portfolio == null) return NotFound($"Couldn't find a portfolio with id of {portfolioId}");

            var album = await albumsRepository.GetAsync(portfolioId, albumId);
            if (album == null)
                return NotFound();

            var authorizationResult = await authorizationService.AuthorizeAsync(User, album, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 403
                return Forbid();
            }

            album.Name = updateAlbumDto.Name;
            album.Description = updateAlbumDto.Description;
            await albumsRepository.UpdateAsync(album);

            return Ok(new AlbumDto(album.Id, album.Name, album.Description, album.CreationDate));
        }

        [HttpDelete]
        [Route("{albumId}")]
        [Authorize(Roles = UserRoles.AppUser)]
        public async Task<ActionResult> Remove(Guid portfolioId, Guid albumId)
        {
            var portfolio = await portfoliosRepository.GetAsync(portfolioId);
            if (portfolio == null) return NotFound($"Couldn't find a portfolio with id of {portfolioId}");

            var album = await albumsRepository.GetAsync(portfolioId, albumId);

            if (album == null)
                return NotFound();

            var authorizationResult = await authorizationService.AuthorizeAsync(User, album, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 403
                return Forbid();
            }

            await albumsRepository.DeleteAsync(album);

            return NoContent();
        }
    }
}
