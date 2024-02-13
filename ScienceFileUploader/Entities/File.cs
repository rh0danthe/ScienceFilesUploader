using System.Collections.Generic;

namespace ScienceFileUploader.Entities
{
    public class File
         {
             public int Id { get; set; }
             public string Name { get; set; }
             public Result Result { get; set; }
             public ICollection<Value> Values { get; set; }
         }
}