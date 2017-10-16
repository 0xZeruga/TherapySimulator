using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapist_Simulator
{
    /// <summary>
    /// For line counting:
    /// ^(?([^rn])s)*[^s+?/]+[^n]*$

    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            new Intro();
        }

    }

    public class Intro
    {
        private Therapist Player = new Therapist();

        public Intro()
        {
            Console.WriteLine("Welcome to the Therapist Simulator \n Please insert your name.");
            Player.Name = Console.ReadLine();
            Console.WriteLine("Welcome " + Player.Name + ". This is your first day at work so we expect you to perform.\n Every individual is different, so get to know them is very important\n in order to earn their trust.\n Are you ready to begin?");
            ReadyCheck(Player);    
            HowToHelp();
        }

        void ReadyCheck(Therapist pPlayer)
        {  
            var response = Console.ReadLine();
            if (response.Contains("y") || response.Contains("Y")) { Console.WriteLine("Good... Now, just before we start"); }
            else
            {
                Console.WriteLine("We expect you to answer appropriately Doctor");
                ReadyCheck(pPlayer);
            }
        }

        void HowToHelp()
        {
            Console.WriteLine("How do you intend to work to help your clients?");
            Console.WriteLine("1. By related to them on an emotional level");
            Console.WriteLine("2. By solving their problems by logical deduction");
            Console.WriteLine("3. By listening to whatever they have to say");

            var read = Console.ReadLine();
            if(read.Contains("1") || read.Contains("emo"))
            {
                Player.Empathy += 2;
                Console.WriteLine("Your Empathy improved to: " + Player.Empathy);
                Console.Read();
            }
            else if(read.Contains("2")|| read.Contains("log"))
            {
                Player.ProblemSolving += 2;
                Console.WriteLine("Your Problem Solving improved to: " + Player.ProblemSolving);
                Console.Read();
            }
            else if(read.Contains("3") || read.Contains("list"))
            {
                Player.Listening += 2;
                Console.WriteLine("Your Listening improved to: " + Player.Listening);
                Console.Read();
            }
            else
            {
                Console.WriteLine("I'm not sure you understood my question Doctor.");
                HowToHelp();
            }

        }
    }
}
