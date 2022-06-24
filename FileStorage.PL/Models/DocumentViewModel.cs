namespace FileStorage.PL.Models
{
    /// <summary>
    /// Model for view document
    /// </summary>
    public class DocumentViewModel
    {
        /// <summary>
        /// Document id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Document`s name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Document size in bytes
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// Document type
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// Path to document
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Owner id
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// Owner
        /// </summary>
        public UserViewModel User { get; set; }
    }
}
