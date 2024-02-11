using System;
using ScienceFileUploader.Exceptions.Shared;

namespace ScienceFileUploader.Exceptions.Value
{
    public class ValueBadRequestException : BadRequestException
    {
        public ValueBadRequestException(string message) : base(message)
        {
            
        }
    }
}