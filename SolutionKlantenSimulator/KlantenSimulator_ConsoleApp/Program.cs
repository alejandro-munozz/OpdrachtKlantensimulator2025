using KlantenSimulator_ConsoleApp.ConfigModel;
using KlantenSimulator_ConsoleApp.ConfigModel.Adressen;
using KlantenSimulator_ConsoleApp.ConfigModel.Namen;
using KlantenSimulator_ConsoleApp.ConfigServices;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Managers;
using KlantenSimulatorDL.Lezers;
using KlantenSimulatorUtils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulator_ConsoleApp
{
    internal class Program
    {
        static DataManager dataManager;
        static string connectionString;
        static ConfigService configService = new("appsettings.json");
        static BestandsLezerConfig bestandsLezerConfig = new();

        static void Main(string[] args)
        {
            SetConfig();
            LeesAlles();
        }
        static void SetConfig()
        {
            bestandsLezerConfig = configService.LoadConfig();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = builder.Build();
            connectionString = config.GetConnectionString("SQLServer");
            dataManager = new(KlantenSimulatorRepositoryFactory.GeefRepository(connectionString));
        }

        static void LeesAlles()
        {
            foreach (var land in bestandsLezerConfig.BestandsLezers)
            {
                string landCode = land.Key;
                LandConfig landConfig = land.Value;
                foreach (var versie in landConfig.Versie)
                {
                    int landVersie = versie.Key;
                    VersieConfig versieConfig = versie.Value;
                    NamenConfig namen = versieConfig.Namen;
                    AdressenConfig adressen = versieConfig.Adressen;
                    int land_versieId = 0;
                    if (!dataManager.repo.BestaatVersie(landVersie))
                    {
                        land_versieId = dataManager.repo.UploadLandMetVersie(landCode, landVersie);
                        if (namen.json == null)
                        {
                            dataManager.bestandsLezer = BestandsLezerFactory.GeefBestandsLezer("txt", "txt");
                            BestandsLezer bl = (BestandsLezer)dataManager.bestandsLezer;
                            Dictionary<string, int> mannenNamenAantal = bl.NaamLezer.LeesNamenTxt(namen.txt.padenVoornamenMannen, namen.txt.mannenNaamKolom, namen.txt.mannenAantalKolom, Convert.ToChar(namen.txt.mannenScheidingsteken), namen.txt.mannenRijenOverTeSlaan);
                            dataManager.repo.UploadNamen(mannenNamenAantal, Geslacht.man, land_versieId, "voornaam");
                            Dictionary<string, int> vrouwenNamenAantal = bl.NaamLezer.LeesNamenTxt(namen.txt.padenVoornamenVrouwen, namen.txt.vrouwenNaamKolom, namen.txt.vrouwenAantalKolom, Convert.ToChar(namen.txt.vrouwenScheidingsteken), namen.txt.vrouwenRijenOverTeSlaan);
                            dataManager.repo.UploadNamen(vrouwenNamenAantal, Geslacht.vrouw, land_versieId, "voornaam");
                            Dictionary<string, int> familieNamenAantal = bl.NaamLezer.LeesNamenTxt(namen.txt.padenFamilienamen, namen.txt.familienaamKolom, namen.txt.familienaamAantalKolom, Convert.ToChar(namen.txt.familienaamScheidingsteken), namen.txt.familienaamRijenOverTeSlaan);
                            dataManager.repo.UploadNamen(familieNamenAantal, Geslacht.uni, land_versieId, "achternaam");
                        }
                        else if (namen.txt == null)
                        {
                            dataManager.bestandsLezer = BestandsLezerFactory.GeefBestandsLezer("json", "json");
                        }
                        if (adressen.json == null)
                        {
                            dataManager.bestandsLezer = BestandsLezerFactory.GeefBestandsLezer("txt", "txt");
                        }
                        else if (adressen.txt == null)
                        {
                            dataManager.bestandsLezer = BestandsLezerFactory.GeefBestandsLezer("json", "json");
                        }
                    }
                }
            }
        }


        //JSON LEZEN NAMEN MET ADRESSEN
        //var builder = new ConfigurationBuilder()
        //                .SetBasePath(Directory.GetCurrentDirectory())
        //                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        //var configuration = builder.Build();
        //string padNamen = configuration.GetSection($"BestandsLezers:CZE:Namen")["padNamen"];
        //string mainSectie = configuration.GetSection($"BestandsLezers:CZE:Namen")["mainSectie"];
        //string sectieMannenNamen = configuration.GetSection($"BestandsLezers:CZE:Namen")["sectieMannenNamen"];
        //string sectieVrouwenNamen = configuration.GetSection($"BestandsLezers:CZE:Namen")["sectieVrouwenNamen"];
        //string sectieMannenFamilieNamen = configuration.GetSection($"BestandsLezers:CZE:Namen")["sectieMannenFamilieNamen"];
        //string sectieVrouwenFamilieNamen = configuration.GetSection($"BestandsLezers:CZE:Namen")["sectieVrouwenFamilieNamen"];


        //string padAdressen = configuration.GetSection($"BestandsLezers:CZE:Adressen")["padAdressen"];
        //string mainSectieA = configuration.GetSection($"BestandsLezers:CZE:Adressen")["mainSectie"];
        //string sectieGemeenten = configuration.GetSection($"BestandsLezers:CZE:Adressen")["sectieGemeentes"];
        //string sectieStraten = configuration.GetSection($"BestandsLezers:CZE:Adressen")["sectieStraten"];


        //JsonNaamLezer nl = new();
        //JsonAdresLezer al = new();
        //BestandsLezer bl = new(nl, al);

        //List<string> mannenNamen = nl.LeesNamenJson(padNamen, mainSectie, sectieMannenNamen);
        //List<string> vrouwenNamen = nl.LeesNamenJson(padNamen, mainSectie, sectieVrouwenNamen);
        //List<string> familieNamenMan = nl.LeesNamenJson(padNamen, mainSectie, sectieMannenFamilieNamen);
        //List<string> familieNamenVrouw = nl.LeesNamenJson(padNamen, mainSectie, sectieVrouwenFamilieNamen);


        //(List<string> gemeenten, List<string> straten) gemeentesStraten = al.LeesAdressenJson(padAdressen, mainSectieA, sectieGemeenten, sectieStraten);

        //ADRESSEN LEZEN TXT
        //var builder = new ConfigurationBuilder()
        //                .SetBasePath(Directory.GetCurrentDirectory())
        //                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        //var configuration = builder.Build();
        //string padStraten = configuration.GetSection($"BestandsLezers:DNK")["padStraten"];
        //var correcteHighways = configuration.GetSection("BestandsLezers:DNK:correcteHighways").Get<string[]>();
        //var highwaySet = new HashSet<string>(correcteHighways);
        //var skipNamen = configuration.GetSection("BestandsLezers:DNK:skipNamen").Get<string[]>();
        //var skipNamenSet = new HashSet<string>(skipNamen);
        //var verwijderWoordenGemeente = configuration.GetSection("BestandsLezers:DNK:verwijderWoordenGemeente").Get<string[]>();
        //var verwijderWoordenGemeenteSet = new HashSet<string>(verwijderWoordenGemeente);

        //TxtNaamLezer nl = new();
        //TxtAdresLezer al = new();
        //BestandsLezer bl = new(nl, al);

        //Dictionary<string, List<string>> gemeenteStraten = al.LeesStraatGemeenteOSM(padStraten, highwaySet, skipNamenSet, verwijderWoordenGemeenteSet);

        //foreach (string gemeente in gemeenteStraten.Keys)
        //{
        //    foreach (string straat in gemeenteStraten[gemeente])
        //    {
        //        Console.WriteLine(gemeente + " " + straat);
        //    }
        //}
        //Console.WriteLine(gemeenteStraten.Keys.Count());



        // NAMEN LEZEN TXT
        //var landenSecties = configuration.GetSection("BestandsLezers");
        //foreach (var landSectie in landenSecties.GetChildren())
        //{
        //    //MANNEN VOORNAMEN
        //    List<string> paden = new();
        //    var padenSectie = configuration.GetSection($"BestandsLezers:{landSectie.Key}:padenVoornamenMannen");
        //    foreach (var p in padenSectie.GetChildren())
        //    {
        //        paden.Add(p.Value);
        //    }
        //    int mannenNaamKolom = int.Parse(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["mannenNaamKolom"]);
        //    int mannenAantalKolom = int.Parse(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["mannenAantalKolom"]);
        //    char mannenScheidingsteken = Convert.ToChar(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["mannenScheidingsteken"]);
        //    int mannenRijenOverTeSlaan = int.Parse(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["mannenRijenOverTeSlaan"]);

        //    TxtNaamLezer nl = new();
        //    TxtAdresLezer al = new();
        //    BestandsLezer bl = new(nl, al);

        //    Dictionary<string, int> mannenNamenAantal = nl.LeesNamen(paden, mannenNaamKolom, mannenAantalKolom, mannenScheidingsteken, mannenRijenOverTeSlaan);

        //    for (int i = 0; i < 20; i++)
        //    {
        //        string mannenNaam = mannenNamenAantal.Keys.ToList()[i];
        //        Console.WriteLine($"{mannenNaam}, {mannenNamenAantal[mannenNaam]}");
        //    }
        //    Console.WriteLine(mannenNamenAantal.Count());

        //    //VROUWEN VOORNAMEN
        //    paden = new();
        //    padenSectie = configuration.GetSection($"BestandsLezers:{landSectie.Key}:padenVoornamenVrouwen");
        //    foreach (var p in padenSectie.GetChildren())
        //    {
        //        paden.Add(p.Value);
        //    }
        //    int vrouwenNaamKolom = int.Parse(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["vrouwenNaamKolom"]);
        //    int vrouwenAantalKolom = int.Parse(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["vrouwenAantalKolom"]);
        //    char vrouwenScheidingsteken = Convert.ToChar(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["vrouwenScheidingsteken"]);
        //    int vrouwenRijenOverTeSlaan = int.Parse(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["vrouwenRijenOverTeSlaan"]);

        //    Dictionary<string, int> vrouwenNamenAantal = nl.LeesNamen(paden, vrouwenNaamKolom, vrouwenAantalKolom, vrouwenScheidingsteken, vrouwenRijenOverTeSlaan);

        //    for (int i = 0; i < 20; i++)
        //    {
        //        string vrouwenNaam = vrouwenNamenAantal.Keys.ToList()[i];
        //        Console.WriteLine($"{vrouwenNaam}, {vrouwenNamenAantal[vrouwenNaam]}");
        //    }
        //    Console.WriteLine(vrouwenNamenAantal.Count());

        //    //FAMILIENAMEN
        //    paden = new();
        //    padenSectie = configuration.GetSection($"BestandsLezers:{landSectie.Key}:padenFamilienamen");
        //    foreach (var p in padenSectie.GetChildren())
        //    {
        //        paden.Add(p.Value);
        //    }
        //    int familienaamKolom = int.Parse(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["familienaamKolom"]);
        //    int familienaamAantalKolom = int.Parse(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["familienaamAantalKolom"]);
        //    char familienaamScheidingsteken = Convert.ToChar(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["familienaamScheidingsteken"]);
        //    int familienaamRijenOverTeSlaan = int.Parse(configuration.GetSection($"BestandsLezers:{landSectie.Key}")["familienaamRijenOverTeSlaan"]);

        //    Dictionary<string, int> familienamenAantal = nl.LeesNamen(paden, familienaamKolom, familienaamAantalKolom, familienaamScheidingsteken, familienaamRijenOverTeSlaan);

        //    for (int i = 0; i < 20; i++)
        //    {
        //        string familienaam = familienamenAantal.Keys.ToList()[i];
        //        Console.WriteLine($"{familienaam}, {familienamenAantal[familienaam]}");
        //    }
        //    Console.WriteLine(familienamenAantal.Count());
        //    Console.WriteLine();
        //    Console.WriteLine();
        //}
    }
}
