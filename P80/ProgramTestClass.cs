using NUnit.Framework;
using System;
using P80Program;
using System.Threading;

namespace Tests
{
    [TestFixture()]
    public class ProgramTestClass
    {
        [Test()]
        public void ProgramTest()
        {
            Program p = new Program();
            String code = "LDA 1\r\n";
                  code += "SHL\r\n";
                  code += "HELLO:\r\n";
                  code += "NOP\r\n";
                  code += "SUB 1\r\n";
                  code += "JNZ HELLO\r\n";

            p.Run(code);

            while (p.isRunning)
            {
                Thread.Sleep(300);
            }

            Assert.AreEqual(true, p.p.ZF);
            Assert.AreEqual(19, p.p.CYCLES);
            Assert.AreEqual(0, p.p.A);
        }
    }
}
