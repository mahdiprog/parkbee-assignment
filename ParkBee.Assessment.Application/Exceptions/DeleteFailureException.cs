using System;

namespace Triskele.Common.Application.Exceptions
{
    public class DeleteFailureException : Exception
    {
        public DeleteFailureException(string name, object key, string message)
            : base($"Deletion of entity \"{name}\" ({key}) failed. {message}")
        {
        }
        public DeleteFailureException(string message) : base(message)
        {
        }
    }
}
