using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulatorDL.Lezers
{
    public class BestandsLezer : IBestandsLezer
    {
        public INaamLezer NaamLezer;
        public IAdresLezer AdresLezer;

        public BestandsLezer(INaamLezer naamLezer, IAdresLezer adresLezer)
        {
            NaamLezer = naamLezer;
            AdresLezer = adresLezer;
        }
    }
}
