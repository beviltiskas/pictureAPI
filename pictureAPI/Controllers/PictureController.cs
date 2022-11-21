using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pictureAPI.Auth.Model;
using pictureAPI.Data.Dtos.Picture;
using pictureAPI.Data.Entities;
using pictureAPI.Data.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace pictureAPI.Controllers
{
    [ApiController]
    [Route("api/Portfolios/{portfolioId}/albums/{albumId}/pictures")]
    public class PictureController : ControllerBase
    {
        private readonly IPicturesRepository picturesRepository;
        private readonly IAlbumsRepository albumsRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IAuthorizationService authorizationService;
        private readonly IPortfoliosRepository portfoliosRepository;

        public PictureController(IPicturesRepository picturesRepository, IAlbumsRepository albumsRepository, IWebHostEnvironment hostEnvironment, IAuthorizationService authorizationService, IPortfoliosRepository portfoliosRepository)
        {
            this.picturesRepository = picturesRepository;
            this.albumsRepository = albumsRepository;
            _hostEnvironment = hostEnvironment;
            this.authorizationService = authorizationService;
            this.portfoliosRepository = portfoliosRepository;
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

            var pictureDto = new PictureDto(picture.Id, picture.Name, picture.Description, picture.CreationDate, picture.IsSold, picture.Price, picture.ImageName, picture.Image, picture.ImagePath = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, picture.ImageName));
            return Ok(new { Resource = pictureDto });
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.AppUser)]
        public async Task<ActionResult<PictureDto>> Create(Guid portfolioId, Guid albumId, [FromForm]CreatePictureDto createPictureDto)
        {
            var album = await albumsRepository.GetAsync(portfolioId, albumId);
            if (album == null) return NotFound($"Couldn't find a album with id of {albumId}");

            var authorizationResult = await authorizationService.AuthorizeAsync(User, album, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 403
                return Forbid();
            }

            var picture = new Picture
            {
                Name = createPictureDto.Name,
                Description = createPictureDto.Description,
                CreationDate = DateTime.UtcNow,
                Price = createPictureDto.Price,
                Image = createPictureDto.Image,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };
            if (picture.Price == 0)
            {
                picture.IsSold = false;
            }
            else if(picture.Price > 0)
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
        [Authorize(Roles = UserRoles.AppUser)]
        public async Task<ActionResult<PictureDto>> Update(Guid portfolioId, Guid albumId, Guid pictureId, [FromForm]UpdatePictureDto updatePictureDto)
        {
            var album = await albumsRepository.GetAsync(portfolioId, albumId);
            if (album == null) return NotFound($"Couldn't find a album with id of {albumId}");

            var picture = await picturesRepository.GetAsync(albumId, pictureId);

            if (picture == null)
                return NotFound();

            var authorizationResult = await authorizationService.AuthorizeAsync(User, picture, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 403
                return Forbid();
            }

            picture.Name = updatePictureDto.Name;
            picture.Description = updatePictureDto.Description;
            picture.Price = updatePictureDto.Price;
            if (picture.Price == 0)
            {
                picture.IsSold = false;
            }
            else if (picture.Price > 0)
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
        [Authorize(Roles = UserRoles.AppUser)]
        public async Task<ActionResult> Remove(Guid albumId, Guid pictureId, Guid portfolioId)
        {
            var album = await albumsRepository.GetAsync(portfolioId, albumId);
            if (album == null) return NotFound($"Couldn't find a album with id of {albumId}");

            var picture = await picturesRepository.GetAsync(albumId, pictureId);

            if (picture == null)
                return NotFound();

            var authorizationResult = await authorizationService.AuthorizeAsync(User, picture, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 403
                return Forbid();
            }

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

