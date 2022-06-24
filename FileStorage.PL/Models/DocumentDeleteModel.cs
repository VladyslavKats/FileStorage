namespace FileStorage.PL.Models
{
    /// <summary>
    /// Model for deleting document
    /// </summary>
    public class DocumentDeleteModel
    {
        /// <summary>
        /// Document`s id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Owner name
        /// </summary>
        public string UserName { get; set; }
    }
}
