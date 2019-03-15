using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Utilities
{
    public class EmulationCoordinationException : Exception
    {
        public EmulationCoordinationException()
        {
        }

        public EmulationCoordinationException(string message) : base(message)
        {
        }

        public EmulationCoordinationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmulationCoordinationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
