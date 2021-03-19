using System;

namespace ParkBee.Assessment.Application.Exceptions
{
    public class PrimaryKeyNotFoundException : Exception
    {
        public PrimaryKeyNotFoundException(string message)
            : base(message)
        {
        }
    }
}
