using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulatorBL.Managers
{
    public class DataManager
    {
        public IBestandsLezer bestandsLezer;
        public IKlantenSimulatorRepository_SQL repo;
        public DataManager(IKlantenSimulatorRepository_SQL repo)
        {
            this.repo = repo;
        }
    }
}
