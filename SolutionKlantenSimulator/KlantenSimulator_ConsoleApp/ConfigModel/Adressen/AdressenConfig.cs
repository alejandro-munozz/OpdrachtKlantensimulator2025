using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulator_ConsoleApp.ConfigModel.Adressen
{
    public class AdressenConfig
    {
        public AdressenJsonConfig? json { get; set; }

        public AdressenTxtConfig? txt { get; set; }
    }
}
