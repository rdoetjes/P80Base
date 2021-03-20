using NUnit.Framework;
using System;
using P80;

namespace Tests
{
    [TestFixture()]
    public class P80TestClass
    {
        [Test()]
        public void Test_1_LDA()
        {
            CPU p = new CPU();
            p.LDA(12);
            Assert.AreEqual(12, p.A);
            Assert.AreEqual(2, p.CYCLES);
            Assert.AreEqual(1, p.PC);
            Assert.AreEqual(false, p.ZF);

            p.LDA(0);
            Assert.AreEqual(4, p.CYCLES);
            Assert.AreEqual(2, p.PC);
            Assert.AreEqual(true, p.ZF);
        }

        [Test()]
        public void Test_2_SWP()
        {
            CPU p = new CPU();
            p.B = 24;
            p.LDA(12);
            p.SWP();

            Assert.AreEqual(4, p.CYCLES);
            Assert.AreEqual(2, p.PC);
            Assert.AreEqual(false, p.ZF);
            Assert.AreEqual(24, p.A);
            Assert.AreEqual(12, p.B);

            p = new CPU();
            p.B = 0;
            p.LDA(12);
            p.SWP();

            Assert.AreEqual(4, p.CYCLES);
            Assert.AreEqual(2, p.PC);
            Assert.AreEqual(true, p.ZF);
            Assert.AreEqual(0, p.A);
            Assert.AreEqual(12, p.B);
        }

        [Test()]
        public void Test_2_SAV()
        {
            CPU p = new CPU();
            p.B = 24;
            p.LDA(12);
            p.SAV();

            Assert.AreEqual(3, p.CYCLES);
            Assert.AreEqual(2, p.PC);
            Assert.AreEqual(false, p.ZF);
            Assert.AreEqual(12, p.A);
            Assert.AreEqual(12, p.B);
        }

        [Test()]
        public void Test_3_SUB()
        {
            CPU p = new CPU();
            p.LDA(1);
            p.SUB(12);
            Assert.AreEqual(245, p.A);
            Assert.AreEqual(5, p.CYCLES);
            Assert.AreEqual(2, p.PC);
            Assert.AreEqual(false, p.ZF);

            p.SUB(10);
            Assert.AreEqual(235, p.A);
            Assert.AreEqual(8, p.CYCLES);
            Assert.AreEqual(3, p.PC);
            Assert.AreEqual(false, p.ZF);

            p.SUB(235);
            Assert.AreEqual(0, p.A);
            Assert.AreEqual(11, p.CYCLES);
            Assert.AreEqual(4, p.PC);
            Assert.AreEqual(true, p.ZF);

        }

        [Test()]
        public void Test_3_SUBB()
        {
            CPU p = new CPU();
            p.LDA(13);
            p.SAV();
            p.LDA(12);
            p.SUBB();
            Assert.AreEqual(255, p.A);
            Assert.AreEqual(7, p.CYCLES);
            Assert.AreEqual(4, p.PC);
            Assert.AreEqual(false, p.ZF);

            p.LDA(13);
            p.SUBB();
            Assert.AreEqual(0, p.A);
            Assert.AreEqual(11, p.CYCLES);
            Assert.AreEqual(6, p.PC);
            Assert.AreEqual(true, p.ZF);
        }


        [Test()]
        public void Test_3_ADD()
        {
            CPU p = new CPU();
            p.LDA(1);
            p.ADD(12);
            Assert.AreEqual(13, p.A);
            Assert.AreEqual(5, p.CYCLES);
            Assert.AreEqual(2, p.PC);
            Assert.AreEqual(false, p.ZF);

            p.ADD(12);
            Assert.AreEqual(25, p.A);
            Assert.AreEqual(8, p.CYCLES);
            Assert.AreEqual(3, p.PC);
            Assert.AreEqual(false, p.ZF);

            p.ADD(231);
            Assert.AreEqual(0, p.A);
            Assert.AreEqual(11, p.CYCLES);
            Assert.AreEqual(4, p.PC);
            Assert.AreEqual(true, p.ZF);

        }

        [Test()]
        public void Test_3_ADDB()
        {
            CPU p = new CPU();
            p.LDA(13);
            p.SAV();
            p.LDA(12);
            p.ADDB();
            Assert.AreEqual(25, p.A);
            Assert.AreEqual(7, p.CYCLES);
            Assert.AreEqual(4, p.PC);
            Assert.AreEqual(false, p.ZF);

            p.LDA(243);
            p.ADDB();
            Assert.AreEqual(0, p.A);
            Assert.AreEqual(11, p.CYCLES);
            Assert.AreEqual(6, p.PC);
            Assert.AreEqual(true, p.ZF);
        }

