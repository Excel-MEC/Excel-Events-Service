using System;
using System.Runtime.Serialization;

namespace API.Extensions.CustomExceptions
{
    public class OperationInvalidException : Exception
    {
        public SerializationInfo Info { get; set; }
        public StreamingContext Context { get; set; }
        
        public OperationInvalidException()
        {
        }

        public OperationInvalidException(string message) : base(message)
        {
        }

        public OperationInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OperationInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Info = info;
            Context = context;
        }
    }
}