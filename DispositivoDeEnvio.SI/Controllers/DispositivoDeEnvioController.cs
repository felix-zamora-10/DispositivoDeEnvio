//using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DispositivoDeEnvio.BW.MensajesAzure;
using DispositivoDeEnvio.BW.MensajesAzure.Contratos;
//using DispositivoDeEnvio.SI.Clases;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Devices.Client;

namespace DispositivoDeEnvio.SI.Controllers {
    [Route("api/DispositivoDeEnvio")]
    [ApiController]
    public class DispositivoDeEnvioController : ControllerBase {

        //private static TransportType s_transportType = TransportType.Mqtt;
        //private static string s_deviceConnectionString = "HostName=IoTHubTestLc.azure-devices.net;DeviceId=DTest01;SharedAccessKeyName=iothubowner;SharedAccessKey=3dJ6+zkw6yWjaTOI5VVK4r5xO4lawtzOs0AGM71kb8M=";

        //public static async Task<int> Init(string mensaje) {
        //    using (var deviceClient = DeviceClient.CreateFromConnectionString(s_deviceConnectionString, s_transportType)) {
        //        var sample = new AdministradorDeMensajes(deviceClient);
        //        await sample.RunSampleAsync(mensaje).ConfigureAwait(false);
        //    }

        //    Console.WriteLine("Done.\n");
        //    return 0;
        //}

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("EnviarMensajeAsync")]
        public async Task<string> EnviarMensajeAsync([FromServices] IConectorDeAzure conectorDeAzure, [FromQuery] string mensaje, [FromQuery] string dispositivo) {
            string mensajeRetornado;

            FlujoMensajesAzure flujoMensajesAzure = new FlujoMensajesAzure(conectorDeAzure);
            mensajeRetornado = await flujoMensajesAzure.EnviaMensajeAsync(mensaje, dispositivo);

            return mensajeRetornado;
        }

        [HttpGet("RecibirMensajeAsync")]
        public async Task<string> RecibirMensajeAsync([FromServices] IConectorDeAzure conectorDeAzure, [FromQuery] string dispositivo) {
            string mensajeRetornado;

            FlujoMensajesAzure flujoMensajesAzure = new FlujoMensajesAzure(conectorDeAzure);
            mensajeRetornado = await flujoMensajesAzure.RecibeMensajeAsync(dispositivo);

            return mensajeRetornado;
        }

        //[HttpPost("EnviarMensajeAsync")]
        //public async Task<int> EnviarMensajeAsync([FromQuery] string mensaje) {
        //    int foo = await Init(mensaje);

        //    return foo;
        //}
    }
}
