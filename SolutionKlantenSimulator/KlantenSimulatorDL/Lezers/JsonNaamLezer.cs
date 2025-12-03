using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static System.Collections.Specialized.BitVector32;

namespace KlantenSimulatorDL.Lezers
{
    public class JsonNaamLezer : INaamLezer
    {
        public List<string> LeesNamenJson(string pad, string mainSectie, string subSectie)
        {
            List<string> namen = new();

            string json = File.ReadAllText(pad);

            using var doc = (JsonDocument.Parse(json));
            var root = doc.RootElement;

            if (root.TryGetProperty(mainSectie, out JsonElement sectionElement) &&
                sectionElement.TryGetProperty(subSectie, out JsonElement subSectionElement))
            {
                foreach (var item in subSectionElement.EnumerateArray())
                {
                    namen.Add(item.GetString());
                }
            }
            return namen;
        }

        public Dictionary<string, int> LeesNamenTxt(List<string> paden, int naamKolom, int aantalKolom, char scheidingsTeken, int rijenOverTeSlaan)
        {
            throw new NotImplementedException();
        }
    }
}
