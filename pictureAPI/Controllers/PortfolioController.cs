using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pictureAPI.Auth.Model;
using pictureAPI.Data.Dtos.Portfolio;
using pictureAPI.Data.Entities;
using pictureAPI.Data.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace pictureAPI.Controllers
{
    [ApiController]
    [Route("api/portfolios/")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfoliosRepository portfoliosRepository;
        private readonly IAuthorizationService authorizationService;

        public PortfolioController(IPortfoliosRepository portfoliosRepository, IAuthorizationService authorizationService)
        {
            this.portfoliosRepository = portfoliosRepository;
            this.authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<PortfolioDto>> GetAllAsync()
        {
            var portfolios = await portfoliosRepository.GetManyAsync();
            return portfolios.Select(x => new PortfolioDto(x.Id, x.Name, x.Description, x.CreationDate));
        }

        [HttpGet]
        [Route("{portfolioId}")]
        public async Task<IActionResult> Get(Guid portfolioId)
        {
            var portfolio = await portfoliosRepository.GetAsync(portfolioId);

            if (portfolio == null)
                return NotFound();

            var portfolioDto = new PortfolioDto(portfolio.Id, portfolio.Name, portfolio.Description, portfolio.CreationDate);
            return Ok(new { Resource = portfolioDto });
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.AppUser)]
        public async Task<ActionResult<PortfolioDto>> Create(CreatePortfolioDto createPortfolioDto)
        {
            var portfolio = new Portfolio
            {
                Name = createPortfolioDto.Name,
                Description = createPortfolioDto.Description,
                CreationDate = DateTime.UtcNow,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };
            await portfoliosRepository.CreateAsync(portfolio);

            return Created("", new PortfolioDto(portfolio.Id, portfolio.Name, portfolio.Description, portfolio.CreationDate));
        }

        [HttpPut]
        [Route("{portfolioId}")]
        [Authorize(Roles = UserRoles.AppUser)]
        public async Task<ActionResult<PortfolioDto>> Update(Guid portfolioId, UpdatePortfolioDto updatePortfolioDto)
        {
            var portfolio = await portfoliosRepository.GetAsync(portfolioId);

            if (portfolio == null)
                return NotFound();

            var authorizationResult = await authorizationService.AuthorizeAsync(User, portfolio, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 404
                return Forbid();
            }

            portfolio.Name = updatePortfolioDto.Name;
            portfolio.Description = updatePortfolioDto.Description;
            await portfoliosRepository.UpdateAsync(portfolio);

            return Ok(new PortfolioDto(portfolio.Id, portfolio.Name, portfolio.Description, portfolio.CreationDate));
        }

        [HttpDelete]
        [Route("{portfolioId}")]
        [Authorize(Roles = UserRoles.AppUser)]
        public async Task<ActionResult> Remove(Guid portfolioId)
        {
            var portfolio = await portfoliosRepository.GetAsync(portfolioId);

            if (portfolio == null)
                return NotFound();

            var authorizationResult = await authorizationService.AuthorizeAsync(User, portfolio, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 404
                return Forbid();
            }

            await portfoliosRepository.DeleteAsync(portfolio);

            return NoContent();
        }
    }
}
