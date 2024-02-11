using System.Collections.Generic;
using System.Threading.Tasks;
using ScienceFileUploader.Entities;

namespace ScienceFileUploader.Repository.Interface
{
    public interface IResultRepository
    {
        public Task<Result> CreateAsync(Result result);
        public Task<Result> GetByIdAsync(int resultId);
        public Task<ICollection<Result>> GetAllAsync();
        public Task<Result> GetByFileNameAsync(string name);
        public Task<ICollection<Result>> GetAllByParametersAsync(int minParameter, int maxParameter);
        public Task<ICollection<Result>> GetAllByTimeAsync(int minTime, int maxTime);
    }
}