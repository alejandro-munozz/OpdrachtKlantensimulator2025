// See https://aka.ms/new-console-template for more information
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL.Lezers;
using Microsoft.Extensions.Configuration;


var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = builder.Build();
string padStraten = configuration.GetSection($"BestandsLezers:BEL")["padStraten"];
var correcteHighways = configuration.GetSection("BestandsLezers:BEL:correcteHighways").Get<string[]>();
var highwaySet = new HashSet<string>(correcteHighways);
var skipNamen = configuration.GetSection("BestandsLezers:BEL:skipNamen").Get<string[]>();
var skipNamenSet = new HashSet<string>(skipNamen);
var verwijderWoordenGemeente = configuration.GetSection("BestandsLezers:BEL:verwijderWoordenGemeente").Get<string[]>();
var verwijderWoordenGemeenteSet = new HashSet<string>(verwijderWoordenGemeente);

TxtNaamLezer nl = new();
TxtAdresLezer al = new();
BestandsLezer bl = new(nl, al);

Dictionary<string, List<string>> gemeenteStraten = al.LeesStraatGemeenteOSM(padStraten, highwaySet, skipNamenSet, verwijderWoordenGemeenteSet);

foreach (string gemeente in gemeenteStraten.Keys)
{
    foreach (string straat in gemeenteStraten[gemeente])
    {
        Console.WriteLine(gemeente + " " + straat);
    }
}
Console.WriteLine(gemeenteStraten.Keys.Count());




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



