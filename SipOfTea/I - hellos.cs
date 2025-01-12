using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipOfTea
{
    public class Starter
    {

        Script read = new Script();
        Colors change = new Colors();
        public void startup()
        {
            read.printline("Hello, World!", 10);
            read.printline("...", 10);
            Thread.Sleep(1000);

            Random rnd = new Random();
            // rnd.next(a,b) -> random no. bw a, b
            
            for (int i = 0; i < 3; i++)
            {
                read.printline("Hello; World!", 10);
                Thread.Sleep(rnd.Next(0, 100));
            }

            for (int i = 0; i < 3; i++)
            {
                read.printline("Hello; World!", 10);
                Thread.Sleep(rnd.Next(0, 1000));
            }

            Thread.Sleep(1938);
            read.printline("eiwevurmomfwf", 20);
            Thread.Sleep(2124);

            string hel = "Hello, World!";
            for (int deg = 0; deg < 9; deg++)
            {
                string output = "";
                for (int letter = 0; letter < hel.Length; letter++)
                {
                    int choice = rnd.Next(0, hel.Length);
                    
                    if (choice < Math.Min(deg, 7)) { output += (char) rnd.Next(32, 127); }
                    else { output += hel[letter]; }
                }
                
                foreach (char c in output)
                {
                    int choice = rnd.Next(hel.Length);
                    if (choice < deg) { change.forecol(rnd.Next(1, 32)); }
                    Console.Write(c);
                    Thread.Sleep(rnd.Next(5,25));
                    if (choice < deg) { change.forecol(); }
                }
                Console.WriteLine();
                Thread.Sleep(rnd.Next(deg * 100, 2000));
            }

            // helloworlds over


        }
    }
}

/*
namespace SipOfTea
{
    public class starter 
    {

        public void startup()
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("...");

            Random rnd = new Random();
            // rnd.next = (a,b) -> gives random no. bw a, b

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Hello, World");
            }

            string string_alter(string input, int degree)
            {
                string output = "";

                for (int i = 0; i < input.Length; i++)
                {
                    int choice = rnd.Next(0, degree) / input.Length;
                    if (choice > 1) { output += (char)rnd.Next(32, 127); }
                    else { output += input[i]; }
                }
                return output;
            }

                string hel = "Hello, World!";
                for (int i = 0; i < 8; i++)
                {
                    Console.WriteLine(string_alter(hel, i));
                }

            }
        }
    }
}
*/