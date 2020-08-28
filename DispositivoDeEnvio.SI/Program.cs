using System;
using System.Threading.Tasks;
using DispositivoDeEnvio.SI.Clases;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.Azure.Devices.Client;

namespace DispositivoDeEnvio.SI {
    public class Program {
        // Select one of the following transports used by DeviceClient to connect to IoT Hub.
        //private static TransportType s_transportType = TransportType.Amqp;
        //private static TransportType s_transportType = TransportType.Mqtt;
        //private static TransportType s_transportType = TransportType.Http1;
        //private static TransportType s_transportType = TransportType.Amqp_WebSocket_Only;
        //private static TransportType s_transportType = TransportType.Mqtt_WebSocket_Only;
        //private static string s_deviceConnectionString = Environment.GetEnvironmentVariable("IOTHUB_DEVICE_CONNECTION_STRING");

        public static void Main(string[] args) {
            CreateWebHostBuilder(args).Build().Run();
        }

            //public static async Task<int> Init(string[] args) {
            //    if (string.IsNullOrEmpty(s_deviceConnectionString) && args.Length > 0) {
            //        s_deviceConnectionString = args[0];
            //    }

            //    using (var deviceClient = DeviceClient.CreateFromConnectionString(s_deviceConnectionString, s_transportType)) {
            //        var sample = new AdministradorDeMensajes(deviceClient);
            //        await sample.RunSampleAsync().ConfigureAwait(false);
            //    }

            //    Console.WriteLine("Done.\n");
            //    return 0;
            //}

            public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
