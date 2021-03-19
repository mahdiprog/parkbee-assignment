using System;

namespace Triskele.Common.Application.Exceptions
{
    public class PrimaryKeyNotFoundException : Exception
    {
        public PrimaryKeyNotFoundException(string message)
            : base(message)
        {
        }
    }
}
