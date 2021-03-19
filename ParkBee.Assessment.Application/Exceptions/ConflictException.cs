using System;
using System.Collections.Generic;
using System.Text;

namespace Triskele.Common.Application.Exceptions
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
