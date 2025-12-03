using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IAdresLezer
    {
        Dictionary<string, List<string>> LeesStraatGemeenteOSM(string pad, HashSet<string> correcteHighways, HashSet<string> skipWoorden, HashSet<string> verwijderWoorden);

        (List<string> gemeenten, List<string> straten) LeesAdressenJson(string pad, string mainSectie, string subSectieGemeentes, string subSectieStraten);
    }
}
