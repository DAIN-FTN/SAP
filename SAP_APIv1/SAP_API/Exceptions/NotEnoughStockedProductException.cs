using System;

namespace SAP_API.Exceptions
{
    public class NotEnoughStockedProductException: Exception
    {
        public NotEnoughStockedProductException() : base("There isn't enough products in stock")
        {
        }
    }
}
