using System.ComponentModel.DataAnnotations;

namespace ScienceFileUploader.Dto
{
    public class ResultQueryParams
    {
        public string? FileName{ get; set; }
        
        [Range(0, int.MaxValue)]
        public int? MinValue{ get; set; }
        
        [Range(0, int.MaxValue)]
        public int? MaxValue{ get; set; }
        
        [Range(0, int.MaxValue)]
        public int? MinTime{ get; set; }
        
        [Range(0, int.MaxValue)]
        public int? MaxTime{ get; set; }
    }
}