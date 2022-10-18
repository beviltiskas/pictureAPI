namespace pictureAPI.Data.Dtos.Picture;
public record PictureDto(Guid Id, string Name, string Description, DateTime CreationDate, bool IsSold, int Price);
public record CreatePictureDto(string Name, string Description, DateTime CreationDate, int Price);
public record UpdatePictureDto(string Name, string Description, bool IsSold, int Price);