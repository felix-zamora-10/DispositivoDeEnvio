using System.Collections.Generic;

namespace DispositivoDeEnvio.BC.MensajesAzure {
    public class ManejadorDeDispositivos {

        const string AMBOS = "Ambos";

        public List<string> ObtieneLosDispositivos(string dispositivos) {
            List<string> dispositivosFinales = new List<string>();

            if (AMBOS == dispositivos) {
                dispositivosFinales.Add("DTest04");
                dispositivosFinales.Add("DTest05");
            } else
                dispositivosFinales.Add(dispositivos);

            return dispositivosFinales;
        }

        public Dictionary<string, string> GeneraLosParesDeNombres() {
            Dictionary<string, string> paresDeNombres = new Dictionary<string, string> {
                { "DTest01", "Validador 1" },
                { "DTest02", "Validador 2" },
                { "DTest03", "BackOffice" },
                { "DTest04", "Validador 4" },
                { "DTest05", "Validador 5" }
            };

            return paresDeNombres;
        }
    }
}