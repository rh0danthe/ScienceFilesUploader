using System.Threading.Tasks;
using ScienceFileUploader.Entities;

namespace ScienceFileUploader.Repository.Interface
{
    public interface IFileRepository
    {
        public Task<File> CreateAsync(File file);
        public Task<File> GetByIdAsync(int fileId);
        public Task<bool> IfExistByNameAsync(string fileName);
        public Task<bool> DeleteAsync(string name);
    }
}