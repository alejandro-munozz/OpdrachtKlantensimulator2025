using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulator_ConsoleApp.ConfigModel.Namen
{
    public class NamenTxtConfig
    {
        public List<string> padenVoornamenMannen { get; set; }
        public int mannenNaamKolom { get; set; }
        public int mannenAantalKolom { get; set; }
        public string mannenScheidingsteken { get; set; }
        public int mannenRijenOverTeSlaan { get; set; }

        public List<string> padenVoornamenVrouwen { get; set; }
        public int vrouwenNaamKolom { get; set; }
        public int vrouwenAantalKolom { get; set; }
        public string vrouwenScheidingsteken { get; set; }
        public int vrouwenRijenOverTeSlaan { get; set; }

        public List<string> padenFamilienamen { get; set; }
        public int familienaamKolom { get; set; }
        public int familienaamAantalKolom { get; set; }
        public string familienaamScheidingsteken { get; set; }
        public int familienaamRijenOverTeSlaan { get; set; }
    }
}
