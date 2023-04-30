using System;


namespace SAP_API.Exceptions
{
    public class ForeignKeyViolationException: Exception
    {
        public ForeignKeyViolationException(string message): base(message)
        {

        }
    }
}
