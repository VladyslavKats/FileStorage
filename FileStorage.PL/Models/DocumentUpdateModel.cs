namespace FileStorage.PL.Models
{
    public class DocumentUpdateModel
    {
        public string Id { get; set; }
       
        public string Name { get; set; }
        
        public long Size { get; set; }
        
        public string ContentType { get; set; }
        
        public string UserId { get; set; }
    }
}
