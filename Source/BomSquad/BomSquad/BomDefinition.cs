using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomSquad
{
    internal class BomDefinition
    {
        internal string Name { get; set; }

        internal byte[] BOM { get; set; }


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

    internal static class BomDefinitions
    {
        private static int maxBomLength = -1; // -1 == not calculated

        internal static IEnumerable<BomDefinition> GetBomDefinitions()
        {
            yield return new BomDefinition { Name = "UTF-7",       BOM = new byte[] { 0x2B, 0x2F, 0x76, 0x38, 0x2D } };
            yield return new BomDefinition { Name = "UTF-EBCDIC",  BOM = new byte[] { 0xDD, 0x73, 0x66, 0x73 } };
            yield return new BomDefinition { Name = "UTF-7",       BOM = new byte[] { 0x2B, 0x2F, 0x76, 0x39 } };
            yield return new BomDefinition { Name = "UTF-7",       BOM = new byte[] { 0x2B, 0x2F, 0x76, 0x38 } };
            yield return new BomDefinition { Name = "UTF-7",       BOM = new byte[] { 0x2B, 0x2F, 0x76, 0x2B } };
            yield return new BomDefinition { Name = "UTF-32 (BE)", BOM = new byte[] { 0x00, 0x00, 0xFE, 0xFF } };
            yield return new BomDefinition { Name = "UTF-32 (LE)", BOM = new byte[] { 0xFF, 0xFE, 0x00, 0x00 } };
            yield return new BomDefinition { Name = "UTF-7",       BOM = new byte[] { 0x2B, 0x2F, 0x76, 0x2F } };
            yield return new BomDefinition { Name = "GB-18030",    BOM = new byte[] { 0x84, 0x31, 0x95, 0x33 } };
            yield return new BomDefinition { Name = "UTF-8",       BOM = new byte[] { 0xEF, 0xBB, 0xBF } };
            yield return new BomDefinition { Name = "UTF-16 (BE)", BOM = new byte[] { 0xFE, 0xFF } };
            yield return new BomDefinition { Name = "UTF-16 (LE)", BOM = new byte[] { 0xFF, 0xFE } };
            yield return new BomDefinition { Name = "UTF-1",       BOM = new byte[] { 0xF7, 0x64, 0x4C } };
            yield return new BomDefinition { Name = "SCSU",        BOM = new byte[] { 0x0E, 0xFE, 0xFF } };
            yield return new BomDefinition { Name = "BOCU-1",      BOM = new byte[] { 0xFB, 0xEE, 0x28 } };
        }

        internal static int MaxBomLength
        {
            get
            {
                if (maxBomLength == -1)
                {
                    foreach (var bomDefinition in GetBomDefinitions())
                    {
                        maxBomLength = Math.Max(maxBomLength, bomDefinition.BOM.Length);
                    }
                }

                return maxBomLength;
            }
        }
    }
}
