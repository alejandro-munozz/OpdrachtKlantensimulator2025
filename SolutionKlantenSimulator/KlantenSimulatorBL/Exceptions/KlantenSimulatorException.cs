using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace KlantenSimulatorBL.Exceptions
{
    public class KlantenSimulatorException : Exception
    {
        public KlantenSimulatorException()
        {
        }

        public KlantenSimulatorException(string? message) : base(message)
        {
        }

        public KlantenSimulatorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected KlantenSimulatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
