using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulatorDL.Lezers
{
    public class BestandsLezer : IBestandsLezer
    {
        private INaamLezer naamLezer;
        private IAdresLezer adresLezer;

        public BestandsLezer(INaamLezer naamLezer, IAdresLezer adresLezer)
        {
            this.naamLezer = naamLezer;
            this.adresLezer = adresLezer;
        }
    }
}
