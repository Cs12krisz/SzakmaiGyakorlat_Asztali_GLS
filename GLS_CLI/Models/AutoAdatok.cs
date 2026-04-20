using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLS_CLI.Models
{
    public class AutoAdatok
    {
        public AutoAdatok(DateTime datum, string soforNeve, int napiKilometer, int kezbesitettCsomagokSzama, int napiFogyasztasLiterben)
        {
            Datum = datum;
            SoforNeve = soforNeve;
            NapiKilometer = napiKilometer;
            KezbesitettCsomagokSzama = kezbesitettCsomagokSzama;
            NapiFogyasztasLiterben = napiFogyasztasLiterben;
        }

        public AutoAdatok(string sor) 
        {
            string[] temps = sor.Split(';');
            Datum = DateTime.Parse(temps[0]);
            SoforNeve = temps[1];
            NapiKilometer = int.Parse(temps[2]);
            KezbesitettCsomagokSzama = int.Parse(temps[3]);
            NapiFogyasztasLiterben = int.Parse(temps[4]);
        }

        public void Modositjuk(AutoAdatok autoAdatok)
        {
            Datum = autoAdatok.Datum;
            SoforNeve = autoAdatok.SoforNeve;
            NapiKilometer = autoAdatok.NapiKilometer;
            KezbesitettCsomagokSzama = autoAdatok.KezbesitettCsomagokSzama;
            NapiFogyasztasLiterben = autoAdatok.NapiFogyasztasLiterben;
        }

        public DateTime Datum { get; private set; }
        public string SoforNeve { get; private set; }
        public int NapiKilometer { get; private set; }
        public int KezbesitettCsomagokSzama { get; private set; }
        public int NapiFogyasztasLiterben { get; private set; }


    }
}
