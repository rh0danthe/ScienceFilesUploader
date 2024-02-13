using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ScienceFileUploader.Dto;
using ScienceFileUploader.Entities;
using ScienceFileUploader.Repository.Interface;
using ScienceFileUploader.Service.Interface;

namespace ScienceFileUploader.Service
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IValueRepository _valueRepository;
        private readonly IResultRepository _resultRepository;
    
        public FileService(IFileRepository fileRepository, IValueRepository valueRepository, IResultRepository resultRepository) 
        { 
            _fileRepository = fileRepository;
            _valueRepository = valueRepository;
            _resultRepository = resultRepository;
        }
        public async Task<FileResponse> AddFileAsync(FileRequest file)
        {
            if (await _fileRepository.IfExistByNameAsync(file.Name))
                await _fileRepository.DeleteAsync(file.Name);
            var dbFile = new Entities.File
            {
                Name = file.Name
            };
            int fileId =  (await _fileRepository.CreateAsync(dbFile)).Id;
            if (!await ReadInfoToValue(file, fileId))
                await _fileRepository.DeleteAsync(file.Name);
            var values = await  _valueRepository.GetAllByFileNameAsync(file.Name);
            dbFile.Values = values;
            await WriteInfoToResult(values, fileId);
            return MapToResponse(await _fileRepository.GetByIdAsync(fileId));
        }

        private async Task WriteInfoToResult(ICollection<Value> values, int fileId)
        {
            var dbFile = await _fileRepository.GetByIdAsync(fileId);
            var result = new Result
            {
                FirstExperimentTime = values.Min(v => v.Time),
                LastExperimentTime = values.Max(v => v.Time),
                MaxExperimentDuration = values.Max(v => v.TimeInMs),
                MinExperimentDuration = values.Min(v => v.TimeInMs),
                AvgExperimentDuration = values.Average(v => v.TimeInMs),
                AvgByParameters = values.Average(v => v.Parameter),
                MaxParameterValue = values.Max(v => v.Parameter),
                MinParameterValue = values.Min(v => v.Parameter),
                AmountOfExperiments = values.Count,
                FileName = dbFile.Name,
                File = dbFile
            };

            if (values.Count % 2 == 0)
                result.MedianByParameters = (values.ToList().ElementAt(values.Count % 2).Parameter
                                             + values.ToList().ElementAt(values.Count % 2).Parameter) / 2;
            result.MedianByParameters = values.ToList().ElementAt(values.Count / 2).Parameter;
            dbFile.Result = result;
            await _resultRepository.CreateAsync(result);
        }

        private async Task<bool> ReadInfoToValue(FileRequest file, int fileId)
        {
            var dbFile = await _fileRepository.GetByIdAsync(fileId);
            using (StreamReader reader = new StreamReader(new MemoryStream(file.Content)))
            {
                bool flag = false;
                int max = 10000;
                DateTime border = new DateTime(2000, 1, 1);
                string? line;
                while ((line = await reader.ReadLineAsync()) != null && max > -1)
                {
                    max--;
                    string[] segments = line.Split(';');
                    if (segments.Length > 3)
                        continue;
                    if (!DateTime.TryParseExact(segments[0], "yyyy-MM-dd_HH-mm-ss", null, DateTimeStyles.None,
                            out var time) || time > DateTime.Now || time < border)
                        continue;
                    if (int.Parse(segments[1]) < 0 || double.Parse(segments[2]) < 0)
                        continue;
                    var value = new Value
                    {
                        Time = time,
                        TimeInMs = int.Parse(segments[1]),
                        Parameter = Convert.ToDouble(segments[2]),
                        FileName = file.Name,
                        File = dbFile
                        
                    };
                    flag = true;
                    await _valueRepository.CreateAsync(value);
                }

                return flag;
            }
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