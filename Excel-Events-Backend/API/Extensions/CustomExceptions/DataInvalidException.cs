using System;
using System.Runtime.Serialization;

namespace API.Extensions.CustomExceptions
{
    public class DataInvalidException : Exception
    {
        public SerializationInfo Info { get; set; }
        public StreamingContext Context { get; set; }
        
        public DataInvalidException()
        {
        }

        public DataInvalidException(string message) : base(message)
        {
        }

        public DataInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Info = info;
            Context = context;
        }
    }
}