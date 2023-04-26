using System;

namespace SAP_API.Exceptions
{
    public class UnableToDeactivateUserException: Exception
    {
        public UnableToDeactivateUserException(string message): base(message)
        {

        }
    }
}
