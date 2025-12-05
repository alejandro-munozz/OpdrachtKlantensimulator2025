using KlantenSimulator_ConsoleApp.ConfigModel.Adressen;
using KlantenSimulator_ConsoleApp.ConfigModel.Namen;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulator_ConsoleApp.ConfigModel
{
    public class VersieConfig
    {
        public NamenConfig Namen { get; set; }

        public AdressenConfig Adressen { get; set; }
    }
}
