using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomSquad
{
    public class BomDefinition
    {
        public string Name { get; set; }

        public byte[] BOM { get; set; }


        internal bool IsMatch(byte[] bytes)
        {
            if (bytes.Length < BOM.Length)
            {
                return false;
            }

            for (var i = 0; i < BOM.Length; i++)
            {
                if (bytes[i] != BOM[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
