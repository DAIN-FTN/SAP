using System;

namespace SAP_API.Exceptions
{
    public class BadProgramStatusException : Exception
    {
        public BadProgramStatusException(string message) : base(message)
        {
        }
    }
}
