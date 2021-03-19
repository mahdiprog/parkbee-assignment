using System;

namespace ParkBee.Assessment.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string propertyName,object propertyValue, string entityName)
            : base($"\"{propertyValue}\" for {propertyName} of {entityName} already exists.")
        {
        }

        public ConflictException(string message) : base(message)
        {
        }
    }
}
