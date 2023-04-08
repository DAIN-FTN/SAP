using System;

namespace SAP_API.Exceptions
{
    public class OrderCreationException: Exception
    {
        public OrderCreationException(string message) : base("Order creation failed: \n " + message)
        {
        }   
    }
}
