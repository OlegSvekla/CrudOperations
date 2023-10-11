using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Domain.Exeptions
{
    public sealed class InvalidValueException : Exception
    {
        public InvalidValueException(string message) : base()
        {
        }
    }
}