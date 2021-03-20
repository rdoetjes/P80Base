using System;
namespace P80
{
    public class CPU
    {
        public byte A;
        public byte B;
        public int PC;
        public int CYCLES;
        public bool ZF;

        public void LDA(byte V)
        {
            CYCLES += 2;
            A = V;

            ZF = (A == 0)? true : false;
            PC++; 
        }

        public void SWAP()
        {
            CYCLES += 2;
            byte C = A;
            A = B;
            B = C;

            ZF = (A == 0) ? true : false;
            PC++;
        }

        public void SAVE()
        {
            CYCLES += 1;
            B = A;

            PC++;
        }

        public void ADD(byte V)
        {
            CYCLES += 3;
            A += V;

            ZF = (A == 0) ? true : false;
            PC++;
        }

        public void ADDB()
        {
            CYCLES += 2;
            A += B;

            ZF = (A == 0) ? true : false;
            PC++;
        }

        public void SUB(byte V)
        {
            CYCLES += 3;
            A -= V;

            ZF = (A == 0) ? true : false;
            PC++;
        }

        public void SUBB()
        {
            CYCLES += 2;
            A -= B;

            ZF = (A == 0) ? true : false;
            PC++;
        }

        public void AND(byte V)
        {
            CYCLES += 1;
            A = (byte)(A & V);

            ZF = (A == 0) ? true : false;
            PC++;  
        }

        public void OR(byte V)
        {
            CYCLES += 1;
            A = (byte)(A | V);

            ZF = (A == 0) ? true : false;
            PC++;   
        }

        public void SHL()
        {
            CYCLES += 1;
            A = (byte)(A << 1);

            ZF = (A == 0) ? true : false;
            PC++;
        }

        public void SHR()
        {
            CYCLES += 1;
            A = (byte)(A >> 1);

            ZF = (A == 0) ? true : false;
            PC++;
        }

        public void JMP(ushort L)
        {
            CYCLES += 3;
            PC = L;
        }

        public void JNZ(ushort L)
        {
            CYCLES += 4;
            PC++;
            if (ZF) return;
            PC = L;
        }

        public void JZ(ushort L)
        {
            CYCLES += 4;
            PC++;
            if (!ZF) return;
            PC = L;
        }

        public void NOP()
        {
            CYCLES += 1;
            PC++;
        }

        public void CMP(byte V)
        {
            CYCLES += 4;
            ZF = (A - V == 0) ? true : false;
            PC++;
        }

        public CPU()
        {
            A = 0;
            B = 0;
            CYCLES = 0;
            ZF = true;
        }
    }
}
