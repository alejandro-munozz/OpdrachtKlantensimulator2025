using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace KlantenSimulatorDL.Lezers
{
    public class TxtAdresLezer : IAdresLezer
    {
        public Dictionary<string, List<string>> LeesStraatGemeenteOSM(string pad, HashSet<string> correcteHighways, HashSet<string> skipWoorden, HashSet<string> verwijderWoorden)
        {
            Dictionary<string, List<string>> gemeenteStraten = new();
            using (StreamReader sr = new StreamReader(pad))
            {
                string line;
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split(';');
                    string highwayType = ss[2];
                    string gemeente = ss[0];
                    bool containsAny = skipWoorden.Any(x => gemeente.Contains(x));
                    if (correcteHighways.Contains(highwayType) && containsAny == false)
                    {
                        gemeente = verwijderWoorden.Aggregate(gemeente, (acc, word) => acc.Replace(word, string.Empty));
                        string straat = ss[1];
                        if (!gemeenteStraten.ContainsKey(gemeente))
                        {
                            List<string> straten = new List<string>();
                            straten.Add(straat);
                            gemeenteStraten.Add(gemeente, straten);
                        } else if (!gemeenteStraten[gemeente].Contains(straat))
                        {
                            gemeenteStraten[gemeente].Add(straat);
                        }
                    }
                }
            }
            return gemeenteStraten;
        }
    }
}
