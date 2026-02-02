using System;
using System.Collections.Generic;
using System.Text;

namespace M1Exception
{
    public class InvalidEntryException : Exception
    {
        public InvalidEntryException(string message) : base(message)
        {
        }
    }
}
