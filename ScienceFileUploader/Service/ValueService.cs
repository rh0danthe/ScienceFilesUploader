using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScienceFileUploader.Dto;
using ScienceFileUploader.Entities;
using ScienceFileUploader.Exceptions.Value;
using ScienceFileUploader.Repository.Interface;
using ScienceFileUploader.Service.Interface;

namespace ScienceFileUploader.Service
{
    public class ValueService : IValueService
    {
        private readonly IValueRepository _valueRepository;
        private readonly IFileRepository _fileRepository;
    
        public ValueService(IValueRepository valueRepository, IFileRepository fileRepository) 
        { 
            _valueRepository = valueRepository;
            _fileRepository = fileRepository;
        }
        public async Task<ICollection<ValueResponse>> GetAllByFileNameAsync(string fileName)
        {
            if (!await _fileRepository.IfExistByNameAsync(fileName))
                throw new ValueNotFoundException("File with that name does not exist");
            var value = await _valueRepository.GetAllByFileNameAsync(fileName);
            return value.Select(MapToResponse).ToList();
        }

        private ValueResponse MapToResponse(Value value)
        {
            return new ValueResponse
            {
                Time = value.Time,
                TimeInMs = value.TimeInMs,
                Parameter = value.Parameter,
                FileName = value.FileName,
                Id = value.Id
            };
        }
    }
}