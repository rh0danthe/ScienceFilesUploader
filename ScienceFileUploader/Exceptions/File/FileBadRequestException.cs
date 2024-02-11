using System;
using ScienceFileUploader.Exceptions.Shared;

namespace ScienceFileUploader.Exceptions.File
{
    public class FileBadRequestException : BadRequestException
    {
        public FileBadRequestException(string message) : base(message)
        {
            
        }
    }
}