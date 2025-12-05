using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL.Lezers;

namespace KlantenSimulatorUtils
{
    public static class BestandsLezerFactory
    {
        public static IBestandsLezer GeefBestandsLezer(string naamLezerType, string adresLezerType)
        {
            return new BestandsLezer(GeefNaamLezer(naamLezerType), GeefAdresLezer(adresLezerType));
        }

        public static INaamLezer GeefNaamLezer(string type)
        {
            switch (type)
            {
                case "txt":
                    return new TxtNaamLezer();
                    break;

                case "json":
                    return new JsonNaamLezer();
                    break;

                default:
                    return null;
            }
        }

        public static IAdresLezer GeefAdresLezer(string type)
        {
            switch (type)
            {
                case "txt":
                    return new TxtAdresLezer();
                    break;

                case "json":
                    return new JsonAdresLezer();
                    break;

                default:
                    return null;
            }
        }
    }
}
