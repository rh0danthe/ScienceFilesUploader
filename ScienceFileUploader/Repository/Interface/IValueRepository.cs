using System.Collections.Generic;
using System.Threading.Tasks;
using ScienceFileUploader.Entities;

namespace ScienceFileUploader.Repository.Interface
{
    public interface IValueRepository
    {
        public Task<Value> CreateAsync(Value value);
        public Task<Value> GetByIdAsync(int valueId);
        public Task<ICollection<Value>> GetAllAsync();
    }
}