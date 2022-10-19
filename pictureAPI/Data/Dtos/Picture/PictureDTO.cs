namespace pictureAPI.Data.Dtos.Picture;
public record PictureDto(Guid Id, string Name, string Description, DateTime CreationDate, bool IsSold, int Price, string ImageName, IFormFile Image, string ImagePath);
public record CreatePictureDto(string Name, string Description, DateTime CreationDate, int Price, IFormFile Image);
public record UpdatePictureDto(string Name, string Description, bool IsSold, int Price, IFormFile Image);