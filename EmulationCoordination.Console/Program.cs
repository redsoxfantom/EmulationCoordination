using System;

namespace EmulationCoordination
{
    class Program
    {
        static void Main(string[] args)
        {
            Processor proc = new Processor(args);
            proc.Run();
        }
    }
}
