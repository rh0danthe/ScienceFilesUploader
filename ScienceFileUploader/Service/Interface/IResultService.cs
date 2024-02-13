using System.Collections.Generic;
using System.Threading.Tasks;
using ScienceFileUploader.Dto;

namespace ScienceFileUploader.Service.Interface
{
    public interface IResultService
    {
        public Task<ICollection<ResultResponse>> GetAllAsync();
        public Task<ResultResponse> GetByFileNameAsync(string name);
        public Task<ICollection<ResultResponse>> GetAllByParametersAsync(int minParameter, int maxParameter);
        public Task<ICollection<ResultResponse>> GetAllByTimeAsync(int minTime, int maxTime);
    }
}