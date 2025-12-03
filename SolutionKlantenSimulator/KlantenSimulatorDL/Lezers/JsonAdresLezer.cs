using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace KlantenSimulatorDL.Lezers
{
    public class JsonAdresLezer : IAdresLezer
    {
        public (List<string> gemeenten, List<string> straten) LeesAdressenJson(string pad, string mainSectie, string subSectieGemeentes, string subSectieStraten)
        {
            List<string> gemeenten = new();
            List<string> straten = new();

            string json = File.ReadAllText(pad);

            using var doc = (JsonDocument.Parse(json));
            var root = doc.RootElement;

            if (root.TryGetProperty(mainSectie, out JsonElement sectionElement) &&
                sectionElement.TryGetProperty(subSectieGemeentes, out JsonElement subSectionElement))
            {
                foreach (var item in subSectionElement.EnumerateArray())
                {
                    gemeenten.Add(item.GetString());
                }
            }

            if (root.TryGetProperty(mainSectie, out JsonElement sectionElement2) &&
                sectionElement.TryGetProperty(subSectieStraten, out JsonElement subSectionElement2))
            {
                foreach (var item in subSectionElement2.EnumerateArray())
                {
                    straten.Add(item.GetString());
                }
            }
            return (gemeenten,straten);
        }

        public Dictionary<string, List<string>> LeesStraatGemeenteOSM(string pad, HashSet<string> correcteHighways, HashSet<string> skipWoorden, HashSet<string> verwijderWoorden)
        {
            throw new NotImplementedException();
        }
    }
}
