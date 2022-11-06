using pictureAPI.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace pictureAPI.Data.Entities
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}
