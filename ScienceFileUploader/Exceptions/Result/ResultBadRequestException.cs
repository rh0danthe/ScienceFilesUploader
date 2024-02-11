using System;
using ScienceFileUploader.Exceptions.Shared;

namespace ScienceFileUploader.Exceptions.Result
{
    public class ResultBadRequestException : BadRequestException
    {
        public ResultBadRequestException(string message) : base(message)
        {
            
        }
    }
}