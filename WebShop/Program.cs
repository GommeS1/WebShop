using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace WebShop
{
    class Program
    {
        static FileSystem fileSystem;
        static Kunde kunde;
        static void Main(string[] args)
        {
            //Ny FileSystem object
            fileSystem = new FileSystem();
            

            Console.Write("Skriv dit navn: ");
            string kundeNavn = Console.ReadLine();

            //Ny Kunde object
            kunde = new Kunde(kundeNavn);

            //Loop menu
            bool power = true;
            do
            {
                menu();
                valgAfMenu();

                Console.ReadKey();

            } while (power);
        }

        public static void menu() //Udskriver menu til bruger
        {
            Console.Clear();
            Console.WriteLine($"Velkommen {kunde.navn}");

            Console.WriteLine("Skriv en af mulighederne: \r\n");
            Console.WriteLine("Bestil vare (bestil)");
            Console.WriteLine("Se varer (udskriv)");
            Console.WriteLine("Gå til kassen (betal)\r\n\r\n");
            Console.WriteLine("Kun for medarbjeder: \r\n");
            Console.WriteLine("Tilføj varer (tilføj)");
            Console.WriteLine("Slet varer (slet)");

            Console.Write("\r\nSkriv her: ");
        }
        public static void valgAfMenu() //Får brugerinput og behandler det
        {
            
            bool power = false;
            do
            {
                string valg = Console.ReadLine();
                switch (valg.ToLower())
                {
                    case "bestil":
                        bestilVarer();
                        power = false;
                        break;
                    case "udskriv":
                        udskrivVarer();
                        power = false;
                        break;
                    case "betal":
                        kunde.udskriv(); //Uskriver fakturer ud 
                        power = false;
                        break;
                    case "tilføj":
                        fileSystem.AddToFile(); //Tilføjer varer til fil
                        power = false;
                        break;
                    case "slet":
                        fileSystem.removeVarer(); //Fjerner varer fra fil
                        power = false;
                        break;
                    default:
                        power = true;
                        break;
                }
            } while (power);
        }

        public static void udskrivVarer() //Udskriver varer ud til bruger så man kan hvad der er 
        {
            Console.Clear();
            List<Produkts> vareList = fileSystem.readFile();

            Console.WriteLine("Navn" + "Pris".PadLeft(30) + "Antal".PadLeft(30));

            foreach (var vare in vareList)
            {
                Console.WriteLine(vare.udskriv());
            }
        }

        public static void bestilVarer() //"Bestiller" varer
        {
            Console.WriteLine("Navn" + "Pris".PadLeft(32) + "Antal".PadLeft(32));

            for (int i = 0; i < fileSystem.varer.Count; i++)
            {
                Console.WriteLine($"{i}. {fileSystem.varer[i].udskriv()}");
            }
            Console.WriteLine("Skriv nummeret på den varer du vil bestille: ");
            int userInput = Converters.UserStringToInt(Console.ReadLine());

            while (fileSystem.varer[userInput].antal <= 0)
            {
                Console.WriteLine("Der er ikke nolge på lager, vælg en anden vare ");
                userInput = Converters.UserStringToInt(Console.ReadLine());
            }

            Console.WriteLine("Skriv hvor mange du vil have: ");
            int antalKøbt = Converters.UserStringToInt(Console.ReadLine());

            while (fileSystem.varer[userInput].antal < antalKøbt) 
            {
                Console.WriteLine("Der er ikke nok på lager, skriv et mindre tal");
                antalKøbt = Converters.UserStringToInt(Console.ReadLine());
            }
            kunde.addToBetal(userInput, antalKøbt, fileSystem.varer); //Gemmer hvad bruger har købt

            fileSystem.varer[userInput].antal -= antalKøbt;
            fileSystem.UpdateFile(); //Updater varer lager (Fil)

        }
    }
}
