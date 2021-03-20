using System;
using P80Program;

namespace P80
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //THE MAIN FUNCTION WILL BE REPLACED BY UNITY IT'S NOT DOING ANYTHING RIGHT NOW
            Console.WriteLine("KREMLIN P80");

            Program p = new Program();
            String code = "";
            code += "  LDA 1\r\n";
            code += "UP:\r\n";
            code += "  SHL\r\n";
            code += "  CMP 128\r\n";
            code += "  JNZ UP\r\n";
            code += "DOWN:\r\n";
            code += "  SHR\r\n";
            code += "  CMP 1\r\n";
            code += "  JNZ DOWN\r\n";
            code += "  JMP UP\r\n";

            Console.SetCursorPosition(0, 2);
            Console.WriteLine(code);

            p.Run(code);
        }
    }
}
