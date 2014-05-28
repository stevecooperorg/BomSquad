using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomSquad.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2 || !Directory.Exists(args[0]))
            {
                Console.WriteLine("Usage: BomSquad <directoryName>");
                return;
            }

            var rootDirectory = args[0];
            var filter = args[1];

            Console.WriteLine("Checking '{0}' for files matching '{1}'.", rootDirectory, filter);

            var dog = new SnifferDog();
            var fileCount = 0;
            foreach(var file in Directory.EnumerateFiles(rootDirectory, filter, SearchOption.AllDirectories))
            {
                fileCount++;
                var bomType = dog.DetectBomType(file);
                if (bomType != null)
                {
                    Console.WriteLine("Found a {0} BOM in '{1}'", bomType, file);
                }
            }

            Console.WriteLine("{0} total files checked", fileCount);
        }
    }
}
