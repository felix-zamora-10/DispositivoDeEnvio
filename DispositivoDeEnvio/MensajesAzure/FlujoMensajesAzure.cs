using System.Collections.Generic;
using System.Threading.Tasks;
using DispositivoDeEnvio.BC.MensajesAzure;
using DispositivoDeEnvio.BW.MensajesAzure.Contratos;

namespace DispositivoDeEnvio.BW.MensajesAzure {
    public class FlujoMensajesAzure : IFlujoMensajesAzure {

        private readonly IConectorDeAzure conectorDeAzure;
        private readonly ManejadorDeDispositivos manejadorDeDispositivos;

        public FlujoMensajesAzure(IConectorDeAzure conectorDeAzure) {
            this.conectorDeAzure = conectorDeAzure;
            manejadorDeDispositivos = new ManejadorDeDispositivos();
        }

        public async Task<string> EnviaMensajeAsync(string mensaje, string dispositivo) {
            string mensajePorRetornar = "";
            List<string> dispositivosFinales;
            Dictionary<string, string> paresDeNombres;

            dispositivosFinales = manejadorDeDispositivos.ObtieneLosDispositivos(dispositivo);
            paresDeNombres = manejadorDeDispositivos.GeneraLosParesDeNombres();
            mensajePorRetornar = await conectorDeAzure.EnviaMensajeAsync(mensaje, dispositivosFinales, paresDeNombres);

            return mensajePorRetornar;
        }

        public async Task<string> RecibeMensajeAsync(string dispositivo) {
            string mensajePorRetornar = "";

            mensajePorRetornar = await conectorDeAzure.RecibeMensajeAsync(dispositivo);

            return mensajePorRetornar;
        }
    }
}
