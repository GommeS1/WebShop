using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace WebShop
{
    class Produkts //General class for varer
    {
        public string navn { get; set; }
        public int pris { get; set; }
        public int antal { get; set; }

        public Produkts(string _navn, int _pris, int _antal)
        {
            pris = _pris;
            navn = _navn;
            antal = _antal;
        }
        public string udskriv() 
        {
            return navn.PadRight(30) + pris.ToString().PadRight(30) + antal.ToString();
        }
    }
}
