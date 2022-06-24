namespace FileStorage.PL.Models
{
    /// <summary>
    /// Model for downloading document
    /// </summary>
    public class DocumentDownloadModel
    {
        /// <summary>
        /// Path to document
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Document`s name
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Document type
        /// </summary>
        public string ContentType { get; set; }
    }
}
