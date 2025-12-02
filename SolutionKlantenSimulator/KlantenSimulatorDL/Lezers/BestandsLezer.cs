using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulatorDL.Lezers
{
    public class BestandsLezer
    {
        private ITxtNaamLezer txtNaamLezer;
        private IAdresLezer txtAdresLezer;

        public BestandsLezer(ITxtNaamLezer txtNaamLezer, IAdresLezer txtAdresLezer)
        {
            this.txtNaamLezer = txtNaamLezer;
            this.txtAdresLezer = txtAdresLezer;
        }
    }
}
