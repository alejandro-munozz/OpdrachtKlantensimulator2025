using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_SQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulatorUtils
{
    public static class KlantenSimulatorRepositoryFactory
    {
        public static IKlantenSimulatorRepository_SQL GeefRepository(string connectionString)
        {
            return new KlantenSimulatorRepository_SQL(connectionString);
        }
    }
}
