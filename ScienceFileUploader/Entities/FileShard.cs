using System.Collections.Generic;

namespace ScienceFileUploader.Entities
{
    public class FileShard
    {
        public string Name { get; set; }
        public List<string> Content { get; set; }
    }
}