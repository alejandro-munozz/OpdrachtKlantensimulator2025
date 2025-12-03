using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulatorBL.Managers
{
    public class DataManager
    {
        private IBestandsLezer bestandsLezer;
        private IKlantenSimulatorRepository_SQL repo;
        public DataManager(IBestandsLezer bestandsLezer, IKlantenSimulatorRepository_SQL repo)
        {
            this.bestandsLezer = bestandsLezer;
            this.repo = repo;
        }
    }
}
