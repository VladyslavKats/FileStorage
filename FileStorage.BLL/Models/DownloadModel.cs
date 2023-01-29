namespace FileStorage.BLL.Models
{
    public class DownloadModel
    {
        public byte[] Document { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }
    }
}
