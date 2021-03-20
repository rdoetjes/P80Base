using System;

namespace P80
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //THE MAIN FUNCTION WILL BE REPLACED BY UNITY IT'S NOT DOING ANYTHING RIGHT NOW
            Console.WriteLine("KREMLIN P80");
            CPU p = new CPU();
            p.LDA(16);
            p.SHL();
            p.SHL();
            Console.WriteLine(p.A);
        }
    }
}
