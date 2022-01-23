using System;

namespace DocPro.Persistence.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException()
        {

        }

        public CustomException(string message)
            : base(message)
        {

        }
    }
}
