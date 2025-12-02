using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulatorDL.Lezers
{
    public class BestandsLezer
    {
        private INaamLezer txtNaamLezer;
        private IAdresLezer txtAdresLezer;

        public BestandsLezer(INaamLezer naamLezer, IAdresLezer adresLezer)
        {
            this.txtNaamLezer = naamLezer;
            this.txtAdresLezer = adresLezer;
        }
    }
}
