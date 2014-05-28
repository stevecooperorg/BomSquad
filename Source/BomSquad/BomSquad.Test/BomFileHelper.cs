using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomSquad.Test
{
    public class BomFileHelper
    {
        public string CreateFileWithBom(string bomType)
        {
            var filePath = Path.Combine(Path.GetTempPath(), "FileWith" + bomType + "Bom" + Guid.NewGuid().ToString("N") + ".txt");

            using (var file = File.OpenWrite(filePath))
            {
                if (bomType != null)
                {
                    var bomDefinition = BomDefinitions.GetBomDefinitions().First(bd => bd.Name == bomType);

                    // write bom
                    foreach (var byt in bomDefinition.BOM)
                    {
                        file.WriteByte(byt);
                    }
                }

                // write some simple content.
                WriteAsciiBytes(file, "foo");
            }

            return filePath;
        }

        private void WriteAsciiBytes(FileStream file, string content)
        {
            // write 'foo' in ascii;
            var fooBytes = System.Text.Encoding.ASCII.GetBytes(content);
            file.Write(fooBytes, 0, fooBytes.Length);
        }
    }
}
