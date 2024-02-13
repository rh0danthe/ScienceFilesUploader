using System.Threading.Tasks;
using ScienceFileUploader.Dto;

namespace ScienceFileUploader.Service.Interface
{
    public interface IFileService
    {
        public Task<FileResponse> AddFileAsync(FileRequest file);
    }
}