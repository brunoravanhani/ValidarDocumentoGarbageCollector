using System;
using System.Diagnostics;

namespace ValidarCpf
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            var before2 = GC.CollectionCount(2);
            var before1 = GC.CollectionCount(1);
            var before0 = GC.CollectionCount(0);

            sw.Start();
            for (int i = 0; i < 1_000_000; i++)
            {
                if (!Validador.ValidarCPF("771.189.500-33"))
                {
                    throw new Exception("Error!");
                }

                if (Validador.ValidarCPF("771.189.500-34"))
                {
                    throw new Exception("Error!");
                }
            }
            sw.Stop();

            Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"GC Gen #2  : {GC.CollectionCount(2) - before2}");
            Console.WriteLine($"GC Gen #1  : {GC.CollectionCount(1) - before1}");
            Console.WriteLine($"GC Gen #0  : {GC.CollectionCount(0) - before0}");
            Console.WriteLine("Done!");
        }
    }
}
