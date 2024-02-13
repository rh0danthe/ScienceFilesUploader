using System;

namespace ScienceFileUploader.Dto
{
    public class ResultResponse
    {
        public int Id { get; set; }
        public DateTime FirstExperimentTime { get; set; }
        public DateTime LastExperimentTime { get; set; }
        public int MaxExperimentDuration { get; set; }
        public int MinExperimentDuration { get; set; }
        public double AvgExperimentDuration { get; set; }
        public double AvgByParameters { get; set; }
        public double MedianByParameters { get; set; }
        public double MaxParameterValue { get; set; }
        public double MinParameterValue { get; set; }
        public int AmountOfExperiments { get; set; }
        public string FileName { get; set; }
    }
}