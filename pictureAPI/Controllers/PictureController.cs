using Microsoft.AspNetCore.Mvc;
using pictureAPI.Data.Dtos.Picture;
using pictureAPI.Data.Entities;
using pictureAPI.Data.Repository;
using System.Text.Json;
using AutoMapper;
using pictureAPI.Data.Dtos.Album;

namespace pictureAPI.Controllers
{
    [ApiController]
    [Route("api/Portfolios/{portfolioId}/albums/{albumId}/pictures")]
    public class PictureController : ControllerBase
    {
        private readonly IPicturesRepository picturesRepository;
        private readonly IAlbumsRepository albumsRepository;

        public PictureController(IPicturesRepository picturesRepository, IAlbumsRepository albumsRepository)
        {
            this.picturesRepository = picturesRepository;
            this.albumsRepository = albumsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PictureDto>> GetMany(Guid albumId)
        {
            var pictures = await picturesRepository.GetManyAsync(albumId);
            return pictures.Select(x => new PictureDto(x.Id, x.Name, x.Description, x.CreationDate, x.IsSold, x.Price));
        }

        [HttpGet]
        [Route("{pictureId}")]
        public async Task<IActionResult> Get(Guid albumId, Guid pictureId)
        {
            var picture = await picturesRepository.GetAsync(albumId, pictureId);

            if (picture == null)
                return NotFound();

            var pictureDto = new PictureDto(picture.Id, picture.Name, picture.Description, picture.CreationDate, picture.IsSold, picture.Price);
            return Ok(new { Resource = pictureDto });
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<PictureDto>> Create(Guid portfolioId, Guid albumId, CreatePictureDto createPictureDto)
        {
            var album = await albumsRepository.GetAsync(portfolioId, albumId);
            if (album == null) return NotFound($"Couldn't find a album with id of {albumId}");

            var picture = new Picture
            {
                Name = createPictureDto.Name,
                Description = createPictureDto.Description,
                CreationDate = DateTime.UtcNow,
                Price = createPictureDto.Price,
            };
            if (picture.Price == 0)
            {
                picture.IsSold = false;
            }
            else
            {
                picture.IsSold = true;

            }
            picture.Album = album;
            await picturesRepository.CreateAsync(picture);

            return Created("", new PictureDto(picture.Id, picture.Name, picture.Description, picture.CreationDate, picture.IsSold, picture.Price));
        }

        [HttpPut]
        [Route("update/{pictureId}")]
        public async Task<ActionResult<PictureDto>> Update(Guid portfolioId, Guid albumId, Guid pictureId, UpdatePictureDto updatePictureDto)
        {
            var album = await albumsRepository.GetAsync(portfolioId, albumId);
            if (album == null) return NotFound($"Couldn't find a album with id of {albumId}");

            var picture = await picturesRepository.GetAsync(albumId, pictureId);

            if (picture == null)
                return NotFound();

            picture.Name = updatePictureDto.Name;
            picture.Description = updatePictureDto.Description;
            picture.Price = updatePictureDto.Price;
            if (picture.Price == 0)
            {
                picture.IsSold = false;
            }
            else
            {
                picture.IsSold = true;

            }
            await picturesRepository.UpdateAsync(picture);

            return Ok(new PictureDto(picture.Id, picture.Name, picture.Description, picture.CreationDate, picture.IsSold, picture.Price));
        }

        [HttpDelete]
        [Route("delete/{pictureId}")]
        public async Task<ActionResult> Remove(Guid albumId, Guid pictureId)
        {
            var picture = await picturesRepository.GetAsync(albumId, pictureId);

            if (picture == null)
                return NotFound();

            await picturesRepository.DeleteAsync(picture);

            return NoContent();
        }
    }
}
