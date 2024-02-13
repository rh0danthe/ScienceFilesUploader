using System.ComponentModel.DataAnnotations;

namespace ScienceFileUploader.Dto
{
    public class FileRequest
    {
        [MaxLength(256)]
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}