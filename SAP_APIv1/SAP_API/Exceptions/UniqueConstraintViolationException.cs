using System;


namespace SAP_API.Exceptions
{
    public class UniqueConstraintViolationException: Exception
    {
        public UniqueConstraintViolationException():base()
        {

        }

        public UniqueConstraintViolationException(string message) : base(message)
        {

        }
    }
}
