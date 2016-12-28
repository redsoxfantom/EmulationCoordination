using EmulationCoordination.Emulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination
{
    class Program
    {
        static void Main(string[] args)
        {
            Processor proc = new Processor();
            proc.Run();
        }
    }
}
