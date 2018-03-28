using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World
{

    //Settlement class
    //Constructors for:
    //iStructures only
    //NiStructres only
    //iStructures and NiStructures only

    class Settlement
    {
        private iStructure[] Abodes;
        private NiStructure[] MiscBuildings;

        //Constructor for settlement with INHABITABLE STRUCTURES ONLY
        public Settlement(iStructure [] abodes)
        {
            Abodes = abodes;
        }

        //Constructor for settlement with NON-INHABITABLE STRUCTURES ONLY
        public Settlement(NiStructure [] miscBuildings)
        {
            MiscBuildings = miscBuildings;
        }

        //Constructor for settlement with BOTH STRUCTURES ONLY
        public Settlement(iStructure [] abodes, NiStructure [] miscBuildings)
        {
            Abodes = abodes;
            MiscBuildings = miscBuildings;
        }
        

    }
}
