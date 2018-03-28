using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World
{

    //pool is the list of every character in the generation
    class Generation
    {
        private List<Character> pool;
        private bool currentGen;
        private int pairs;

        public List<Character> Pool { get => pool; set => pool = value; }
        public bool CurrentGen { get => currentGen; set => currentGen = value; }
        public int Pairs { get => pairs; set => pairs = value; }

        //Constructor that initializes Pool
        public Generation()
        {
            Pool = new List<Character>();
            CurrentGen = false;
            Pairs = 0;
        }

        //Method to generate first generation
        public void GenerateBase(int c)
        {
            for(int i = 0; i < c / 2; i++)
            {
                Pool.Add(new Character(Globals.Human, "Male", 0));
            }
            for (int i = 0; i < c / 2; i++)
            {
                Pool.Add(new Character(Globals.Human, "Female", 0));
            }
            for (int i = 0; i < c / 2; i++)
            {
                Pool.Add(new Character(Globals.Dwarf, "Male", 0));
            }
            for (int i = 0; i < c / 2; i++)
            {
                Pool.Add(new Character(Globals.Dwarf, "Female", 0));
            }
            for (int i = 0; i < c / 2; i++)
            {
                Pool.Add(new Character(Globals.Elf, "Male", 0));
            }
            for (int i = 0; i < c / 2; i++)
            {
                Pool.Add(new Character(Globals.Elf, "Female", 0));
            }
            for (int i = 0; i < c / 2; i++)
            {
                Pool.Add(new Character(Globals.Gnome, "Male", 0));
            }
            for (int i = 0; i < c / 2; i++)
            {
                Pool.Add(new Character(Globals.Gnome, "Female", 0));
            }
        }

        //Method to pair couples together
        public void Pair()
        {
            //Iterate through the pool
            for(int i = 0; i<Pool.Count; i++)
            {
                //Check if current character is already married
                if(Pool[i].HasSpouse == false)
                {
                    //If not married iterate through spouse canditates
                    for(int q = 0; q<Pool.Count; q++)
                    {
                        //If no spouse, not same character, is same race, and opposite gender **CHANGE FOR INCLUDING HALF BREED********
                        if(Pool[q].HasSpouse == false && Pool[q].Id != Pool[i].Id && Pool[q].Race == Pool[i].Race && Pool[i].Sex != Pool[q].Sex)
                        {
                            //Aribitrary chance to be married, assign appropriate characteristics
                            if(Globals.Rand.Next(10) < 2)
                            {
                                Pool[i].HasSpouse = true;
                                Pool[i].Spouse = Pool[q];
                                Pool[i].SpouseIndex = q;
                                Pool[q].HasSpouse = true;
                                Pool[q].Spouse = Pool[i];
                                Pool[q].SpouseIndex = i;
                                pairs++;
                            }
                        }
                    }
                }
            }
            //if not enough couples to continue population growth, try for more couples. **LIKELY NEED TO CHANGE TO ENSURE FIRST FEW GENERATIONS HAVE ENOUGH KIDS IN EACH RACE************
            //if (Pairs < Pool.Count/4)
            //    Pair();
        }

        public Generation Next()
        {
            Generation nextGen = new Generation();
            Character temp;

            //Iterate through the Pool
            for(int i = 0; i < Pool.Count; i++)
            {
                //If current character has a spouse and doesn't have kids, try for kids
                if (Pool[i].HasSpouse && !Pool[i].HasKids)
                {
                    //Try for kids, if so make them else move on to next iteration
                    if(Globals.Rand.Next(10) <= 8)
                    {
                        Pool[i].HasKids = true;
                        Pool[Pool[i].SpouseIndex].HasKids = true;
                        Pool[i].NumKids = (Globals.Rand.Next(4) + 1);
                        Pool[Pool[i].SpouseIndex].NumKids = Pool[i].NumKids;

                        //Iterate through number of children, create them and add them to nextGen
                        for(int q = 0; q < Pool[i].NumKids; q++)
                        {
                            temp = new Character(Pool[i], Pool[Pool[i].SpouseIndex]);

                            Pool[i].Children.Add(temp);
                            Pool[Pool[i].SpouseIndex].Children.Add(temp);
                            nextGen.Pool.Add(temp);

                            if (temp.BirthYear >= Globals.CurrentYear)
                                CurrentGen = true;
                        }
                    }
                }
            }
            

            return nextGen;
        }
    }
}