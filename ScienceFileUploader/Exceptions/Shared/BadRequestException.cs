using System;
using System.IO.Pipelines;

namespace ScienceFileUploader.Exceptions.Shared
{
    public class BadRequestException : Exception
    {
        protected BadRequestException(string message) : base(message)
        {
        }
    }
}