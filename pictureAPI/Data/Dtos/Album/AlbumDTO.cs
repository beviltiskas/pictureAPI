namespace pictureAPI.Data.Dtos.Album;
public record AlbumDto(Guid Id, string Name, string Description, DateTime CreationDate);
public record CreateAlbumDto(string Name, string Description, DateTime CreationDate);
public record UpdateAlbumDto(string Name, string Description);
