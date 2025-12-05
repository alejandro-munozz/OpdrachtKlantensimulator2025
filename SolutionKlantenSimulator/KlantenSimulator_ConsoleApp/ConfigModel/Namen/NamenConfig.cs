using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulator_ConsoleApp.ConfigModel.Namen
{
    public class NamenConfig
    {
        public NamenTxtConfig? txt { get; set; }

        public NamenJsonConfig? json { get; set; }
    }
}
