using DispositivoDeEnvio.BW.MensajesAzure.Contratos;
using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispositivoDeEnvio.SG.AzureIoT.Conector {
    public class ConectorDeAzure : IConectorDeAzure {

        private readonly TransportType tipoDeTransporte = TransportType.Mqtt;
        private string conexionDeDispositivo = "HostName=IoTHubTestLc.azure-devices.net;DeviceId={0};SharedAccessKeyName=iothubowner;SharedAccessKey=3dJ6+zkw6yWjaTOI5VVK4r5xO4lawtzOs0AGM71kb8M=";

        public async Task<string> EnviaMensajeAsync(string mensaje, List<string> dispositivos, Dictionary<string, string> paresDeNombres) {
            string mensajelocal = "";
            string mensajePorRetornar = "";
            string conesxionDeDispositivoNueva = "";

            for (int dispositivo = 0; dispositivo < dispositivos.Count; dispositivo++) {
                mensajelocal = string.Format("{0} > Enviando mensaje '{1}' desde el dispositivo '{2}'.\n|", 
                    DateTime.Now.ToLocalTime(), mensaje, ObtieneNombreDeDispositivo(dispositivos[dispositivo], paresDeNombres));
                conesxionDeDispositivoNueva = string.Format(conexionDeDispositivo, dispositivos[dispositivo]);

                using (var dispositivoCliente = DeviceClient.CreateFromConnectionString(conesxionDeDispositivoNueva, tipoDeTransporte)) {
                    await EjecutaEnviarMensajeAsync(mensaje, dispositivoCliente).ConfigureAwait(false);
                }

                mensajePorRetornar = string.Concat(mensajePorRetornar, mensajelocal);
            }

            return mensajePorRetornar;
        }

        public async Task<string> RecibeMensajeAsync(string dispositivo) {
            conexionDeDispositivo = string.Format(conexionDeDispositivo, dispositivo);
            using (var dispositivoCliente = DeviceClient.CreateFromConnectionString(conexionDeDispositivo, tipoDeTransporte)) {
                return await EjecutaRecibirMensajeAsync(dispositivoCliente).ConfigureAwait(false);
            }
        }

        private string ObtieneNombreDeDispositivo(string dispositivo, Dictionary<string, string> paresDeNombres) {
            string nombreDelDispositivo = "";

            nombreDelDispositivo = paresDeNombres.FirstOrDefault(elemento => elemento.Key == dispositivo).Value;

            return nombreDelDispositivo;
        }

        private async Task EjecutaEnviarMensajeAsync(string mensaje, DeviceClient dispositivoCliente) {
            using (var eventoDelMensaje = new Message(Encoding.UTF8.GetBytes(mensaje))) {
                await dispositivoCliente.SendEventAsync(eventoDelMensaje).ConfigureAwait(false);
            }
        }

        private async Task<string> EjecutaRecibirMensajeAsync(DeviceClient dispositivoCliente) {
            using (Message mensajeRecibido = await dispositivoCliente.ReceiveAsync(TimeSpan.FromSeconds(10)).ConfigureAwait(false)) {
                if (mensajeRecibido != null) {
                    string mensajeFinal = "";
                    string datosDelMensaje = Encoding.ASCII.GetString(mensajeRecibido.GetBytes());
                    mensajeFinal = string.Format(string.Concat("{0} > Mensaje recibido: ", datosDelMensaje, ".\n"), DateTime.Now.ToLocalTime());

                    await dispositivoCliente.CompleteAsync(mensajeRecibido).ConfigureAwait(false);

                    return mensajeFinal;
                } else {
                    return "No hay mensajes en la cola.\n";
                }
            }
        }
    }
}
