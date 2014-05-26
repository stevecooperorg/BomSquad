using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomSquad
{
    public class SnifferDog
    {
        public bool DetectBom(string filePath)
        {
            return this.DetectBomType(filePath) != null;

        }

        public object DetectBomType(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Could not detect the BOM because the file does not exist.", filePath);
            }

            var maxBomLength = BomDefinitions.MaxBomLength;

            using (var file = File.OpenRead(filePath))
            {
                byte[] bytes = new byte[maxBomLength];
                file.Read(bytes, 0, maxBomLength);
                var definition = BomDefinitions.GetBomDefinitions().FirstOrDefault(def => def.IsMatch(bytes));
                if (definition != null)
                {
                    return definition.Name;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
