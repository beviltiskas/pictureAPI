using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pictureAPI.Data.Dtos.Portfolio;
using pictureAPI.Data.Entities;
using pictureAPI.Data.Repository;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace pictureAPI.Controllers
{
    [ApiController]
    [Route("api/portfolios/")]
    public class PortfolioController:ControllerBase
    {
        private readonly IPortfoliosRepository portfoliosRepository;

        public PortfolioController(IPortfoliosRepository portfoliosRepository)
        {
            this.portfoliosRepository = portfoliosRepository;
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
            return Ok(new { Resource = portfolioDto});
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<PortfolioDto>> Create(CreatePortfolioDto createPortfolioDto)
        {
            var portfolio = new Portfolio
            { 
                Name = createPortfolioDto.Name, 
                Description = createPortfolioDto.Description, 
                CreationDate = DateTime.UtcNow 
            };

            await portfoliosRepository.CreateAsync(portfolio);

            return Created("", new PortfolioDto(portfolio.Id, portfolio.Name, portfolio.Description, portfolio.CreationDate));
        }

        [HttpPut]
        [Route("update/{portfolioId}")]
        public async Task<ActionResult<PortfolioDto>> Update(Guid portfolioId, UpdatePortfolioDto updatePortfolioDto)
        {
            var portfolio = await portfoliosRepository.GetAsync(portfolioId);

            if (portfolio == null)
                return NotFound();

            portfolio.Name = updatePortfolioDto.Name;
            portfolio.Description = updatePortfolioDto.Description;
            await portfoliosRepository.UpdateAsync(portfolio);

            return Ok(new PortfolioDto(portfolio.Id, portfolio.Name, portfolio.Description, portfolio.CreationDate));
        }

        [HttpDelete]
        [Route("delete/{portfolioId}")]
        public async Task<ActionResult> Remove(Guid portfolioId)
        {
            var portfolio = await portfoliosRepository.GetAsync(portfolioId);

            if (portfolio == null)
                return NotFound();

            await portfoliosRepository.DeleteAsync(portfolio);

            return NoContent();
        }
    }
}
