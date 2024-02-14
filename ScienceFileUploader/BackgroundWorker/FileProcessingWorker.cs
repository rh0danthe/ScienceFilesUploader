using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScienceFileUploader.Dto;
using ScienceFileUploader.Entities;
using ScienceFileUploader.Repository.Interface;

namespace ScienceFileUploader.BackgroundWorker
{
    public class FileProcessingWorker : BackgroundService
    {
        private readonly FileStorageQueue _queue;
        private readonly IServiceProvider _serviceProvider;

        public FileProcessingWorker(FileStorageQueue queue, IServiceProvider serviceProvider)
        {
            _queue = queue;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var fileRepository = scope.ServiceProvider.GetRequiredService<IFileRepository>();
            var valueRepository = scope.ServiceProvider.GetRequiredService<IValueRepository>();
            var file = await _queue.GetFileAsync(stoppingToken);
            await ShardFileAsync(file);
            var values = await valueRepository.GetAllByFileNameAsync(file.Name);
            if (values.Count == 0)
            {
                await fileRepository.DeleteAsync(file.Name);
            }
            await WriteInfoToResultAsync(values, file.Name);
        }

        private async Task ShardFileAsync(FileRequest file)
        {
            using (StreamReader reader = new StreamReader(new MemoryStream(file.Content)))
            {
                var piece = new FileShard();
                piece.Name = file.Name;
                int max = 10000;
                int splitLineAmount = 500;
                var tasks = new List<Task>();
                string? line;
                while ((line = await reader.ReadLineAsync()) != null && max > -1)
                {
                    max--;
                    splitLineAmount--;
                    piece.Content.Add(line);
                    if (splitLineAmount <= 0)
                    {
                        var task = ProcessPieceAsync(piece, file.Name);
                        tasks.Add(task);
                        splitLineAmount = 500;
                        piece.Content = new List<string>();
                    }
                }

                if (piece.Content.Count > 0)
                {
                    var lastTask = ProcessPieceAsync(piece, file.Name);
                    tasks.Add(lastTask);
                }

                await Task.WhenAll(tasks);
            }
        }

        private async Task ProcessPieceAsync(FileShard piece, string name)
        {
            using var scope = _serviceProvider.CreateScope();
            var valueRepository = scope.ServiceProvider.GetRequiredService<IValueRepository>();
            var content = piece.Content;
            for (int i = 0; i < content.Count; i++)
            {
                var border = new DateTime(2000, 1, 1);
                
                string[] segments = content.ElementAt(i).Split(';');
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
                    FileName = name
                };
                await valueRepository.CreateAsync(value);
            }
        }
        
        private async Task WriteInfoToResultAsync(ICollection<Value> values, string name)
        {
           using var scope = _serviceProvider.CreateScope();
           var fileRepository = scope.ServiceProvider.GetRequiredService<IFileRepository>();
           var resultRepository = scope.ServiceProvider.GetRequiredService<IResultRepository>();
           
            var dbFile = await fileRepository.GetByNameAsync(name);
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
                FileName = dbFile.Name
            };

            if (values.Count % 2 == 0)
                result.MedianByParameters = (values.ToList().ElementAt(values.Count % 2).Parameter
                                             + values.ToList().ElementAt(values.Count % 2).Parameter) / 2;
            result.MedianByParameters = values.ToList().ElementAt(values.Count / 2).Parameter;
            dbFile.Result = result;
            await resultRepository.CreateAsync(result);
        }
    }
    }

