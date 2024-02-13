using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScienceFileUploader.Dto;
using ScienceFileUploader.Entities;
using ScienceFileUploader.Repository.Interface;
using ScienceFileUploader.Service.Interface;

namespace ScienceFileUploader.Service
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
    
        public ResultService(IResultRepository resultRepository) 
        { 
            _resultRepository = resultRepository;
        }
        
        public async Task<ICollection<ResultResponse>> GetAllAsync()
        {
            var results = await _resultRepository.GetAllAsync();
            return results.Select(MapToResponse).ToList();
        }

        public async Task<ResultResponse> GetByFileNameAsync(string name)
        {
            return MapToResponse(await _resultRepository.GetByFileNameAsync(name));
        }

        public async Task<ICollection<ResultResponse>> GetAllByParametersAsync(int minParameter, int maxParameter)
        {
            var results = await _resultRepository.GetAllByParametersAsync(minParameter, maxParameter);
            return results.Select(MapToResponse).ToList();
        }

        public async Task<ICollection<ResultResponse>> GetAllByTimeAsync(int minTime, int maxTime)
        {
            var results = await _resultRepository.GetAllByTimeAsync(minTime, maxTime);
            return results.Select(MapToResponse).ToList();
        }

        private ResultResponse MapToResponse(Result dbResult)
        {
            return new ResultResponse
            {
                AvgByParameters = dbResult.AvgByParameters,
                FileName = dbResult.FileName,
                AvgExperimentDuration = dbResult.AvgExperimentDuration,
                Id = dbResult.Id,
                FirstExperimentTime = dbResult.FirstExperimentTime,
                AmountOfExperiments = dbResult.AmountOfExperiments,
                LastExperimentTime = dbResult.LastExperimentTime,
                MaxExperimentDuration = dbResult.MaxExperimentDuration,
                MinExperimentDuration = dbResult.MinExperimentDuration,
                MedianByParameters = dbResult.MedianByParameters,
                MaxParameterValue = dbResult.MaxParameterValue,
                MinParameterValue = dbResult.MinParameterValue
                
            };
        }
    }
}