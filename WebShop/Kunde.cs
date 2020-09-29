using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop
{
    class Kunde
    {
        public List<Produkts> betaltVarer { get; set; }
        public string navn { get; set; }
        public DateTime dato { get; set; }

        public Kunde(string inputNavn) 
        {
            navn = inputNavn;
            dato = DateTime.Today;

            betaltVarer = new List<Produkts>();
        }

        public void udskriv() //Udskriver fakturer til kunden
        {
            dato = DateTime.Now;

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("             Webshop               ");
            Console.WriteLine("-----------------------------------");
            getvarer();
            Console.WriteLine($"Købt af {navn}          Dato: {dato}           Sum: {sum()}");
        }

        int sum() //Regner summen ud
        {
            int tal = 0;
            for (int i = 0; i < betaltVarer.Count; i++)
            {
                tal += betaltVarer[i].pris * betaltVarer[i].antal; 

            }
            return tal;
        }
        void getvarer() //Udskriver alle de varer som bruger har købt
        {
            for (int i = 0; i < betaltVarer.Count; i++)
            {
                Console.WriteLine($"{betaltVarer[i].navn.PadRight(20)} {betaltVarer[i].pris * betaltVarer[i].antal} {betaltVarer[i].antal.ToString().PadLeft(20)}");
            }
            
        }
        public void addToBetal(int varenummer, int vareAntal, List<Produkts> produkts) //Tilføjer vare til list
        {
            betaltVarer.Add(new Produkts(produkts[varenummer].navn,produkts[varenummer].pris,vareAntal));
        }
    }
}
