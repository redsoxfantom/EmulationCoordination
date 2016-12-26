using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators
{
    public class EmulatorManagerException : Exception
    {
        public EmulatorManagerException()
        {
        }

        public EmulatorManagerException(string message) : base(message)
        {
        }

        public EmulatorManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmulatorManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
