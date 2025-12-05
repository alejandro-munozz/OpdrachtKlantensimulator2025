using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulator_ConsoleApp.ConfigModel.Adressen
{
    public class AdressenTxtConfig
    {
        public string padStraten { get; set; }
        public List<string> correcteHighways { get; set; }
        public List<string> skipNamen { get; set; }
        public List<string> verwijderWoordenGemeente { get; set; }
    }
}
