namespace pictureAPI.Data.Entities
{
    public class Picture
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSold { get; set; }
        public int Price { get; set; }

        public Album Album { get; set; }
    }
}
