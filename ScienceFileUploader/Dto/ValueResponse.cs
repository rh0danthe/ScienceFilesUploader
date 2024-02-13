using System;

namespace ScienceFileUploader.Dto
{
    public class ValueResponse
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int TimeInMs { get; set; }
        public double Parameter { get; set; }
        public string FileName { get; set; }
    }
}