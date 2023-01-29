namespace FileStorage.DAL.Models
{
    public class Document
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }
        
        public string ContentType { get; set; }
        
        public string UserId { get; set; }

        public User  User { get; set; }
    }
}
