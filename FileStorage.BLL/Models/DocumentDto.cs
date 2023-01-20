using FileStorage.DAL.Models;

namespace FileStorage.BLL.Models
{
    public class DocumentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
      
        public long Size { get; set; }
   
        public string ContentType { get; set; }
    
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
