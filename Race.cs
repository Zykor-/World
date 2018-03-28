using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World
{
    public class Race
    {
        private string name;
        private int ageMod;
        private int heightMod;
        private int weightMod;

        public string Name { get => name; set => name = value; }
        public int AgeMod { get => ageMod; set => ageMod = value; }
        public int HeightMod { get => heightMod; set => heightMod = value; }
        public int WeightMod { get => weightMod; set => weightMod = value; }

        public Race(string n, int a, int h, int w)
        {
            Name = n;
            AgeMod = a;
            HeightMod = h;
            WeightMod = w;
        }
    }
}
