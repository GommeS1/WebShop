using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebShop
{

    class FileSystem //Varer lagerer som håndteres i en json fil
    {
        public List<Produkts> varer { get; set; }

        string path = AppDomain.CurrentDomain.BaseDirectory + "\\Varer.json";

        public FileSystem() 
        {
            //Prøver at læse fil
            //Hvis den ikke kan så opretter den en ny json fil
            try
            {
                varer = readFile(); 
            }
            catch (Exception ex)
            {
                varer = new List<Produkts>();
                FirstWriteFile();
            }
            
        }
        void FirstWriteFile() //Laver json fil
        {
            varer.Add(new Produkts("Test",10,2));

            var jsonToWrite = JsonConvert.SerializeObject(varer, Formatting.Indented);

            using (var writer = new StreamWriter(path))
            {
                writer.Write(jsonToWrite);
            }
        }

        Produkts addVarer() //Opretter ny varer til lager
        {
            Console.Clear();
            Console.Write("Vare navn: ");
            string navn = Console.ReadLine();

            Console.Write("\r\nPris: ");
            int pris = Converters.UserStringToInt(Console.ReadLine());

            Console.Write("\r\nAntal: ");
            int antal = Converters.UserStringToInt(Console.ReadLine());

            var vare = new Produkts(navn, pris, antal);
            return vare;
        }

        
        public void AddToFile() //Tilføjer vare til lager
        {
            var list = readFile();
            list.Add(addVarer());
            varer = list;
            UpdateFile();
        }
        public void removeVarer() //Fjerner varer
        {
            var list = readFile();

            Console.Clear();
            Console.WriteLine("Navn" + "Pris".PadLeft(32) + "Antal".PadLeft(32));

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i}. {list[i].udskriv()}");
            }
            Console.WriteLine("Vælg et nummer som du vil slette: ");
            int userInput = Converters.UserStringToInt(Console.ReadLine());


            list.Remove(list[userInput]);

            if (list.Count == 0) 
            {
                list.Add(new Produkts("Du er klam",0,1));
            }

            varer = list;
            UpdateFile();
        }
        public void UpdateFile() //Updater lager efter der er sket en ændring
        {
            var jsonToWrite = JsonConvert.SerializeObject(varer, Formatting.Indented);

            using (var writer = new StreamWriter(path))
            {
                writer.Write(jsonToWrite);
            }
        }
        public List<Produkts> readFile()// læser fil
        {
            string jsonFromFile;
            using (var reader = new StreamReader(path))
            {
                jsonFromFile = reader.ReadToEnd();
            }

            var FileTing = JsonConvert.DeserializeObject<List<Produkts>>(jsonFromFile);

            return FileTing;
        }
    }
}
