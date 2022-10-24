using Microsoft.AspNetCore.Mvc;
using pictureAPI.Data.Dtos.Picture;
using pictureAPI.Data.Entities;
using pictureAPI.Data.Repository;

namespace pictureAPI.Controllers
{
    [ApiController]
    [Route("api/Portfolios/{portfolioId}/albums/{albumId}/pictures")]
    public class PictureController : ControllerBase
    {
        private readonly IPicturesRepository picturesRepository;
        private readonly IAlbumsRepository albumsRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PictureController(IPicturesRepository picturesRepository, IAlbumsRepository albumsRepository, IWebHostEnvironment hostEnvironment)
        {
            this.picturesRepository = picturesRepository;
            this.albumsRepository = albumsRepository;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IEnumerable<PictureDto>> GetMany(Guid albumId)
        {
            var pictures = await picturesRepository.GetManyAsync(albumId);
            return pictures.Select(x => new PictureDto(x.Id, x.Name, x.Description, x.CreationDate, x.IsSold, x.Price, x.ImageName, x.Image, x.ImagePath= String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)));
        }

        [HttpGet]
        [Route("{pictureId}")]
        public async Task<IActionResult> Get(Guid albumId, Guid pictureId)
        {
            var picture = await picturesRepository.GetAsync(albumId, pictureId);

            if (picture == null)
                return NotFound();

            var pictureDto = new PictureDto(picture.Id, picture.Name, picture.Description, picture.CreationDate, picture.IsSold, picture.Price, picture.ImageName, picture.Image, picture.ImagePath);
            return Ok(new { Resource = pictureDto });
        }

        [HttpPost]
        public async Task<ActionResult<PictureDto>> Create(Guid portfolioId, Guid albumId, [FromForm]CreatePictureDto createPictureDto)
        {
            var album = await albumsRepository.GetAsync(portfolioId, albumId);
            if (album == null) return NotFound($"Couldn't find a album with id of {albumId}");

            var picture = new Picture
            {
                Name = createPictureDto.Name,
                Description = createPictureDto.Description,
                CreationDate = DateTime.UtcNow,
                Price = createPictureDto.Price,
                Image = createPictureDto.Image,
            };
            if (picture.Price == 0)
            {
                picture.IsSold = false;
            }
            else
            {
                picture.IsSold = true;

            }

            picture.ImageName = await SaveImage(picture.Image);

            picture.Album = album;
            await picturesRepository.CreateAsync(picture);

            return Created("", new PictureDto(picture.Id, picture.Name, picture.Description, picture.CreationDate, picture.IsSold, picture.Price, picture.ImageName, picture.Image, picture.ImagePath));
        }

        [HttpPut]
        [Route("{pictureId}")]
        public async Task<ActionResult<PictureDto>> Update(Guid portfolioId, Guid albumId, Guid pictureId, [FromForm]UpdatePictureDto updatePictureDto)
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

            if (updatePictureDto.Image != null)
            {
                DeleteImage(picture.ImageName);
                picture.ImageName = await SaveImage(updatePictureDto.Image);
            }

            await picturesRepository.UpdateAsync(picture);

            return Ok(new PictureDto(picture.Id, picture.Name, picture.Description, picture.CreationDate, picture.IsSold, picture.Price, picture.ImageName, picture.Image, picture.ImagePath));
        }

        [HttpDelete]
        [Route("{pictureId}")]
        public async Task<ActionResult> Remove(Guid albumId, Guid pictureId)
        {
            var picture = await picturesRepository.GetAsync(albumId, pictureId);

            if (picture == null)
                return NotFound();

            DeleteImage(picture.ImageName);

            await picturesRepository.DeleteAsync(picture);

            return NoContent();
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }

    }
}

