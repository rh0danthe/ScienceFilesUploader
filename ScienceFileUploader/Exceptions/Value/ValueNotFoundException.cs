using System;
using ScienceFileUploader.Exceptions.Shared;

namespace ScienceFileUploader.Exceptions.Value
{
    public class ValueNotFoundException : NotFoundException
    {
        public ValueNotFoundException(string message) : base(message)
        {
        }
    }
}