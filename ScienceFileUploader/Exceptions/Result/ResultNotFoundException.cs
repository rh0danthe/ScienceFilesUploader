using System;
using ScienceFileUploader.Exceptions.Shared;

namespace ScienceFileUploader.Exceptions.Result
{
    public class ResultNotFoundException : NotFoundException
    {
        public ResultNotFoundException(string message) : base(message)
        {
        }
    }
}