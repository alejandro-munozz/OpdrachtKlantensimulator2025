using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulator_ConsoleApp.ConfigModel.Namen
{
    public class NamenJsonConfig
    {
        public string padNamen { get; set; }
        public string mainSectie { get; set; }

        public string sectieMannenNamen { get; set; }
        public string sectieVrouwenNamen { get; set; }

        // Tsjechië extra
        public string sectieMannenFamilieNamen { get; set; }
        public string sectieVrouwenFamilieNamen { get; set; }
    }
}
