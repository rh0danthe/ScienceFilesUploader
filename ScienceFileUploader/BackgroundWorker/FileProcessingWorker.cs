using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ScienceFileUploader.Dto;
using ScienceFileUploader.Entities;

namespace ScienceFileUploader.BackgroundWorker
{
    public class FileProcessingWorker : BackgroundService
    {
        private readonly FileStorageQueue _queue;

        public FileProcessingWorker(FileStorageQueue queue)
        {
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var file = _queue.GetFile();
            await ShardFileAsync(file);
        }

        private async Task ShardFileAsync(FileRequest file)
        {
            using (StreamReader reader = new StreamReader(new MemoryStream(file.Content)))
            {
                var piece = new FileShard();
                piece.Name = file.Name;
                int max = 10000;
                int splitLineAmount = 500;
                string? line;
                while ((line = await reader.ReadLineAsync()) != null && max > -1)
                {
                    max--;
                    splitLineAmount--;
                    piece.Content.Add(line);
                    if (splitLineAmount > -1)
                    { 
                        
                    }
                }
                
            }
        }
    }
    }

