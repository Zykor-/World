using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace World
{

    //For non self-explanatory characteristics
    //id is a unique identifier, may not need TBD
    //spouseIndex is the number used in a generation pool indicated which character is their spouse
    public class Character
    {
        private Race race;
        private string sex;
        private string firstName;
        private string occupation;
        private string lastName;
        private string hairColor;
        private string eyeColor;
        private string currentLocation;
        private int height;
        private int weight;
        private int age;
        private int birthYear;
        private int lifeSpan;                      
        private int numKids;
        private int id;
        private int spouseIndex;
        private bool alive;
        private bool hasSpouse;
        private bool hasKids;
        private Character spouse;
        private Character mother;
        private Character father;
        private List<Character> children;

        public string Sex { get => sex; set => sex = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public int Height { get => height; set => height = value; }
        public int Weight { get => weight; set => weight = value; }
        public string Occupation { get => occupation; set => occupation = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Age { get => age; set => age = value; }
        public int BirthYear { get => birthYear; set => birthYear = value; }
        public string HairColor { get => hairColor; set => hairColor = value; }
        public string EyeColor { get => eyeColor; set => eyeColor = value; }
        public string CurrentLocation { get => currentLocation; set => currentLocation = value; }
        public bool HasKids { get => hasKids; set => hasKids = value; }
        public int NumKids { get => numKids; set => numKids = value; }
        public bool Alive { get => alive; set => alive = value; }
        public int Id { get => id; set => id = value; }
        public bool HasSpouse { get => hasSpouse; set => hasSpouse = value; }
        public Character Mother { get => mother; set => mother = value; }
        public Character Father { get => father; set => father = value; }
        public List<Character> Children { get => children; set => children = value; }
        public Character Spouse { get => spouse; set => spouse = value; }
        public Race Race { get => race; set => race = value; }
        public int LifeSpan { get => lifeSpan; set => lifeSpan = value; }
        public int SpouseIndex { get => spouseIndex; set => spouseIndex = value; }

        public Character()
        {
            Id = Globals.ID;
            Globals.ID++;
            HasSpouse = false;
        }

        //Generates Character with the given race, sex, and birthyear
        public Character(Race r, string s, int b)
        {
            Id = Globals.ID;
            Globals.ID++;
            HasSpouse = false;
            HasKids = false;
            Race = r;
            Sex = s;
            birthYear = b;
            age = Globals.CurrentYear - birthYear;
            LifeSpan = (5 * Race.AgeMod) + Globals.Rand.Next(Race.AgeMod);
            Children = new List<Character>();

            if (age > LifeSpan)
                Alive = false;

            StreamReader File = new StreamReader(Race.Name + Sex + "Names.txt");
            string line;

            //Opens appropriate txt file of names and assigns random first name
           File = new StreamReader(Race.Name + Sex + "Names.txt");
            List<string> Names = new List<string>();
            while ((line = File.ReadLine()) != null)
            {
                Names.Add(line);
            }
            FirstName = Names[Globals.Rand.Next(Names.Count())];
            File.Close();

            //Assign random height and weight. Weight is based on height.
            Height = 30 + Globals.Rand.Next(Race.HeightMod, Race.HeightMod*2);
            Weight = Race.WeightMod + Globals.Rand.Next(Convert.ToInt32(Height * 1.5), Convert.ToInt32(Height * 3));

            //Opens appropriate file of occupations and assigns random occupation
            File = new StreamReader("Occupations.txt");
            List<string> Occupations = new List<string>();
            while((line = File.ReadLine()) !=null)
            {
                Occupations.Add(line);
            }
            Occupation = Occupations[Globals.Rand.Next(Occupations.Count())];
            File.Close();

            //Assign random last name
            File = new StreamReader(Race.Name + "Surnames.txt");
            List<string> Surnames = new List<string>();
            while((line = File.ReadLine()) !=null)
            {
                Surnames.Add(line);
            }
            LastName = Surnames[Globals.Rand.Next(Surnames.Count())];
            File.Close();

            //Assign random haircolor
            File = new StreamReader("HairColors.txt");
            List<string> HairColors = new List<string>();
            while((line = File.ReadLine()) !=null)
            {
                HairColors.Add(line);
            }
            HairColor = HairColors[Globals.Rand.Next(HairColors.Count())];
            File.Close();

            //Assign random eye color
            File = new StreamReader("EyeColors.txt");
            List<string> EyeColors = new List<string>();
            while((line = File.ReadLine()) !=null)
            {
                EyeColors.Add(line);
            }
            EyeColor = EyeColors[Globals.Rand.Next(EyeColors.Count)];
            File.Close();
        }

        //Generates a couples child
        public Character(Character d, Character m)
        {

            Id = Globals.ID;
            Globals.ID++;

            Children = new List<Character>();
            HasKids = false;
            HasSpouse = false;

            if (d.Sex == "Male")
            {
                Father = d;
                Mother = m;
            }            
            else
            {
                Father = m;
                Mother = d;
            }

            //Establish Race based on parents, NEEDS WORK FOR CROSS BREEDERS****************
            if (d.Race == m.Race)
                Race = d.Race;
            else Race = m.Race;

            //Randomizes gender
            if (Globals.Rand.Next(2) == 0)
                Sex = "Male";
            else Sex = "Female";

            StreamReader File = new StreamReader(Race.Name + Sex + "Names.txt");
            string line;

            LifeSpan = (5 * Race.AgeMod) + Globals.Rand.Next(Race.AgeMod);

            //Assign a random birth year that is 1-2 times the mothers age modifier years after their mother
            BirthYear = m.BirthYear + m.Race.AgeMod + Globals.Rand.Next(m.Race.AgeMod);
            Age = Globals.CurrentYear - BirthYear;

            if (Age > LifeSpan)
                Alive = false;            

            //Assign random height and weight. Weight is based on height.
            Height = 30 + Globals.Rand.Next(Race.HeightMod, Race.HeightMod * 2);
            Weight = Race.WeightMod + Globals.Rand.Next(Convert.ToInt32(Height * 1.5), Convert.ToInt32(Height * 3));

            //Inherits last name
            LastName = d.LastName;
                      
            //Establish eye color based on parents.
            if (d.EyeColor == m.EyeColor)
                EyeColor = d.EyeColor;
            else if (Globals.Rand.Next(2) == 0)
                EyeColor = d.EyeColor;
            else EyeColor = m.EyeColor;

            //Establish hair color based on parents
            if (d.HairColor == m.HairColor)
                HairColor = d.HairColor;
            else if (Globals.Rand.Next(2) == 0)
                HairColor = d.HairColor;
            else HairColor = m.HairColor;

            //Opens appropriate txt file of names and assigns random first name
            File = new StreamReader(Race.Name + Sex + "Names.txt");
            List<string> Names = new List<string>();
            while ((line = File.ReadLine()) != null)
            {
                Names.Add(line);
            }
            FirstName = Names[Globals.Rand.Next(Names.Count())];
            File.Close();

            //Opens appropriate file of occupations and assigns random occupation
            File = new StreamReader("Occupations.txt");
            List<string> Occupations = new List<string>();
            while ((line = File.ReadLine()) != null)
            {
                Occupations.Add(line);
            }
            Occupation = Occupations[Globals.Rand.Next(Occupations.Count())];
            File.Close();
        }
    }
}