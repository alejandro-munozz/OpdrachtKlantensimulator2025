using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulatorBL.Interfaces
{
    public interface INaamLezer
    {
        Dictionary<string, int> LeesNamenTxt(List<string> paden, int naamKolom, int aantalKolom, char scheidingsTeken, int rijenOverTeSlaan);

        List<string> LeesNamenJson(string pad,string mainSectie, string subSectie);
    }
}
