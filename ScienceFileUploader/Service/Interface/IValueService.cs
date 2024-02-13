using System.Collections.Generic;
using System.Threading.Tasks;
using ScienceFileUploader.Dto;

namespace ScienceFileUploader.Service.Interface
{
    public interface IValueService
    {
        public Task<ICollection<ValueResponse>> GetAllByFileNameAsync(string fileName);
    }
}