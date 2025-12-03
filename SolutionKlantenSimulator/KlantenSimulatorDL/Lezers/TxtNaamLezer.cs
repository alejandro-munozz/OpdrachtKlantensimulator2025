using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulatorDL.Lezers
{
    public class TxtNaamLezer : INaamLezer
    {
        public List<string> LeesNamenJson(string pad, string mainSectie, string subSectie)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, int> LeesNamenTxt(List<string> paden, int naamKolom, int aantalKolom, char scheidingsTeken, int rijenOverTeSlaan)
        {
            Dictionary<string, int> namenAantal = new();
            foreach (string pad in paden)
            {
                using (StreamReader sr = new StreamReader(pad))
                {
                    string line;
                    for (int i = 0; i < rijenOverTeSlaan; i++)
                    {
                        sr.ReadLine();
                    }
                    while ((line = sr.ReadLine()) != null)
                    {
                        string voornaam = line.Split(scheidingsTeken)[naamKolom];
                        int aantal;
                        bool isGetal = int.TryParse(line.Split(scheidingsTeken)[aantalKolom].Replace(".", string.Empty), out aantal);
                        if (!namenAantal.ContainsKey(voornaam) && isGetal == true)
                        {
                            namenAantal.Add(voornaam, aantal);
                        }
                    }
                }
            }
            return namenAantal;
        }
    }
}
