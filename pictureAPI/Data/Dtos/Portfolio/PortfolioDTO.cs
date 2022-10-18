namespace pictureAPI.Data.Dtos.Portfolio;
public record PortfolioDto(Guid Id, string Name, string Description, DateTime CreationDate);
public record CreatePortfolioDto(string Name, string Description, DateTime CreationDate);
public record UpdatePortfolioDto(string Name, string Description);