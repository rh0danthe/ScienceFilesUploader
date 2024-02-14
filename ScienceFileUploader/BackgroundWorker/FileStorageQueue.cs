using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using ScienceFileUploader.Dto;

namespace ScienceFileUploader.BackgroundWorker
{
    public class FileStorageQueue
    {
        private readonly ConcurrentQueue<FileRequest> _queue = new();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void AddFile(FileRequest file)
        {
            _queue.Enqueue(file);
            _signal.Release();
        }
        public async Task<FileRequest> GetFileAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _queue.TryDequeue(out var file);
            return file;
        }
    }
}