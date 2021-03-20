using System;
using System.Collections.Generic;
using P80;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace P80Program
{
    public class Program
    {

        private List<String> program;

        public CPU p;

        public bool isRunning = false;

        public int msSleepPerInstruction = 100;

        public Program()
        {
            p = new CPU();

            program = new List<string>();

            program.Clear();

        }

        private void Execute(String ins)
        {
            Regex rx = new Regex(@"\d+:",
               RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //if it's labels than inc PC and no execution required
            Match m = rx.Match(ins);

            if (m.Success)
            {
                p.PC++;
                return;
            }

            //This is an executable instruction
            rx = new Regex(@"(\w+)(\s(\d+))?",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            m = rx.Match(ins);

            if (!m.Success)
            {
                p.PC++;
                return;
            } 

            Type cpuType = p.GetType();
            try
            {
                MethodInfo cpuInstruction = cpuType.GetMethod(m.Groups[1].Value);
                if (m.Groups[2].Value != "")
                    cpuInstruction.Invoke(p, new object[] { Convert.ToByte(Int32.Parse(m.Groups[2].Value) )});
                else
                    cpuInstruction.Invoke(p, null);

                Thread.Sleep(msSleepPerInstruction);
            }
            catch(NullReferenceException e)
            {
                p.PC++;
                Debug.WriteLine( e.Message );
            }
        }

        /*
         * Strips multiple CRLF and spaces
         * as well as leading spaces and CRLF
         */
        private String cleanUpCode(String ins)
        {
            String parsedString = Regex.Replace(ins, "\\s+[\r\n]+\\s+", "\r\n");
            parsedString = Regex.Replace(parsedString, "[\r\n]{2,}", "\r\n");
            parsedString = Regex.Replace(parsedString, "^[\r\n]{1,}", "");
            parsedString = Regex.Replace(parsedString, "[ ]+", " ");
            parsedString = Regex.Replace(parsedString, "\r\n", "\n");
            parsedString = Regex.Replace(parsedString, "[\r\n]$", "");
            return parsedString;
        }

        private List<String> LabelToLineNumber(String ins)
        {
            String cleanCode = cleanUpCode(ins);

            program = cleanCode.Split('\n').OfType<String>().ToList();

            Regex rx = new Regex(@"(.*):",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);


            Dictionary<String, int> labelLineNr = new Dictionary<string, int>();

            for(int i=0; i<program.Count; ++i)
            {
                Match m = rx.Match(program[i]);
                if (m.Success)
                {
                    System.Diagnostics.Debug.WriteLine(program[i]);
                    labelLineNr.Add(m.Groups[1].Value, i);
                }
            }

            //replace all label occurences with line #
            foreach (String key in labelLineNr.Keys)
            {
                cleanCode = Regex.Replace(cleanCode, key, labelLineNr[key].ToString());
            }

            return cleanCode.Split('\n').OfType<String>().ToList();
            
        }

        public void DisplayRegisters(CPU p)
        {
            Console.SetCursorPosition(40, 1);
            Console.WriteLine("|     A:  "+p.A.ToString().PadLeft(3));

            Console.SetCursorPosition(40, 2);
            Console.WriteLine("|     B:  " + p.B.ToString().PadLeft(3));

            Console.SetCursorPosition(40, 3);
            String ZF = (p.ZF == true) ? "1" : "0";
            Console.WriteLine("|    ZF:  " + ZF.PadLeft(3)); 

            Console.SetCursorPosition(40, 4);
            Console.WriteLine("|    PC:  " + p.PC.ToString().PadLeft(3));

            Console.SetCursorPosition(40, 5);
            Console.WriteLine("|P1: " + Convert.ToString(p.P1, 2).PadLeft(8, '0'));
        }

        private void RunCode()
        {
            while (p.PC < program.Count)
            {
                Execute(program[p.PC]);
                DisplayRegisters(p);
            }
            isRunning = false;
        }

        public void Run(String code)
        {
            program = LabelToLineNumber(code);

            isRunning = true;   //this has to be here because Start() takes more time than test will Assert the results
            Thread progThread = new Thread(new ThreadStart(RunCode));
            progThread.Start();
        }
    }
}
