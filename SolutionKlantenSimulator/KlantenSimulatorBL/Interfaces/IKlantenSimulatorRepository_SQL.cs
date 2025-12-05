using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IKlantenSimulatorRepository_SQL
    {
        bool BestaatVersie(int landVersie);
        int UploadLandMetVersie(string landCode, int landVersie);
        void UploadNamen(Dictionary<string, int> namenAantal, Geslacht geslacht, int land_versieId, string naamType);
    }
}
