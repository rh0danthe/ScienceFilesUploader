using System.Collections.Generic;
using ScienceFileUploader.Dto;

namespace ScienceFileUploader.BackgroundWorker
{
    public class FileStorageQueue
    {
        private readonly Queue<FileRequest> _queue = new();

        public void AddFile(FileRequest file)
        {
            _queue.Enqueue(file);
        }
        public FileRequest GetFile()
        {
            return _queue.Dequeue();
        }
    }
}