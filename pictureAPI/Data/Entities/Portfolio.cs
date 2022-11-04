using pictureAPI.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace pictureAPI.Data.Entities
{
    public class Portfolio : IUserOwnedResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
