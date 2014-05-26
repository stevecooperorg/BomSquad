using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BomSquad.Test
{
    [TestClass]
    public class SnifferDogTests
    {
        [TestMethod]
        public void CanDetectUtf8Bom()
        {
            var utf8File = Path.Combine(Path.GetTempPath(), "utf8-test-file.txt");

            using (var file = File.OpenWrite(utf8File))
            {
                // utf8 bom
                file.WriteByte(0xEF);
                file.WriteByte(0xBB);
                file.WriteByte(0xBF);
                WriteFoo(file);
            }

            AssertHasBomType(utf8File, "UTF-8");
        }

        [TestMethod]
        public void CanDetectUtf16Bom()
        {
            var utf16File = Path.Combine(Path.GetTempPath(), "utf16-test-file.txt");

            using (var file = File.OpenWrite(utf16File))
            {
                // utf16 bom
                file.WriteByte(0xFF);
                file.WriteByte(0xFE);
                WriteFoo(file);
            }

            AssertHasBomType(utf16File, "UTF-16 (LE)");
        }

        private void WriteFoo(FileStream file)
        {
            // write 'foo' in ascii;
            var fooBytes = System.Text.Encoding.ASCII.GetBytes("foo");
            file.Write(fooBytes, 0, fooBytes.Length);
        }

        [TestMethod]
        public void CanDetectUtf32Bom()
        {
            var utf32File = Path.Combine(Path.GetTempPath(), "utf32-test-file.txt");

            using (var file = File.OpenWrite(utf32File))
            {
                // utf32 bom
                file.WriteByte(0xFF);
                file.WriteByte(0xFE);
                file.WriteByte(0x00);
                file.WriteByte(0x00);
                WriteFoo(file);

            }

            AssertHasBomType(utf32File, "UTF-32 (LE)");
        }

        public void CanDetectUtf7Bom()
        {
            var utf7File = Path.Combine(Path.GetTempPath(), "utf7-test-file.txt");

            using (var file = File.OpenWrite(utf7File))
            {
                // a utf17 bom
                file.WriteByte(0x2B);
                file.WriteByte(0x2F);
                file.WriteByte(0x76);
                file.WriteByte(0x38);
                WriteFoo(file);
            }

            AssertHasBomType(utf7File, "UTF7");
        }

        [TestMethod]
        public void CanDetectLackOfBom()
        {
            var noBomFile = Path.Combine(Path.GetTempPath(), "no-bom-test-file.txt");

            using (var file = File.OpenWrite(noBomFile))
            {
                WriteFoo(file);
            }

            var dog = new SnifferDog();
            Assert.IsFalse(dog.DetectBom(noBomFile));
            Assert.IsNull(dog.DetectBomType(noBomFile));
        }

        [TestMethod, ExpectedException(typeof(FileNotFoundException))]
        public void ExplodesOnMissingFile()        
        {
            var fakeFile = "c:\\" + Guid.NewGuid().ToString("N") + ".txt";
            new SnifferDog().DetectBom(fakeFile);
        }

        private void AssertHasBomType(string filePath, string bomType)
        {
            var dog = new SnifferDog();

            Assert.IsTrue(dog.DetectBom(filePath));
            Assert.AreEqual(bomType, dog.DetectBomType(filePath));
        }
    }
}
