using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScienceFileUploader.Entities
{
    public class Value
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int TimeInMs { get; set; }
        public double Parameter { get; set; }
        public string FileName { get; set; }
        public File File { get; set; }
    }
}