        [Test()]
        public void Test_4_AND()
        {
            CPU p = new CPU();
            p.LDA(255);
            p.AND(128);
            Assert.AreEqual(128, p.A);
            Assert.AreEqual(3, p.CYCLES);
            Assert.AreEqual(2, p.PC);
            Assert.AreEqual(false, p.ZF);

            p.AND(0);
            Assert.AreEqual(0, p.A);
            Assert.AreEqual(4, p.CYCLES);
            Assert.AreEqual(3, p.PC);
            Assert.AreEqual(true, p.ZF);
        }

        [Test()]
        public void Test_4_OR()
        {
            CPU p = new CPU();
            p.LDA(1);
            p.OR(128);

            Assert.AreEqual(129, p.A);
            Assert.AreEqual(3, p.CYCLES);
            Assert.AreEqual(2, p.PC);
            Assert.AreEqual(false, p.ZF);

            p.LDA(0);
            p.OR(0);

            Assert.AreEqual(0, p.A);
            Assert.AreEqual(6, p.CYCLES);
            Assert.AreEqual(4, p.PC);
            Assert.AreEqual(true, p.ZF);
        }

        [Test()]
        public void Test_4_SHL()
        {
            CPU p = new CPU();
            p.LDA(1);
            p.SHL();
            p.SHL();
            Assert.AreEqual(4, p.A);
            Assert.AreEqual(4, p.CYCLES);
            Assert.AreEqual(3, p.PC);
            Assert.AreEqual(false, p.ZF);
            p.SHL();
            p.SHL();
            p.SHL();
            p.SHL();
            p.SHL();
            p.SHL();
            Assert.AreEqual(0, p.A);
            Assert.AreEqual(10, p.CYCLES);
            Assert.AreEqual(9, p.PC);
            Assert.AreEqual(true, p.ZF);
        }

        [Test()]
        public void Test_4_SHR()
        {
            CPU p = new CPU();
            p.LDA(128);
            p.SHR();
            p.SHR();
            Assert.AreEqual(32, p.A);
            Assert.AreEqual(4, p.CYCLES);
            Assert.AreEqual(3, p.PC);
            Assert.AreEqual(false, p.ZF);
            p.SHR();
            p.SHR();
            p.SHR();
            p.SHR();
            p.SHR();
            p.SHR();
            Assert.AreEqual(0, p.A);
            Assert.AreEqual(10, p.CYCLES);
            Assert.AreEqual(9, p.PC);
            Assert.AreEqual(true, p.ZF);
        }

        [Test()]
        public void Test_5_NOP()
        {
            CPU p = new CPU();
            p.NOP();
            Assert.AreEqual(0, p.A);
            Assert.AreEqual(1, p.CYCLES);
            Assert.AreEqual(1, p.PC);
        }


        [Test()]
        public void Test_6_JMP()
        {
            CPU p = new CPU();
            p.JMP(10);
            Assert.AreEqual(0, p.A);
            Assert.AreEqual(3, p.CYCLES);
            Assert.AreEqual(10, p.PC);
        }

        [Test()]
        public void Test_6_JNZ()
        {
            CPU p = new CPU();
            p.LDA(10);
            p.JNZ(10);
            Assert.AreEqual(false, p.ZF);
            Assert.AreEqual(6, p.CYCLES);
            Assert.AreEqual(10, p.PC);

            p = new CPU();
            p.LDA(0);
            p.JNZ(10);
            Assert.AreEqual(true, p.ZF);
            Assert.AreEqual(6, p.CYCLES);
            Assert.AreEqual(2, p.PC);
        }

        [Test()]
        public void Test_7_CMP()
        {
            CPU p = new CPU();
            p.LDA(10);
            p.CMP(10);
            Assert.AreEqual(true, p.ZF);
            Assert.AreEqual(6, p.CYCLES);
            Assert.AreEqual(2, p.PC);

            p.LDA(10);
            p.CMP(11);
            Assert.AreEqual(false, p.ZF);
            Assert.AreEqual(12, p.CYCLES);
            Assert.AreEqual(4, p.PC);
        }

        [Test()]
        public void Test_8_OUT()
        {
            CPU p = new CPU();
            p.LDA(10);
            p.OUT();

            Assert.AreEqual(false, p.ZF);
            Assert.AreEqual(6, p.CYCLES);
            Assert.AreEqual(2, p.PC);
        }

        [Test()]
        public void Test_8_IN()
        {
            CPU p = new CPU();
            p.P1 = 255;
            p.IN();

            Assert.AreEqual(false, p.ZF);
            Assert.AreEqual(4, p.CYCLES);
            Assert.AreEqual(1, p.PC);

            p.P1 = 0;
            p.IN();

            Assert.AreEqual(true, p.ZF);
            Assert.AreEqual(8, p.CYCLES);
            Assert.AreEqual(2, p.PC);
        }
    }        
}
