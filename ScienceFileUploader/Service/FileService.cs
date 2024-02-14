using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ScienceFileUploader.BackgroundWorker;
using ScienceFileUploader.Dto;
using ScienceFileUploader.Entities;
using ScienceFileUploader.Repository.Interface;
using ScienceFileUploader.Service.Interface;

namespace ScienceFileUploader.Service
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly FileStorageQueue _queue;
        private readonly IValueRepository _valueRepository;
        private readonly IResultRepository _resultRepository;
    
        public FileService(IFileRepository fileRepository, IValueRepository valueRepository, IResultRepository resultRepository, FileStorageQueue queue) 
        { 
            _fileRepository = fileRepository;
            _valueRepository = valueRepository;
            _resultRepository = resultRepository;
            _queue = queue;
        }
        public async Task<FileResponse> AddFileAsync(FileRequest file)
        {
            if (await _fileRepository.IfExistByNameAsync(file.Name))
                await _fileRepository.DeleteAsync(file.Name);
            _queue.AddFile(file);
            var dbFile = new Entities.File
            {
                Name = file.Name
            };
            return MapToResponse(await _fileRepository.CreateAsync(dbFile));
        }

        private FileResponse MapToResponse(Entities.File dbFile)
        {
            return new FileResponse
            {
                Id = dbFile.Id,
                Name = dbFile.Name
            };
        }
    }
}