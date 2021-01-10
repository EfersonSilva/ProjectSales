using System;

namespace ProjectSales.Services.Exceptions
{
    public class DbConcurrencyException:ApplicationException
    {
        public DbConcurrencyException(string message) : base(message)
        {
        }
    }
}
