using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World
{
    class Program
    {
        static void Main(string[] args)
        {
            //Establish Globals
            Globals.CurrentYear = 10000;
            Globals.Rand = new Random();
            Globals.Races = new List<string>();
            Globals.ID = 0;

            //Set up races
            Globals.Human = new Race("Human", 15, 25, 30);
            Globals.Dwarf = new Race("Dwarf", 55, 15, 40);
            Globals.Elf = new Race("Elf", 175, 22, 12);
            Globals.Gnome = new Race("Gnome", 45, 6, -26);

            int it = 0;
            bool Current = false;

            List<Generation> Generations = new List<Generation>();
            Generations.Add(new Generation());
            Generations[0].GenerateBase(10);

            //Continue creating generations till the current generation is complete
            while(!Current)
            {
                Generations[it].Pair();
                Generations.Add(Generations[it].Next());
                Current = Generations[it].CurrentGen;                

                Console.WriteLine("Gen {0}: {1} people, {2} pairs.", it, Generations[it].Pool.Count, Generations[it].Pairs);
                if (Generations[it].Pool.Count == 0)
                    Current = true;
                it++;
            }           
        }
    }
}
