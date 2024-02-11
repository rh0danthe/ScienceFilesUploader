using System;
using ScienceFileUploader.Exceptions.Shared;

namespace ScienceFileUploader.Exceptions.File
{
    public class FileNotFoundException : NotFoundException
    {
        public FileNotFoundException(string message) : base(message)
        {
        }
    }
}